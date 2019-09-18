using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;

namespace Hospital.Controllers
{
    public class TherapiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Therapy2
        public ActionResult Index(int? id)
        {
            var therapy2 = db.Therapies.Include(t => t.Patient);

           
                therapy2 = from therapies in db.Therapies.Include(a => a.Patient) where id == therapies.PatientId select therapies;

            return View(therapy2.ToList());
        }

        // GET: Therapy2/Details/5
        public ActionResult Details(int? id)
        {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Therapy therapy2 = db.Therapies.Include(sss=>sss.Patient).Single(p=>p.Id==id);
               var drug = db.Drugs.Find(therapy2.DrugId);
                ViewBag.drug = drug.Name;
            ViewBag.drugId = drug.Id;
                if (therapy2 == null)
                {
                    return HttpNotFound();
                }
                
                return View(therapy2);
           
        }

        // GET: Therapy2/Create
        [Authorize(Roles = "Doctor")]
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                var patient = db.Patients.Find(id);

                ViewBag.patientId = id;
                ViewBag.patient = patient.NameSurName;
                ViewBag.PatientId = id;
            }
            ViewBag.Drugs = db.Drugs.ToList();
            
            return View();
        }

        // POST: Therapy2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateFrom,DateTo,Diagnose,PatientId,DrugId")] Therapy therapy2)
        {
            if (ModelState.IsValid)
            {
                db.Therapies.Add(therapy2);


                string diagnose = Request.Form["Diagnose"];
                int pacient =int.Parse(Request.Form["PatientId"]);
                var patient = db.Patients.Find(pacient);
                var historyofdeseases = patient.HistoryOfDiseases.ToString();
                var added = historyofdeseases + " " + diagnose;
                patient.HistoryOfDiseases = added;

                db.SaveChanges();




                return RedirectToAction("Index");
            }

            ViewBag.PatientId = new SelectList(db.Patients, "Id", "NameSurName", therapy2.PatientId);
            return View(therapy2);
        }

        // GET: Therapy2/Edit/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Therapy therapy2 = db.Therapies.Find(id);
            if (therapy2 == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "NameSurName", therapy2.PatientId);
            ViewBag.PatientName = db.Patients.Find(therapy2.PatientId).NameSurName;
            ViewBag.Drugs = db.Drugs.ToList();

            return View(therapy2);
        }

        // POST: Therapy2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateFrom,DateTo,Diagnose,PatientId,DrugId")] Therapy therapy2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(therapy2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id = therapy2.PatientId });
            }
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "NameSurName", therapy2.PatientId);
            return View(therapy2);
        }

        // GET: Therapy2/Delete/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Therapy therapy2 = db.Therapies.Find(id);
            if (therapy2 == null)
            {
                return HttpNotFound();
            }
            return View(therapy2);
        }

        // POST: Therapy2/Delete/5
        [Authorize(Roles = "Doctor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Therapy therapy2 = db.Therapies.Find(id);
            db.Therapies.Remove(therapy2);
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
