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
    public class SalePurchasesController : ApiController
    {
        private ShopDbEntities db = new ShopDbEntities();

        // GET: api/SalePurchases
        [Authorize(Roles = "Accountant")]
        public IQueryable<SalePurchase> GetSalePurchases()
        {
            return db.SalePurchases;
        }

        // GET: api/SalePurchases/5
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(SalePurchase))]
        public IHttpActionResult GetSalePurchase(int id)
        {
            SalePurchase salePurchase = db.SalePurchases.Find(id);
            if (salePurchase == null)
            {
                return NotFound();
            }

            return Ok(salePurchase);
        }

        // PUT: api/SalePurchases/5
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalePurchase(int id, SalePurchase salePurchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salePurchase.Id)
            {
                return BadRequest();
            }

            db.Entry(salePurchase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalePurchaseExists(id))
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

        // POST: api/SalePurchases
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(SalePurchase))]
        public IHttpActionResult PostSalePurchase(SalePurchase salePurchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalePurchases.Add(salePurchase);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SalePurchaseExists(salePurchase.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salePurchase.Id }, salePurchase);
        }

        // DELETE: api/SalePurchases/5
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(SalePurchase))]
        public IHttpActionResult DeleteSalePurchase(int id)
        {
            SalePurchase salePurchase = db.SalePurchases.Find(id);
            if (salePurchase == null)
            {
                return NotFound();
            }

            db.SalePurchases.Remove(salePurchase);
            db.SaveChanges();

            return Ok(salePurchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalePurchaseExists(int id)
        {
            return db.SalePurchases.Count(e => e.Id == id) > 0;
        }
    }
}