using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAppSender.Interfaces;
using WebAppSender.Models;
using WebAppSender.Settings;

namespace WebAppSender.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISenderTopicMessage _senderMessage;
        public HomeController(ISenderTopicMessage senderMessage)
        {
            _senderMessage = senderMessage;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(TopicMessage topicMessage)
        {
            ViewBag.ErrorMessage = null;
            try
            {
                await _senderMessage.SendMessage(topicMessage);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            if (ViewBag.ErrorMessage == null)
            {
                ViewBag.SuccessMessage = "object added to topic";
            }
            ModelState.Clear();
            return View(new TopicMessage());
        }

    }
}