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
using Final.Models;

namespace Final.Controllers
{
    public class DiscountsController : ApiController
    {
        private ShopDbEntities db = new ShopDbEntities();

        // GET: api/Discounts
        [Authorize(Roles = "Accountant")]
        public IQueryable<Discount> GetDiscounts()
        {
            return db.Discounts;
        }

        // GET: api/Discounts/5
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(Discount))]
        public IHttpActionResult GetDiscount(int id)
        {
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return NotFound();
            }

            return Ok(discount);
        }

        // PUT: api/Discounts/5
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiscount(int id, Discount discount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discount.Id)
            {
                return BadRequest();
            }

            db.Entry(discount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountExists(id))
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

        // POST: api/Discounts
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(Discount))]
        public IHttpActionResult PostDiscount(Discount discount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Discounts.Add(discount);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DiscountExists(discount.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = discount.Id }, discount);
        }

        // DELETE: api/Discounts/5
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(Discount))]
        public IHttpActionResult DeleteDiscount(int id)
        {
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return NotFound();
            }

            db.Discounts.Remove(discount);
            db.SaveChanges();

            return Ok(discount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscountExists(int id)
        {
            return db.Discounts.Count(e => e.Id == id) > 0;
        }
    }
}