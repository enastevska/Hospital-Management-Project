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
using Microsoft.AspNet.Identity.Owin;
using Vereyon.Web;

namespace Hospital.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _usermanager;
       

   /*     public AppointmentsController(UserManager<ApplicationUser> userManager)
        {
            _usermanager = userManager;
        }
*/
        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.Doctor).Include(a => a.Patient);
           
            if (User.IsInRole("Patient"))
            {
                var currentuser = User.Identity.GetUserId();
                var current = db.Users.FirstOrDefault(x => x.Id == currentuser);

                var patient = db.Patients.Single(user => user.Email == current.Email);


               
                //   appointments = db.Appointments.Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefault(x => x.PatientId == patient.Id);

                appointments = from appointment in db.Appointments.Include(a => a.Doctor).Include(a => a.Patient) where patient.Id == appointment.PatientId select appointment;

                //  appointments = db.Appointments.FirstOrDefault(x => x.PatientId == patient.Id);


            }

            if (User.IsInRole("Doctor"))
            {
                var currentuser = User.Identity.GetUserId();
                var current = db.Users.FirstOrDefault(x => x.Id == currentuser);

                var doctor = db.Doctors.Single(user => user.Email == current.Email);


                appointments = from appointment in db.Appointments.Include(a => a.Doctor).Include(a => a.Patient) where doctor.Id == appointment.DoctorId select appointment;

            }


            return View(appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Include(lk=>lk.Doctor).Include(lk => lk.Patient).Single(x=>x.Id==id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "Patient")]
        public ActionResult Create(int id)
        {
            var doctor = db.Doctors.Find(id);

           
            ViewBag.doctorId = id;
            ViewBag.doctor = doctor.NameSurName;
          //  ViewBag.PatientId = new SelectList(db.Patients, "Id", "NameSurName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Patient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,DoctorId,FromTime,HasOccured")] Appointment appointment)
        {
           var manager= Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            
            var currentuser = User.Identity.GetUserId();
            var current = db.Users.FirstOrDefault(x => x.Id == currentuser);
      
            Patient patient = db.Patients.Single(user=> user.Email==current.Email);

            List<Appointment> times = new List<Appointment>();
            List<int> doctors = new List<int>();
            List<string> dates = new List<string>();
            var appointments = db.Appointments.ToList();
            
            foreach (var app in appointments)
            {
             if(app.Date.ToShortDateString()== appointment.Date.ToShortDateString() && app.FromTime == appointment.FromTime && app.DoctorId == appointment.DoctorId)
                {
                    FlashMessage.Warning("The appointment time you selected is taken. Please select another time.");
                    return RedirectToAction("Create", new { id = appointment.DoctorId });

                }
            }
           
           
           

            if (ModelState.IsValid)
            {
                appointment.PatientId = patient.Id;
               
                db.Appointments.Add(appointment);
                int doctorid = int.Parse(Request.Form["DoctorId"]);
                Doctor doctor = db.Doctors.Find(doctorid);

              db.Patients.Find(patient.Id).Doctors.Add(doctor);
                db.Doctors.Find(doctor.Id).Patients.Add(patient);



                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "NameSurName", appointment.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "NameSurName", appointment.PatientId);
            return View("Index");
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "NameSurName", appointment.DoctorId);
            ViewBag.Date = appointment.Date.ToShortDateString();
            ViewBag.PatientName = db.Patients.Find(appointment.PatientId).NameSurName;
            ViewBag.Patient = appointment.PatientId;
            ViewBag.DoctorName = db.Doctors.Find(appointment.DoctorId).NameSurName;
            ViewBag.Doctor = appointment.DoctorId;
            ViewBag.FromTime = appointment.FromTime.ToString();
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "NameSurName", appointment.PatientId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,PatientId,DoctorId,FromTime,HasOccured")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "NameSurName", appointment.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "NameSurName", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles = "Doctor,Patient")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [Authorize(Roles = "Doctor, Patient")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Doctor,Patient")]
        public ActionResult DeleteAjax(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
