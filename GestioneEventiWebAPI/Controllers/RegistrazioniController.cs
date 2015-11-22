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
    //[Authorize]
    public class RegistrazioniController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Registrazioni
        public IQueryable<Registrazione> GetRegistrazioni()
        {
            return db.Registrazioni;
        }

        // GET: api/Registrazioni/5
        [ResponseType(typeof(Registrazione))]
        public IHttpActionResult GetRegistrazione(int id)
        {
            Registrazione registrazione = db.Registrazioni.Find(id);
            if (registrazione == null)
            {
                return NotFound();
            }

            return Ok(registrazione);
        }

        // PUT: api/Registrazioni/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegistrazione(int id, Registrazione registrazione)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registrazione.IdRegistrazione)
            {
                return BadRequest();
            }

            db.Entry(registrazione).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrazioneExists(id))
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

        // POST: api/Registrazioni
        [ResponseType(typeof(Registrazione))]
        [Route("api/eventi/{idEvento}/RegistrazioneUtente/{userId}")]
        [HttpPost]
        public IHttpActionResult PostRegistrazione(string userId, int idEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Registrazione registrazione;

            int id = GetRegistrazioneUtente(userId, idEvento);
            if (id > 0) {
                registrazione= db.Registrazioni.Find(id);
            } else
            {
                int posti=GetPostiDisponibili(idEvento);
                DateTime? dataConfermaRegistrazione= null;
                if (posti > 0) dataConfermaRegistrazione = DateTime.Now;
                registrazione = new Registrazione()
                {
                    DataRichiestaRegistrazione = DateTime.Now
                    ,IdEvento = idEvento
                    ,UserId = userId
                    ,DataUltimoStato=DateTime.Now
                    ,DataConfermaRegistrazione = dataConfermaRegistrazione.Value 
                    ,StatoRegistrazione = (posti > 0) ? StatoRegistrazione.Confermata : StatoRegistrazione.StandBy
                };
                db.Registrazioni.Add(registrazione);
                db.SaveChanges();

            }

            if (registrazione.IdRegistrazione > 0) {
                return Ok(registrazione);
            }
            else
            {
                return BadRequest();
            }

            //return CreatedAtRoute("DefaultApi", new { id = registrazione.IdRegistrazione }, registrazione);
        }

        [Route("api/eventi/{idEvento}/CancellaRegistrazione/{userId}")]
        [HttpPost]
        public IHttpActionResult PostCancellaRegistrazione(string userId, int idEvento)
        {
            List<Registrazione> registrazioni = (from r in db.Registrazioni
                                                 where r.UserId == userId && r.IdEvento == idEvento
                                                 select r).ToList();
            if (registrazioni.Count == 0)
            {
                return NotFound();
            }

            foreach (var item in registrazioni)
            {
                db.Registrazioni.Remove(item);
            }

            db.SaveChanges();

            return Ok();

            //return CreatedAtRoute("DefaultApi", new { id = registrazione.IdRegistrazione }, registrazione);
        }

       

        private int GetPostiDisponibili(int id)
        {
            var numeroPosti = (from e in db.Eventi where e.IdEvento == id select e.NumeroPosti).FirstOrDefault();
            var numeroRegistrati = (from r in db.Registrazioni where r.IdEvento == id select r.UserId).Count();

            if (numeroPosti > numeroRegistrati)
            {
                return (numeroPosti - numeroRegistrati);
            }
            else
            {
                return 0;
            }

        }
        // DELETE: api/Registrazioni/5
        [ResponseType(typeof(Registrazione))]
        public IHttpActionResult DeleteRegistrazione(int id)
        {
            Registrazione registrazione = db.Registrazioni.Find(id);
            if (registrazione == null)
            {
                return NotFound();
            }

            db.Registrazioni.Remove(registrazione);
            db.SaveChanges();

            return Ok(registrazione);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegistrazioneExists(int id)
        {
            return db.Registrazioni.Count(e => e.IdRegistrazione == id) > 0;
        }
        private int GetRegistrazioneUtente(string userid, int idEvento)
        {
            var query = (from r in db.Registrazioni
                         where r.UserId == userid && r.IdEvento == idEvento
                         select r.IdRegistrazione).FirstOrDefault();
            return query;
        }
    }
}