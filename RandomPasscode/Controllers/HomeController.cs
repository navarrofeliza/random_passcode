using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Count") == null)
            {
                HttpContext.Session.SetInt32("Count", 0);
            }
            ViewBag.Count = HttpContext.Session.GetInt32("Count");
            ViewBag.String = HttpContext.Session.GetInt32("random");
            return View();
        }
        [HttpPost]
        [Route("redirect")]
        public IActionResult redirect()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] randomString = new char[14];
            Random rand = new Random();
            for (int i = 0; i < randomString.Length; i++)
            {
                randomString[i] = chars[rand.Next(chars.Length)];
            }
            string newString = new String(randomString);
            HttpContext.Session.SetString("random", newString);
            HttpContext.Session.SetInt32("Count", (int)HttpContext.Session.GetInt32("Count") + 1);
            return RedirectToAction("Index");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}