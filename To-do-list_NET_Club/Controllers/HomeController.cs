using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using To_do_list_NET_Club.Models;
using To_do_list_NET_Club.Services;

namespace To_do_list_NET_Club.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger,IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }
        
        

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public IActionResult SendEmail()

        {

            var toAddress = HttpContext.Request.Form["toAddress"].FirstOrDefault();

            var subject = HttpContext.Request.Form["subject"].FirstOrDefault();

            var body = HttpContext.Request.Form["body"].FirstOrDefault();



            _emailSender.Send(toAddress, subject, body);



            return RedirectToAction("Index");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
