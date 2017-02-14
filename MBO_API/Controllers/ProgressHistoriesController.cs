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
    public class ProgressHistoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProgressHistories
        public IQueryable<ProgressHistory> GetProgressHistories()
        {
            return db.ProgressHistories;
        }

        // GET: api/ProgressHistories/5
        [ResponseType(typeof(ProgressHistory))]
        public IHttpActionResult GetProgressHistory(int id)
        {
            ProgressHistory progressHistory = db.ProgressHistories.Find(id);
            if (progressHistory == null)
            {
                return NotFound();
            }

            return Ok(progressHistory);
        }

        // PUT: api/ProgressHistories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProgressHistory(int id, ProgressHistory progressHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != progressHistory.ProgressHistoryID)
            {
                return BadRequest();
            }

            db.Entry(progressHistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressHistoryExists(id))
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

        // POST: api/ProgressHistories
        [ResponseType(typeof(ProgressHistory))]
        public IHttpActionResult PostProgressHistory(ProgressHistory progressHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProgressHistories.Add(progressHistory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = progressHistory.ProgressHistoryID }, progressHistory);
        }

        // DELETE: api/ProgressHistories/5
        [ResponseType(typeof(ProgressHistory))]
        public IHttpActionResult DeleteProgressHistory(int id)
        {
            ProgressHistory progressHistory = db.ProgressHistories.Find(id);
            if (progressHistory == null)
            {
                return NotFound();
            }

            db.ProgressHistories.Remove(progressHistory);
            db.SaveChanges();

            return Ok(progressHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProgressHistoryExists(int id)
        {
            return db.ProgressHistories.Count(e => e.ProgressHistoryID == id) > 0;
        }
    }
}