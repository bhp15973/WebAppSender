using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppSender.Data;
using WebAppSender.Models;

namespace WebAppSender.Controllers
{
    public class QueueMessagesController : Controller
    {
        private readonly DatabaseContext _context;

        public QueueMessagesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: QueueMessages
        public async Task<IActionResult> Index()
        {
            return View(await _context.QueueMessages.ToListAsync());
        }

        // GET: QueueMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queueMessage = await _context.QueueMessages
                .SingleOrDefaultAsync(m => m.ID == id);
            if (queueMessage == null)
            {
                return NotFound();
            }

            return View(queueMessage);
        }

        // GET: QueueMessages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QueueMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Content")] QueueMessage queueMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(queueMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(queueMessage);
        }

        // GET: QueueMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queueMessage = await _context.QueueMessages.SingleOrDefaultAsync(m => m.ID == id);
            if (queueMessage == null)
            {
                return NotFound();
            }
            return View(queueMessage);
        }

        // POST: QueueMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Content")] QueueMessage queueMessage)
        {
            if (id != queueMessage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queueMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueueMessageExists(queueMessage.ID))
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
            return View(queueMessage);
        }

        // GET: QueueMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queueMessage = await _context.QueueMessages
                .SingleOrDefaultAsync(m => m.ID == id);
            if (queueMessage == null)
            {
                return NotFound();
            }

            return View(queueMessage);
        }

        // POST: QueueMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queueMessage = await _context.QueueMessages.SingleOrDefaultAsync(m => m.ID == id);
            _context.QueueMessages.Remove(queueMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueueMessageExists(int id)
        {
            return _context.QueueMessages.Any(e => e.ID == id);
        }
    }
}
