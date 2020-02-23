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
    public class ZordsController : ApiController
    {
        private FoodBaseContext db = new FoodBaseContext();

        // GET: api/Zords
        public IQueryable<ZordDto> GetZords()
        {
            var zords = from b in db.Zords
                        select new ZordDto()
                        {
                            Id = b.Id,
                            Name = b.Name,
                            ProdName = b.Prod.Name
                        };
            return zords;
        }

        // GET: api/Zords/5
        [ResponseType(typeof(ZordDetailDto))]
        public async Task<IHttpActionResult> GetZord(int id)
        {
            var zord = await db.Zords.Include(b => b.Prod).Select(b =>
              new ZordDetailDto()
              {
                  Id = b.Id,
                  Name = b.Name,
                  Year = b.Year,
                  ProdName = b.Prod.Name
              }).SingleOrDefaultAsync(b => b.Id == id);
            if (zord == null)
            {
                return NotFound();
            }

            return Ok(zord);
        }

        // PUT: api/Zords/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutZord(int id, Zord zord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zord.Id)
            {
                return BadRequest();
            }

            db.Entry(zord).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZordExists(id))
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

        // POST: api/Zords
        [ResponseType(typeof(Zord))]
        public async Task<IHttpActionResult> PostZord(Zord zord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Zords.Add(zord);
            await db.SaveChangesAsync();

            db.Entry(zord).Reference(x => x.Prod).Load();

            var dto = new ZordDto()
            {
                Id = zord.Id,
                Name = zord.Name,
                ProdName = zord.Prod.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = zord.Id }, dto);
        }

        // DELETE: api/Zords/5
        [ResponseType(typeof(Zord))]
        public async Task<IHttpActionResult> DeleteZord(int id)
        {
            Zord zord = await db.Zords.FindAsync(id);
            if (zord == null)
            {
                return NotFound();
            }

            db.Zords.Remove(zord);
            await db.SaveChangesAsync();

            return Ok(zord);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ZordExists(int id)
        {
            return db.Zords.Count(e => e.Id == id) > 0;
        }
    }
}