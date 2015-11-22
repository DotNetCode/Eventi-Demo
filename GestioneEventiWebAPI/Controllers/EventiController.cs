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
using Microsoft.AspNet.Identity;

namespace EventiWebAPI.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class EventiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Eventi
        //public IQueryable<Evento> GetEventi()
        //{
        //    return db.Eventi;
        //}
        // GET: api/Eventi
        //  [Authorize]

        public IEnumerable<EventoViewModel> GetEventi()
        {
            var result = (from e in db.Eventi.Include(x => x.Location)
                          where e.Fine >= DateTime.Now
                          select new EventoViewModel()
                          {
                              Descrizione = e.Descrizione,
                              DescrizioneBreve = e.DescrizioneBreve,
                              Fine = e.Fine,
                              IdEvento = e.IdEvento
                          ,
                              IdLocation = e.IdLocation,
                              Inizio = e.Inizio,
                              Nome = e.Nome,
                              NumeroPosti = e.NumeroPosti,
                              UrlEvento = e.UrlEvento,
                              UrlImmagine = e.UrlImmagine
                          ,
                              Location = e.Location.Nome
                          }).ToList();
            return result;
        }

        [Route("api/archivio")]
        public IEnumerable<EventoViewModel> GetEventiArchivio()
        {
            var result = (from e in db.Eventi.Include(x => x.Location)
                          where e.Fine <= DateTime.Now
                          select new EventoViewModel()
                          {
                              Descrizione = e.Descrizione,
                              DescrizioneBreve = e.DescrizioneBreve,
                              Fine = e.Fine,
                              IdEvento = e.IdEvento
                          ,
                              IdLocation = e.IdLocation,
                              Inizio = e.Inizio,
                              Nome = e.Nome,
                              NumeroPosti = e.NumeroPosti,
                              UrlEvento = e.UrlEvento,
                              UrlImmagine = e.UrlImmagine
                          ,
                              Location = e.Location.Nome
                          }).ToList();
            return result;
        }

        [Route("api/myeventi/{userid}")]
        public IEnumerable<EventoViewModel> GetMyEventi(string userid)
        {
            if (!string.IsNullOrEmpty(userid) && User.Identity.GetUserId() != userid && !User.IsInRole("Admin"))
            {
                userid = User.Identity.GetUserId();
            }

            var result = (from e in db.Eventi.Include(x => x.Location)
                          join r in db.Registrazioni on e.IdEvento equals r.IdEvento
                          where r.UserId == userid
                          orderby e.Inizio descending
                          select new EventoViewModel()
                          {
                              Descrizione = e.Descrizione,
                              DescrizioneBreve = e.DescrizioneBreve,
                              Fine = e.Fine,
                              IdEvento = e.IdEvento
                          ,
                              IdLocation = e.IdLocation,
                              Inizio = e.Inizio,
                              Nome = e.Nome,
                              NumeroPosti = e.NumeroPosti,
                              UrlEvento = e.UrlEvento,
                              UrlImmagine = e.UrlImmagine
                          ,
                              Location = e.Location.Nome
                          }).ToList();
            return result;
        }


        // GET: api/Eventi/5
        //[Authorize]
        [ResponseType(typeof(EventoViewModel))]
        public IHttpActionResult GetEvento(int id)
        {
            EventoViewModel evento = (from e in db.Eventi.Include(x => x.Location)
                          where e.IdEvento == id
                          select new EventoViewModel()
                          {
                              Descrizione = e.Descrizione,
                              DescrizioneBreve = e.DescrizioneBreve,
                              Fine = e.Fine,
                              IdEvento = e.IdEvento
                          ,
                              IdLocation = e.IdLocation,
                              Inizio = e.Inizio,
                              Nome = e.Nome,
                              NumeroPosti = e.NumeroPosti,
                              UrlEvento = e.UrlEvento,
                              UrlImmagine = e.UrlImmagine
                          ,
                              Location = e.Location.Nome
                              
                          }).FirstOrDefault();

            if (User.Identity.IsAuthenticated)
            {
                var userid = User.Identity.GetUserId();

                var stato = (from reg in db.Registrazioni
                             where reg.IdEvento == id && reg.UserId == userid
                             select reg.StatoRegistrazione).FirstOrDefault();
                if (stato == StatoRegistrazione.Richiesta || stato == StatoRegistrazione.StandBy || stato == StatoRegistrazione.Confermata) {
                    evento.isUserRegistred = true;
                }
                evento.StatoRegistrazioneUtente = stato;


            }
                // Evento evento = db.Eventi.Find(id);
                if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        [Route("api/eventi/{id}/PostiDisponibili")]
        [HttpGet]
        public IHttpActionResult PostiDisponibili(int id)
        {
            int numeroPosti = GetPostiDisponibili(id);

            return Ok(new { PostiDisponibili = numeroPosti });


        }


        // PUT: api/Eventi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvento(int id, Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evento.IdEvento)
            {
                return BadRequest();
            }

            db.Entry(evento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
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

        // POST: api/Eventi
        [ResponseType(typeof(Evento))]
        public IHttpActionResult PostEvento(Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Eventi.Add(evento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = evento.IdEvento }, evento);
        }

        // DELETE: api/Eventi/5
        [ResponseType(typeof(Evento))]
        public IHttpActionResult DeleteEvento(int id)
        {
            Evento evento = db.Eventi.Find(id);
            if (evento == null)
            {
                return NotFound();
            }

            db.Eventi.Remove(evento);
            db.SaveChanges();

            return Ok(evento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventoExists(int id)
        {
            return db.Eventi.Count(e => e.IdEvento == id) > 0;
        }

        private int GetPostiDisponibili(int id)
        {
            var numeroPosti = (from e in db.Eventi where e.IdEvento == id select e.NumeroPosti).FirstOrDefault();
            var numeroRegistrati = (from r in db.Registrazioni where r.IdEvento == id select r.UserId).ToList().Count;

            if (numeroPosti > numeroRegistrati)
            {
                return (numeroPosti - numeroRegistrati);
            }
            else
            {
                return 0;
            }

        }

    }
}