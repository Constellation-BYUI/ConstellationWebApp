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
    public class StarredProjectsController : Controller
    {
        private readonly ConstellationWebAppContext _context;

        public StarredProjectsController(ConstellationWebAppContext context)
        {
            _context = context;
        }

        #region StarredProjectsGets&PostsFunctions
        // GET: StarredProjects
        public async Task<IActionResult> Index()
        {
            var viewModel = new ViewModel();
            viewModel.Projects = await _context.Projects
                   .Include(i => i.UserProjects)
                     .ThenInclude(i => i.User)
                      .Include(i => i.ProjectLinks)
                   .AsNoTracking()
                   .OrderBy(i => i.CreationDate)
                   .ToListAsync();

            viewModel.StarredProjects = await _context.StarredProjects.ToListAsync();


            return View(viewModel);

        }

        // POST: StarredProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int projectID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null && projectID != null)
            {
                StarredProject starredProject = new StarredProject();
                starredProject.UserID = currentUser;
                starredProject.ProjectID = projectID;
                _context.Add(starredProject);
                await _context.SaveChangesAsync();
            }
            var returnPath = "../Projects/Details/" + projectID.ToString();
            return Redirect(returnPath);
        }

        // POST: StarredPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int projectID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            StarredProject thisSP = ((_context.StarredProjects.Where(i => (i.ProjectID == projectID) && (i.UserID == currentUser)).FirstOrDefault()));
            if (currentUser == thisSP.UserID)
            {
                _context.StarredProjects.Remove(thisSP);
                await _context.SaveChangesAsync();
            }
            var returnPath = "../StarredProjects/";
            return Redirect(returnPath);
        }
        private bool StarredProjectExists(int id)
        {
            return _context.StarredProjects.Any(e => e.StarredProjectID == id);
        }
        #endregion
    }
}
