using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConstellationWebApp.Data;
using ConstellationWebApp.Models;

namespace ConstellationWebApp.Controllers
{
    public class UserProjectsController : Controller
    {
        private readonly ConstellationWebAppContext _context;

        public UserProjectsController(ConstellationWebAppContext context)
        {
            _context = context;
        }

        // GET: UserProjects
        public async Task<IActionResult> Index()
        {
            var constellationWebAppContext = _context.UserProjects.Include(u => u.Project).Include(u => u.User);
            return View(await constellationWebAppContext.ToListAsync());
        }


        // GET: UserProjects/Create
        public IActionResult Create()
        {
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "Description");
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "Bio");
            return View();
        }

        // POST: UserProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,ProjectID")] UserProject userProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "Description", userProject.ProjectID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "Bio", userProject.UserID);
            return View(userProject);
        }

        // POST: UserProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int projectID, string collabId)
        {
            UserProject thisUP = ((_context.UserProjects.Where(i => (i.UserID == collabId) && (i.ProjectID == projectID)).FirstOrDefault()));
            _context.UserProjects.Remove(thisUP);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Projects");
        }

        private bool UserProjectExists(string id)
        {
            return _context.UserProjects.Any(e => e.UserID == id);
        }
    }
}
