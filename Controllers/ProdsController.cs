 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FoodBase.Data;
using FoodBase.Models;

namespace FoodBase.Controllers
{
    public class ProdsController : ApiController
    {
        private FoodBaseContext db = new FoodBaseContext();

        // GET: api/Prods
        public IQueryable<Prod> GetProds()
        {
            return db.Prods;
        }

        // GET: api/Prods/5
        [ResponseType(typeof(Prod))]
        public async Task<IHttpActionResult> GetProd(int id)
        {
            Prod prod = await db.Prods.FindAsync(id);
            if (prod == null)
            {
                return NotFound();
            }

            return Ok(prod);
        }

        // PUT: api/Prods/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProd(int id, Prod prod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prod.Id)
            {
                return BadRequest();
            }

            db.Entry(prod).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdExists(id))
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

        // POST: api/Prods
        [ResponseType(typeof(Prod))]
        public async Task<IHttpActionResult> PostProd(Prod prod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Prods.Add(prod);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = prod.Id }, prod);
        }

        // DELETE: api/Prods/5
        [ResponseType(typeof(Prod))]
        public async Task<IHttpActionResult> DeleteProd(int id)
        {
            Prod prod = await db.Prods.FindAsync(id);
            if (prod == null)
            {
                return NotFound();
            }

            db.Prods.Remove(prod);
            await db.SaveChangesAsync();

            return Ok(prod);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdExists(int id)
        {
            return db.Prods.Count(e => e.Id == id) > 0;
        }
    }
}