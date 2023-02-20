using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MGPhoto.Models;
using Microsoft.AspNetCore.Authorization;

namespace MGPhoto.Controllers
{
    public class ReplyMessagesController : Controller
    {
        private readonly MGPhotoDbContext _context;

        public ReplyMessagesController(MGPhotoDbContext context)
        {
            _context = context;
        }

        // GET: ReplyMessages
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
              return _context.ReplyMessages != null ? 
                          View(await _context.ReplyMessages.ToListAsync()) :
                          Problem("Entity set 'MGPhotoDbContext.ReplyMessages'  is null.");
        }
        [Authorize]
        public async Task<IActionResult> Inbox()
        {
            return _context.ReplyMessages != null ?
                        View(await _context.ReplyMessages.ToListAsync()) :
                        Problem("Entity set 'MGPhotoDbContext.ReplyMessages'  is null.");
        }

        // GET: ReplyMessages/Details/5
        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReplyMessages == null)
            {
                return NotFound();
            }

            var replyMessage = await _context.ReplyMessages
                .FirstOrDefaultAsync(m => m.ReplyId == id);
            if (replyMessage == null)
            {
                return NotFound();
            }

            return View(replyMessage);
        }

        // GET: ReplyMessages/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReplyMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ReplyId,Message,Sended,Recipient")] ReplyMessage replyMessage)
        {
            if (ModelState.IsValid)
            {
                replyMessage.Sended = DateTime.Now;
                _context.Add(replyMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Inbox));
            }
            return View(replyMessage);
        }

        // GET: ReplyMessages/Edit/5
        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReplyMessages == null)
            {
                return NotFound();
            }

            var replyMessage = await _context.ReplyMessages.FindAsync(id);
            if (replyMessage == null)
            {
                return NotFound();
            }
            return View(replyMessage);
        }

        // POST: ReplyMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> Edit(int id, [Bind("ReplyId,Message,Sended,Recipient")] ReplyMessage replyMessage)
        {
            if (id != replyMessage.ReplyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(replyMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplyMessageExists(replyMessage.ReplyId))
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
            return View(replyMessage);
        }

        // GET: ReplyMessages/Delete/5
        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReplyMessages == null)
            {
                return NotFound();
            }

            var replyMessage = await _context.ReplyMessages
                .FirstOrDefaultAsync(m => m.ReplyId == id);
            if (replyMessage == null)
            {
                return NotFound();
            }

            return View(replyMessage);
        }

        // POST: ReplyMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReplyMessages == null)
            {
                return Problem("Entity set 'MGPhotoDbContext.ReplyMessages'  is null.");
            }
            var replyMessage = await _context.ReplyMessages.FindAsync(id);
            if (replyMessage != null)
            {
                _context.ReplyMessages.Remove(replyMessage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Inbox));
        }

        private bool ReplyMessageExists(int id)
        {
          return (_context.ReplyMessages?.Any(e => e.ReplyId == id)).GetValueOrDefault();
        }
    }
}
