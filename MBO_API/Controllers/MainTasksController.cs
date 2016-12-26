using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MBO_API.Models;

namespace MBO_API.Controllers
{
    public class MainTasksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MainTasks
        public IQueryable<MainTask> GetMainTask()
        {
            return db.MainTask;
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

            if (id != mainTask.MainTask_Id)
            {
                return BadRequest();
            }

            db.Entry(mainTask).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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
        public IHttpActionResult PostMainTask(MainTask mainTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MainTask.Add(mainTask);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mainTask.MainTask_Id }, mainTask);
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
            return db.MainTask.Count(e => e.MainTask_Id == id) > 0;
        }
    }
}