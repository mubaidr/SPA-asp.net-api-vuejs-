using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MBO_API.Models;
using Microsoft.AspNet.Identity;

namespace MBO_API.Controllers
{
    [Authorize]
    public class MessagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Messages
        public IQueryable<Message> GetMessages(string contact)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            return db.Messages.Where(m => (m.SenderID == userId || m.SenderID == contact) || (m.ReceiverID == userId || m.ReceiverID == contact));
        }

        // GET: api/Messages/5
        public IQueryable<ApplicationUser> GetAllContacts()
        {
            return db.Users;
        }

        // GET: api/Messages/5
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

            return contacts;
        }

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = message.MessageID }, message);
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