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
using Microsoft.AspNetCore.Authorization;

namespace ConstellationWebApp.Controllers
{
    [Authorize]
    public class StarredUsersController : Controller
    {
        private readonly ConstellationWebAppContext _context;

        public StarredUsersController(ConstellationWebAppContext context)
        {
            _context = context;
        }

        //The StarredUser entity gets the ID of the Starring user and the user being starred to easily 
        //display the listed starred users to the owner of this list. This user can add to this list 
        //from the details of the user page and can remove the user from the details or the starred list view itself.  
        //This only uses the Create Post, Delete Post, and Index Get from the StarredUsersController. 

        #region StarredUsersGets&PostsFunctions
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
            }
            var returnPath = "../Users/Details/" + id.ToString();
            return Redirect(returnPath);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            StarredUser thisSU = ((_context.StarredUsers.Where(i => (i.UserStarredID == id) && (i.StarredOwnerID == currentUser)).FirstOrDefault()));

            if (currentUser == thisSU.StarredOwnerID)
            {
                _context.StarredUsers.Remove(thisSU);
                await _context.SaveChangesAsync();
            }
            var returnPath = "../Users/Details/" + id.ToString();
            return Redirect(returnPath);
        }

        private bool StarredUserExists(int id)
        {
            return _context.StarredUsers.Any(e => e.StarredUserID == id);
        }

        #endregion
    }
}
