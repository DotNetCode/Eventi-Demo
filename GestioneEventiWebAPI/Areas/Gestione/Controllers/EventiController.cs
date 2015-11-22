using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventiWebAPI.Models;

namespace EventiWebAPI.Areas.Gestione.Controllers
{
    public class EventiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gestione/Eventi
        public ActionResult Index()
        {
            var eventi = db.Eventi.Include(e => e.Location);
            return View(eventi.ToList());
        }

        // GET: Gestione/Eventi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventi.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: Gestione/Eventi/Create
        public ActionResult Create()
        {
            ViewBag.IdLocation = new SelectList(db.Locations, "IdLocation", "Nome");
            return View();
        }

        // POST: Gestione/Eventi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEvento,Nome,Descrizione,DescrizioneBreve,NumeroPosti,UrlEvento,UrlImmagine,Inizio,Fine,IdLocation")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Eventi.Add(evento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdLocation = new SelectList(db.Locations, "IdLocation", "Nome", evento.IdLocation);
            return View(evento);
        }

        // GET: Gestione/Eventi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventi.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLocation = new SelectList(db.Locations, "IdLocation", "Nome", evento.IdLocation);
            return View(evento);
        }

        // POST: Gestione/Eventi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEvento,Nome,Descrizione,DescrizioneBreve,NumeroPosti,UrlEvento,UrlImmagine,Inizio,Fine,IdLocation")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLocation = new SelectList(db.Locations, "IdLocation", "Nome", evento.IdLocation);
            return View(evento);
        }

        // GET: Gestione/Eventi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventi.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Gestione/Eventi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = db.Eventi.Find(id);
            db.Eventi.Remove(evento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
