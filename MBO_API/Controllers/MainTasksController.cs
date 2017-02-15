using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MBO_API.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MBO_API.Controllers
{
    public class MainTasksController : ApiController
    {
        public class MainTaskUsers
        {
            public MainTask mainTask { get; set; }
            public string[] users { get; set; }
        }
        
        public class TaskListResult
        {
            public List<MainTask> mainTask { get; set; }
            public int count { get; set; }
            public int last_page { get; set; }
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MainTasks
        public List<MainTask> GetMainTask()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            //var taskList = db.MainTask;

            var taskList = from m in db.MainTask
                           where (m.AssignedTo.All(u => u.Id == userId) || m.AssignedByID == userId) && m.IsDeleted == false
                           select m;

            return taskList.ToList();
        }
        
        // GET: api/MainTasks?type=created        
        public TaskListResult GetMainTask(string type, string filter = "", string orderby = "DateDue", int page = 1, int pagesize = 3)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var last_page = 0;
            IQueryable<MainTask> taskList;

            switch (type)
            {
                case "assigned":
                    taskList = from m in db.MainTask
                               where m.AssignedTo.All(u => u.Id == userId) && m.IsDeleted == false
                               select m;
                    break;
                case "created":
                    taskList = from m in db.MainTask
                               where m.AssignedByID == userId && m.IsDeleted == false
                               select m;
                    break;
                case "completed":
                    taskList = (from m in db.MainTask
                                where (m.AssignedTo.All(u => u.Id == userId) || m.AssignedByID == userId) && m.Progress == 100 && m.IsDeleted == false
                                select m);
                    break;
                case "trash":
                    taskList = (from m in db.MainTask
                                where (m.AssignedTo.All(u => u.Id == userId) || m.AssignedByID == userId) && m.IsDeleted == true
                                select m);
                    break;
                default:
                    taskList = from m in db.MainTask
                               where (m.AssignedTo.All(u => u.Id == userId) || m.AssignedByID == userId)
                               select m;
                    break;
            }
            
            if (!string.IsNullOrWhiteSpace(filter))
            {
                taskList = taskList.Where(t => t.Description.Contains(filter) || t.Title.Contains(filter));
            }

            var count = taskList.Count();            
            var mod = count % pagesize;
            last_page = mod > 0 ? ((count - mod) / pagesize) + 1: count/pagesize;
            
            return new TaskListResult
            {
                mainTask = taskList.OrderByDescending(m => m.DateDue).Skip(pagesize * (page - 1)).Take(pagesize).Include(m => m.AssignedTo).ToList(),
                count = count,
                last_page = last_page == 0 ? 1: last_page
            };
        }

        // GET: api/MainTasks/5
        [ResponseType(typeof(MainTask))]
        public IHttpActionResult GetMainTask(int id)
        {
            MainTask mainTask = db.MainTask.Find(id);
            if (mainTask == null)
            {
                return NotFound();
            }

            return Ok(mainTask);
        }

        // PUT: api/MainTasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMainTask(int id, MainTask mainTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mainTask.MainTaskID)
            {
                return BadRequest();
            }

            db.Entry(mainTask).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                //TODO Add progress history and comment
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MainTasks
        [ResponseType(typeof(MainTask))]
        public IHttpActionResult PostMainTask(MainTaskUsers mainTaskUsers)
        {
            var mainTask = mainTaskUsers.mainTask;
            var users = mainTaskUsers.users;
            var userId = RequestContext.Principal.Identity.GetUserId();

            mainTask.IsDeleted = false;
            mainTask.AssignedByID = userId;
            mainTask.DateAssigned = DateTime.Now;
            mainTask.Status = db.Status.Where(s => s.Level == 0).FirstOrDefault();
            mainTask.Progress = 0;

            ModelState.Remove("mainTaskUsers.mainTask.AssignedByID");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var usr_id in users)
            {
                var usr = db.Users.Find(usr_id);
                mainTask.AssignedTo.Add(usr);
            }

            db.MainTask.Add(mainTask);
            db.SaveChanges();

            //Add default progress histry and comment
            var progressHisotry = new ProgressHistory()
            {                
                MainTaskID = mainTask.MainTaskID
            };
            db.ProgressHistories.Add(progressHisotry);

            var log = new Log()
            {
                ApplicationUserID = userId,
                MainTaskID = mainTask.MainTaskID
            };
            db.Logs.Add(log);

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mainTask.MainTaskID }, mainTask);
        }

        // Remove: api/MainTasks/Restore?id=5
        [Route("api/MainTasks/Restore")]
        [ResponseType(typeof(MainTask))]
        public IHttpActionResult RestoreMainTask(int id)
        {
            MainTask mainTask = db.MainTask.Find(id);
            if (mainTask == null)
            {
                return NotFound();
            }

            mainTask.IsDeleted = false;
            db.Entry(mainTask).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(mainTask);
        }

        // Remove: api/MainTasks/Remove?id=5
        [Route("api/MainTasks/Remove")]
        [ResponseType(typeof(MainTask))]
        public IHttpActionResult RemoveMainTask(int id)
        {
            MainTask mainTask = db.MainTask.Find(id);
            if (mainTask == null)
            {
                return NotFound();
            }

            mainTask.IsDeleted = true;
            db.Entry(mainTask).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(mainTask);
        }

        // DELETE: api/MainTasks/5
        [ResponseType(typeof(MainTask))]
        public IHttpActionResult DeleteMainTask(int id)
        {
            MainTask mainTask = db.MainTask.Find(id);
            if (mainTask == null)
            {
                return NotFound();
            }

            db.MainTask.Remove(mainTask);
            db.SaveChanges();

            return Ok(mainTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MainTaskExists(int id)
        {
            return db.MainTask.Count(e => e.MainTaskID == id) > 0;
        }
    }
}