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
                .Include(i => i.User);
            return View(await constellationWebAppContext.ToListAsync());
        }

        // GET: IntrestedCandidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intrestedCandidate = await _context.IntrestedCandidate
                .Include(i => i.Posting)
                .Include(i => i.User)

                .FirstOrDefaultAsync(m => m.IntrestedCandidateID == id);
            if (intrestedCandidate == null)
            {
                return NotFound();
            }

            return View(intrestedCandidate);
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

        private bool IntrestedCandidateExists(int id)
        {
            return _context.IntrestedCandidate.Any(e => e.IntrestedCandidateID == id);
        }
    }
}
