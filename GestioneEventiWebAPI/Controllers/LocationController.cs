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
using EventiWebAPI.Models;

namespace EventiWebAPI.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class LocationController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Location
        //[Authorize]
        public IEnumerable<LocationViewModel> GetLocations()
        {
            var result = from l in db.Locations
                         select new LocationViewModel { IdLocation=l.IdLocation
                          , Cap=l.Indirizzo.Cap
                          , Capienza=l.Capienza
                          , Citta=l.Indirizzo.Citta
                          , Descrizione=l.Descrizione
                          , Nome=l.Nome
                          , NumeroCivico=l.Indirizzo.NumeroCivico
                          , Provincia=l.Indirizzo.Provincia
                          , Via=l.Indirizzo.Via
                         };
            return result.ToList();
        }

        // GET: api/Location/5
        [ResponseType(typeof(Location))]
        //[Authorize]
        public IHttpActionResult GetLocation(int id)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            LocationViewModel result= new LocationViewModel
            {
                IdLocation = location.IdLocation
             ,
                Cap = location.Indirizzo.Cap
             ,
                Capienza = location.Capienza
             ,
                Citta = location.Indirizzo.Citta
             ,
                Descrizione = location.Descrizione
             ,
                Nome = location.Nome
             ,
                NumeroCivico = location.Indirizzo.NumeroCivico
             ,
                Provincia = location.Indirizzo.Provincia
             ,
                Via = location.Indirizzo.Via
            };

            return Ok(result);
        }

        // PUT: api/Location/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocation(int id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.IdLocation)
            {
                return BadRequest();
            }

            db.Entry(location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Location
        [ResponseType(typeof(Location))]
        public IHttpActionResult PostLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locations.Add(location);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = location.IdLocation }, location);
        }

        // DELETE: api/Location/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult DeleteLocation(int id)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            db.Locations.Remove(location);
            db.SaveChanges();

            return Ok(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationExists(int id)
        {
            return db.Locations.Count(e => e.IdLocation == id) > 0;
        }
    }
}