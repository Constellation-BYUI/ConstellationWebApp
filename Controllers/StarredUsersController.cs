using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConstellationWebApp.Data;
using ConstellationWebApp.Models;
using System.Security.Claims;


namespace ConstellationWebApp.Controllers
{
    public class StarredUsersController : Controller
    {
        private readonly ConstellationWebAppContext _context;

        public StarredUsersController(ConstellationWebAppContext context)
        {
            _context = context;
        }

        // GET: StarredUsers
        public async Task<IActionResult> Index()
        {
            var constellationWebAppContext = _context.StarredUsers
                .Include(s => s.StarOwner)
                .Include(s => s.StarredPerson)
                .ThenInclude(s => s.ContactLinks);
            return View(await constellationWebAppContext.ToListAsync());
        }

        // POST: StarredUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id)
        {
            if (ModelState.IsValid)
            {
                var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUser != null && id != null)
                {
                    StarredUser starredUser = new StarredUser();
                    starredUser.StarredOwnerID = currentUser;
                    starredUser.UserStarredID = id;
                    _context.Add(starredUser);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index", "StarredUsers");
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            StarredUser thisSU = ((_context.StarredUsers.Where(i => (i.UserStarredID == id) && (i.StarredOwnerID == currentUser)).FirstOrDefault()));

            if (currentUser == thisSU.StarredOwnerID)
            {
                _context.StarredUsers.Remove(thisSU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StarredUserExists(int id)
        {
            return _context.StarredUsers.Any(e => e.StarredUserID == id);
        }
    }
}
