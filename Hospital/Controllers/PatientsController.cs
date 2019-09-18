using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;
using Microsoft.AspNet.Identity;

namespace Hospital.Controllers
{
    public class PatientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Patients
        public ActionResult Index()
        {
            var patients = db.Patients.ToList();
            if (User.IsInRole("Doctor"))
            {

                   var currentuser = User.Identity.GetUserId();
                var current = db.Users.FirstOrDefault(x => x.Id == currentuser);

                var doctor = db.Doctors.Single(user => user.Email == current.Email);

                var name = doctor.NameSurName;

                //   appointments = db.Appointments.Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefault(x => x.PatientId == patient.Id);

                //  patients = from patients1 in db.Patients where patient.Id == appointment.PatientId select appointment;
                var patients2 = db.Patients.ToList();
                patients = new List<Patient>();
                foreach(var p in patients2)
                {
                    List<Doctor> doctors = p.Doctors;
                    if (doctors.Contains(doctor))
                    {
                        patients.Add(p);
                    }
                }
                
            }
            return View(patients);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        [Authorize(Roles ="Administrator")]
        public ActionResult Create()
        {
            List<String> bloodtypes = new List<string> { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
           ViewBag.bloodtypes = bloodtypes;
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameSurName,Age,Gender,DateOfBirth,Address,PhoneNumber,Email,BloodType,Allergies,HistoryOfDiseases")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            List<String> bloodtypes = new List<string> { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
           ViewBag.bloodtypes = bloodtypes;
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameSurName,Age,Gender,DateOfBirth,Address,PhoneNumber,Email,BloodType,Allergies,HistoryOfDiseases")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteAjax(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
