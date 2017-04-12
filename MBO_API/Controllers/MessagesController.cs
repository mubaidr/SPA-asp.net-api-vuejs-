using MBO_API.Models;
using Microsoft.AspNet.Identity;
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

        // GET: api/Messages
        public IQueryable<Message> GetMessages([FromBody]string contact)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            return db.Messages.Where(m => (m.SenderID == userId && m.ReceiverID == contact) || (m.SenderID == contact || m.ReceiverID == userId)).OrderBy(m => m.Time);
        }

        // GET: api/Messages/GetAllContacts
        [Route("api/Messages/GetAllContacts")]
        public IQueryable<ApplicationUser> GetAllContacts()
        {
            return db.Users;
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