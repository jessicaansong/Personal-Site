using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstApp.Models;

namespace MyFirstApp.Controllers
{
     [RequireHttps]
    public class ContactController : Controller
    {
        // Get Contact
        public ActionResult Index()
        {
            ViewBag.MessageSent = TempData["MessageSent"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactMessage contactForm)
        {
            if (!ModelState.IsValid) return View(contactForm);
            var emailer = new EmailService();

            var mail = new IdentityMessage
            {
                //Subject = contactForm.Subject,
                Destination = ConfigurationManager.AppSettings["ContactEmail"],
                Body = "You have received a new contact submission form" + contactForm.contactName +
            "( " + contactForm.FromEmail + ") with the following contents/br> /br>" +
            contactForm.Message

            };

            emailer.SendAsync(mail);

            TempData["MessageSent"] = "Your message has be delivered successfully!";

            return RedirectToAction("Index");
        }

    }

}

   