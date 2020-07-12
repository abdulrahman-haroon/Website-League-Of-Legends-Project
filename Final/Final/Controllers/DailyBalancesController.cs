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
    public class DailyBalancesController : ApiController
    {
        private ShopDbEntities db = new ShopDbEntities();

        // GET: api/DailyBalances
        [Authorize(Roles = "Accountant")]
        public IQueryable<DailyBalance> GetDailyBalances()
        {
            return db.DailyBalances;
        }

        // GET: api/DailyBalances/5
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(DailyBalance))]
        public IHttpActionResult GetDailyBalance(int id)
        {
            DailyBalance dailyBalance = db.DailyBalances.Find(id);
            if (dailyBalance == null)
            {
                return NotFound();
            }

            return Ok(dailyBalance);
        }

        // PUT: api/DailyBalances/5
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDailyBalance(int id, DailyBalance dailyBalance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dailyBalance.Id)
            {
                return BadRequest();
            }

            db.Entry(dailyBalance).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyBalanceExists(id))
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

        // POST: api/DailyBalances
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(DailyBalance))]
        public IHttpActionResult PostDailyBalance(DailyBalance dailyBalance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DailyBalances.Add(dailyBalance);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DailyBalanceExists(dailyBalance.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dailyBalance.Id }, dailyBalance);
        }

        // DELETE: api/DailyBalances/5
        [Authorize(Roles = "Accountant")]
        [ResponseType(typeof(DailyBalance))]
        public IHttpActionResult DeleteDailyBalance(int id)
        {
            DailyBalance dailyBalance = db.DailyBalances.Find(id);
            if (dailyBalance == null)
            {
                return NotFound();
            }

            db.DailyBalances.Remove(dailyBalance);
            db.SaveChanges();

            return Ok(dailyBalance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DailyBalanceExists(int id)
        {
            return db.DailyBalances.Count(e => e.Id == id) > 0;
        }
    }
}