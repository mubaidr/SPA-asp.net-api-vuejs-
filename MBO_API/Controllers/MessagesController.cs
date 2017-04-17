using MBO_API.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace MBO_API.Controllers
{
    // TODO add document/file attach
    // [Authorize]
    public class MessagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public class MessageListResult
        {
            public List<Message> message { get; set; }
            public int count { get; set; }
            public int last_page { get; set; }
        }

        // GET: api/Messages
        public MessageListResult GetMessages(string folder, string filter = "", int page = 1, int pagesize = 12)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var last_page = 0;
            IQueryable<Message> msgs;

            switch (folder)
            {
                case "sent":
                    msgs = db.Messages.Where(m => m.SenderID == userId && m.IsDeleted == false);
                    break;
                case "trash":
                    msgs = db.Messages.Where(m => (m.SenderID == userId || m.ReceiverID == userId) && m.IsDeleted == true);
                    break;
                case "inbox":
                default:
                    msgs = db.Messages.Where(m => m.ReceiverID == userId && m.IsDeleted == false);
                    break;
            }

            if (!string.IsNullOrWhiteSpace(filter))
            {
                msgs = msgs.Where(m => m.Description.Contains(filter) || m.Sender.Email.Contains(filter) || m.Sender.FullName.Contains(filter));
            }

            var count = msgs.Count();
            var mod = count % pagesize;
            last_page = mod > 0 ? ((count - mod) / pagesize) + 1 : count / pagesize;

            return new MessageListResult
            {
                message = msgs.OrderBy(m => m.Time).Skip(pagesize * (page - 1)).Take(pagesize).Include(m => m.Sender).ToList(),
                count = count,
                last_page = last_page == 0 ? 1 : last_page
            };
        }

        // GET: api/Messages/GetAllContacts
        [Route("api/Messages/GetAllContacts")]
        public IQueryable<ApplicationUser> GetAllContacts()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            return db.Users.Where(u => u.Id != userId);
        }

        // GET: api/Messages/GetContacts
        [Route("api/Messages/GetContacts")]
        public IQueryable<ApplicationUser> GetContacts()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var msgs = db.Messages;
            var contacts = (from m in msgs
                            where m.SenderID == userId
                            select m.Receiver)
                           .Union(
                            from m in msgs
                            where m.ReceiverID == userId
                            select m.Sender
                            );

            return contacts.Where(u => u.Id != userId);
        }

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(ICollection<Message> message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = RequestContext.Principal.Identity.GetUserId();
            for (var i = 0; i < message.Count; i++)
            {
                var msg = message.ElementAt(i);
                msg.Time = DateTime.Now;
                msg.SenderID = userId;
                msg.IsRead = false;

                db.Messages.Add(msg);
            }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { success = true }, message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.MessageID == id) > 0;
        }
    }
}