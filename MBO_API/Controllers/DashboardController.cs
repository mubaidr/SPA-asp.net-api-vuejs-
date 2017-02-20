using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MBO_API.Models;
using Microsoft.AspNet.Identity;
using System.Linq.Dynamic;

namespace MBO_API.Controllers
{
    public class DashboardController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public class Dashboard
        {
            public Dashboard()
            {
                LogsLatest = new HashSet<Log>();
                TasksCreatedLatest = new HashSet<MainTask>();
                TasksAssignedLatest = new HashSet<MainTask>();
                TasksCompletedLatest = new HashSet<MainTask>();
            }
            
            public int TasksCreatedCount { get; set; }

            public int TasksAssignedCount { get; set; }

            public int TasksCompletedCount { get; set; }

            public int TasksDeletedCount { get; set; }

            public int LogsCount { get; set; }

            public int MessagesReceivedCount { get; set; }

            public int MessagesSentCount { get; set; }

            public int MessagesDeletedCount { get; set; }

            public ICollection<MainTask> TasksCreatedLatest { get; set; }

            public ICollection<MainTask> TasksAssignedLatest { get; set; }

            public ICollection<MainTask> TasksCompletedLatest { get; set; }

            public ICollection<Log> LogsLatest { get; set; }

            public ICollection<Message> MessagesLatest { get; set; }
        }
        
        public Dashboard Get()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var dashboard = new Dashboard();

            dashboard.TasksCreatedCount = db.MainTask.Where(m => m.AssignedByID == userId && m.IsDeleted == false).Count();
            dashboard.TasksAssignedCount = db.MainTask.Where(m => m.AssignedTo.All(u => u.Id == userId) && m.IsDeleted == false).Count();
            dashboard.TasksCompletedCount = db.MainTask.Where(m => (m.AssignedTo.All(u => u.Id == userId) || m.AssignedByID == userId) && m.IsDeleted == false && m.Progress==100).Count();
            dashboard.TasksDeletedCount = db.MainTask.Where(m => (m.AssignedTo.All(u => u.Id == userId) || m.AssignedByID == userId) && m.IsDeleted == true).Count();

            dashboard.LogsCount = db.Logs.Where(l => l.MainTask.AssignedByID == userId || l.MainTask.AssignedTo.All(u => u.Id == userId)).Count();

            dashboard.MessagesReceivedCount = db.Message.Where(m => (m.ReceiverID == userId)).Count();
            dashboard.MessagesSentCount = db.Message.Where(m => (m.SenderID == userId)).Count();
            dashboard.MessagesDeletedCount = db.Message.Where(m => (m.ReceiverID == userId || m.SenderID == userId)).Count();

            dashboard.TasksCreatedLatest = db.MainTask.Where(m => m.AssignedByID == userId && m.IsDeleted == false).OrderByDescending(m=>m.DateAssigned).Take(10).ToList();
            dashboard.TasksAssignedLatest = db.MainTask.Where(m => m.AssignedTo.All(u => u.Id == userId) && m.IsDeleted == false).Take(10).ToList();
            dashboard.TasksCompletedLatest = db.MainTask.Where(m => (m.AssignedTo.All(u => u.Id == userId) || m.AssignedByID == userId) && m.IsDeleted == false && m.Progress == 100).OrderByDescending(m => m.DateCompleted).Take(10).ToList();

            dashboard.LogsLatest = db.Logs.Where(l => l.MainTask.AssignedByID == userId || l.MainTask.AssignedTo.All(u => u.Id == userId)).OrderByDescending(l => l.LogTime).Take(10).ToList();
            dashboard.MessagesLatest = db.Message.Where(m => (m.ReceiverID == userId || m.SenderID == userId)).OrderByDescending(m => m.Time).Take(10).ToList();

            return dashboard;
        }
    }
}
