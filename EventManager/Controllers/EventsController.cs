﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManager.Data;
using EventManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace EventManager.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly MvcEventContext _context;

        public EventsController(MvcEventContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index(string searchString, string region, string keyWords)
        {
            var events = from m in _context.Event
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.Title.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(keyWords))
            {
                string[] words = keyWords.Split(' ');
                foreach(string word in words)
                {
                    events = events.Where(s => s.KeyWords.Contains(word));
                }
            }
            if (!String.IsNullOrEmpty(region))
            {
                events = events.Where(s => s.Region.Contains(region));
            }

            ViewData["UserName"] = User.Identity.Name;

            return View(await events.ToListAsync());
        }

        //GET: My
        public async Task<IActionResult> My()
        {
            var events = from m in _context.Event
                         select m;
            string UserName = User.Identity.Name;
            ViewData["UserName"] = User.Identity.Name;
            if (!String.IsNullOrEmpty(UserName))
            {
                events = events.Where(s => s.UserName == UserName);
            }
            return View(await events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["UserName"] = User.Identity.Name;
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["UserName"] = User.Identity.Name;
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Type,KeyWords,Region,StartDate,EndDate,Price")] Event @event)
        {
            ViewData["UserName"] = User.Identity.Name;
            if (ModelState.IsValid)
            {
                @event.UserName = User.Identity.Name;
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = User.Identity.Name;
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Type,KeyWords,Region,StartDate,EndDate,Price")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }
            ViewData["UserName"] = User.Identity.Name;
            if (ModelState.IsValid)
            {
                try
                {
                    @event.UserName = User.Identity.Name;
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = User.Identity.Name;
            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
