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
    public class StarredPostingsController : Controller
    {
        private readonly ConstellationWebAppContext _context;

        public StarredPostingsController(ConstellationWebAppContext context)
        {
            _context = context;
        }

        // GET: StarredPostings
        public async Task<IActionResult> Index()
        {

            var viewModel = new ViewModel();
             viewModel.StarredPostings =  _context.StarredPosting
                .Include(s => s.Posting)
                .ThenInclude(s => s.Posting_PostingTypes)
                .ThenInclude(s => s.PostingTypes)
                  .Include(s => s.Posting)
                  .ThenInclude( s => s.PostingOwner)
                .Include(s => s.User);
            return View(viewModel);
        }

        // POST: StarredPostings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int postingID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null && postingID != null)
            {
                StarredPosting starredPosting = new StarredPosting();
                starredPosting.UserID = currentUser;
                starredPosting.PostingID = postingID;
                _context.Add(starredPosting);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Postings");
        }

        // POST: StarredPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int postingID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            StarredPosting thisSP = ((_context.StarredPosting.Where(i => (i.PostingID == postingID) && (i.UserID == currentUser)).FirstOrDefault()));

            if (currentUser == thisSP.UserID)
            {
                _context.StarredPosting.Remove(thisSP);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StarredPostingExists(string id)
        {
            return _context.StarredPosting.Any(e => e.UserID == id);
        }
    }
}
