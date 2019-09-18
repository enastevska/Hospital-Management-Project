using Hospital.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Details(int? id)
        {

            if (User.IsInRole("Doctor"))
            {

                var currentuser = User.Identity.GetUserId();
                var current = db.Users.FirstOrDefault(x => x.Id == currentuser);

                var doctor = db.Doctors.Single(user => user.Email == current.Email);

                var iddoctor = doctor.Id;
              
                return RedirectToAction("Details", "Doctors", new { id = iddoctor });

            }
            else if (User.IsInRole("Patient"))
            {

                var currentuser = User.Identity.GetUserId();
                var current = db.Users.FirstOrDefault(x => x.Id == currentuser);

                var patient = db.Patients.Single(user => user.Email == current.Email);

                var idpatient = patient.Id;

                return RedirectToAction("Details", "Patients", new { id = idpatient });

            }

            else
                return View("Index");
        }
    }
}