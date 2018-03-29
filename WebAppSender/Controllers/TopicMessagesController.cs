using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppSender.Data;
using WebAppSender.Interfaces;
using WebAppSender.Models;

namespace WebAppSender.Controllers
{
    public class TopicMessagesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly ISenderTopicMessage _senderTopicMessage;

        public TopicMessagesController(DatabaseContext context, ISenderTopicMessage senderTopicMessage)
        {
            _context = context;
            _senderTopicMessage = senderTopicMessage;
        }


        // GET: TopicMessages
        public async Task<IActionResult> Index()
        {
            return View(await _context.TopicMessages.ToListAsync());
        }

        // GET: TopicMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicMessage = await _context.TopicMessages
                .SingleOrDefaultAsync(m => m.ID == id);
            if (topicMessage == null)
            {
                return NotFound();
            }

            return View(topicMessage);
        }

        // GET: TopicMessages/Create
        public IActionResult Create()
        {
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TopicMessage topicMessage)
        {
            ViewBag.ErrorMessage = null;
            try
            {
                await _senderTopicMessage.SendMessage(topicMessage);
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
        // GET: TopicMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicMessage = await _context.TopicMessages.SingleOrDefaultAsync(m => m.ID == id);
            if (topicMessage == null)
            {
                return NotFound();
            }
            return View(topicMessage);
        }

        // POST: TopicMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Subscription,Title,Content")] TopicMessage topicMessage)
        {
            if (id != topicMessage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topicMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicMessageExists(topicMessage.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(topicMessage);
        }

        // GET: TopicMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicMessage = await _context.TopicMessages
                .SingleOrDefaultAsync(m => m.ID == id);
            if (topicMessage == null)
            {
                return NotFound();
            }

            return View(topicMessage);
        }

        // POST: TopicMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topicMessage = await _context.TopicMessages.SingleOrDefaultAsync(m => m.ID == id);
            _context.TopicMessages.Remove(topicMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicMessageExists(int id)
        {
            return _context.TopicMessages.Any(e => e.ID == id);
        }
    }
}
