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
    public class IntrestedCandidatesController : Controller
    {
        private readonly ConstellationWebAppContext _context;

        public IntrestedCandidatesController(ConstellationWebAppContext context)
        {
            _context = context;
        }

        // GET: IntrestedCandidates
        public async Task<IActionResult> Index()
        {
            var constellationWebAppContext = _context.IntrestedCandidate
                .Include(i => i.Posting)
                 .ThenInclude(i => i.Posting_PostingTypes)
                  .ThenInclude(i => i.PostingTypes)
                .Include(i => i.Posting)
                  .ThenInclude(i => i.PostingOwner)
                .Include(i => i.User)
                  .Include(i => i.Posting)
                  .ThenInclude(i => i.RecruiterPicks);

            List<RecruiterPicks> recuiterPicks = _context.RecruiterPicks.ToList();
            ViewBag.picks = recuiterPicks;

            return View(await constellationWebAppContext.ToListAsync());
        }


        // POST: IntrestedCandidate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int postingID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null && postingID != null)
            {
                IntrestedCandidate intrestedCandidate = new IntrestedCandidate();
                intrestedCandidate.UserID = currentUser;
                intrestedCandidate.PostingID = postingID;
                _context.Add(intrestedCandidate);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Postings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePick(int postingID, string recruiterID, string candidateID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null && currentUser == recruiterID)
            {
                RecruiterPicks recruiterPicks = new RecruiterPicks();
                recruiterPicks.RecuiterID = recruiterID;
                recruiterPicks.CandidateID = candidateID;
                recruiterPicks.PostingID = postingID;

                _context.Add(recruiterPicks);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "IntrestedCandidates");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePick(int recuiterPicksID)
        {
            RecruiterPicks thisRP = (_context.RecruiterPicks.Where(i => i.RecuiterPicksID == recuiterPicksID).FirstOrDefault());

            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (thisRP.RecuiterID == currentUser)
            {
                _context.RecruiterPicks.Remove(thisRP);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "IntrestedCandidates");
        }

        // POST: IntrestedCandidates/Delete/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int postingID, string returnUrl)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            IntrestedCandidate thisIC = ((_context.IntrestedCandidate.Where(i => (i.PostingID == postingID) && (i.UserID == currentUser)).FirstOrDefault()));
            string detailReturnID = postingID.ToString();
            if (currentUser == thisIC.UserID)
            {
                _context.IntrestedCandidate.Remove(thisIC);
                await _context.SaveChangesAsync();
                var returnPath = "https://localhost:44359/Postings/Details/" + postingID;

            }
            return RedirectToAction("Index", "Postings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePick(int recuiterPicksID)
        {
            RecruiterPicks thisRP = (_context.RecruiterPicks.Where(i => i.RecuiterPicksID == recuiterPicksID).FirstOrDefault());

            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (thisRP.RecuiterID == currentUser)
            {
                _context.RecruiterPicks.Remove(thisRP);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "IntrestedCandidates");
        }

        private bool IntrestedCandidateExists(int id)
        {
            return _context.IntrestedCandidate.Any(e => e.IntrestedCandidateID == id);
        }
    }
}
