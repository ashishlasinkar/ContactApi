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
using ContactCRUD.Models;

namespace ContactCRUD.Controllers
{
    public class ContactController : ApiController
    {
        private ContactEntitie db = new ContactEntitie();

        // GET: api/Contact
        public IQueryable<tbl_ContactDetails> Gettbl_ContactDetails()
        {
            return db.tbl_ContactDetails;
        }

        // GET: api/Contact/5
        [ResponseType(typeof(tbl_ContactDetails))]
        public IHttpActionResult Gettbl_ContactDetails(int id)
        {
            tbl_ContactDetails tbl_ContactDetails = db.tbl_ContactDetails.Find(id);
            if (tbl_ContactDetails == null)
            {
                return NotFound();
            }

            return Ok(tbl_ContactDetails);
        }

        // PUT: api/Contact/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_ContactDetails(int id, tbl_ContactDetails tbl_ContactDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_ContactDetails.Id)
            {
                return BadRequest();
            }

            db.Entry(tbl_ContactDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ContactDetailsExists(id))
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

        // POST: api/Contact
        [ResponseType(typeof(tbl_ContactDetails))]
        public IHttpActionResult Posttbl_ContactDetails(tbl_ContactDetails ContactDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<tbl_ContactDetails> tbl_ContactDetails = db.tbl_ContactDetails.Where(c=>c.Email==ContactDetails.Email).ToList();
            if (tbl_ContactDetails.Any())
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                "Email already exist with other contact"));
            }

            tbl_ContactDetails = db.tbl_ContactDetails.Where(c => c.PhoneNumber == ContactDetails.PhoneNumber).ToList();
            if (tbl_ContactDetails.Any())
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                "Phone Number is already exist with other contact"));
            }
            db.tbl_ContactDetails.Add(ContactDetails);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ContactDetails.Id }, ContactDetails);
        }

        // DELETE: api/Contact/5
        [ResponseType(typeof(tbl_ContactDetails))]
        public IHttpActionResult Deletetbl_ContactDetails(int id)
        {
            tbl_ContactDetails tbl_ContactDetails = db.tbl_ContactDetails.Find(id);
            if (tbl_ContactDetails == null)
            {
                return NotFound();
            }

            db.tbl_ContactDetails.Remove(tbl_ContactDetails);
            db.SaveChanges();

            return Ok(tbl_ContactDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ContactDetailsExists(int id)
        {
            return db.tbl_ContactDetails.Count(e => e.Id == id) > 0;
        }
    }
}