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
using practica2.Models;

namespace practica2.Controllers
{
    public class rossesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/rosses
        public IQueryable<ross> Getross()
        {
            return db.ross;
        }

        // GET: api/rosses/5
        [ResponseType(typeof(ross))]
        public IHttpActionResult Getross(int id)
        {
            ross ross = db.ross.Find(id);
            if (ross == null)
            {
                return NotFound();
            }

            return Ok(ross);
        }

        // PUT: api/rosses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putross(int id, ross ross)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ross.rossID)
            {
                return BadRequest();
            }

            db.Entry(ross).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!rossExists(id))
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

        // POST: api/rosses
        [ResponseType(typeof(ross))]
        public IHttpActionResult Postross(ross ross)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ross.Add(ross);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ross.rossID }, ross);
        }

        // DELETE: api/rosses/5
        [ResponseType(typeof(ross))]
        public IHttpActionResult Deleteross(int id)
        {
            ross ross = db.ross.Find(id);
            if (ross == null)
            {
                return NotFound();
            }

            db.ross.Remove(ross);
            db.SaveChanges();

            return Ok(ross);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool rossExists(int id)
        {
            return db.ross.Count(e => e.rossID == id) > 0;
        }
    }
}