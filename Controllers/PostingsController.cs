using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConstellationWebApp.Data;
using ConstellationWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ConstellationWebApp.Controllers
{
    [Authorize]
    public class PostingsController : Controller
    {
        private readonly ConstellationWebAppContext _context;

        public PostingsController(ConstellationWebAppContext context)
        {
            _context = context;
        }

        #region SimplePostingFunctions
        //Finds all users who have marked interest in the posting to show in the details page to mark themselves as interested or not. 
        //* could optimize this later by doing a single linq on the IntrestedCandidate table to select current user & posting to see if it has intrest*
        private void PopulateIntrestData(Posting posting)
        {
            var allUsers = _context.User;
            var intrestPostings = new HashSet<string>(posting.IntrestedCandidates.Select(c => c.UserID));
            var viewModel = new List<AssignedIntrestData>();
            foreach (var users in allUsers)
            {
                viewModel.Add(new AssignedIntrestData
                {
                    UserID = users.Id,
                    UserName = users.UserName,
                    Intrested = intrestPostings.Contains(users.Id)
                });
            }
            ViewData["intrestInPostings"] = viewModel;
        }

        //Finds all users who have starred posting to show in the details page to mark project as starred or not.  
        //* could optimize this later by just a single linq on the StarredPosting table to select current user & posting to see if it is starred*
        private void PopulateStarredPostingData(Posting posting)
        {
            var allUsers = _context.User;
            var starredPostings = new HashSet<string>(posting.StarredPostings.Select(c => c.UserID));
            var viewModel = new List<AssignedStarredPostingData>();
            foreach (var users in allUsers)
            {
                viewModel.Add(new AssignedStarredPostingData
                {
                    UserID = users.Id,
                    UserName = users.UserName,
                    Owned = starredPostings.Contains(users.Id)
                });
            }
            ViewData["UsersStarredPostings"] = viewModel;
        }

        //Gets the types that the Posting is part of  (i.e. Hourly, Internship, Remote)
        private void PopulateAssignedTypeData(Posting posting)
        {
            var allTypes = _context.PostingTypes;
            var PostingTypes = new HashSet<int>(posting.Posting_PostingTypes.Select(c => c.PostingTypeID));
            var viewModel = new List<AssignedTypeData>();
            foreach (var type in allTypes)
            {
                viewModel.Add(new AssignedTypeData
                {
                    PostingTypeID = type.PostingTypeID,
                    PostingTypeName = type.PostingTypeName,
                    Assigned = PostingTypes.Contains(type.PostingTypeID)
                });
            }
            ViewData["PostingTypes"] = viewModel;
        }

        //Updates the Posting types on edit
        private void UpdatePostingTypes(string[] selectedTypes, Posting postingToUpdate)
        {
            if (selectedTypes == null)
            {
                postingToUpdate.Posting_PostingTypes = new List<Posting_PostingType>();
                return;
            }

            var selectedTypesHS = new HashSet<string>(selectedTypes);
            var postingTypes = new HashSet<int>
                (postingToUpdate.Posting_PostingTypes.Select(c => c.PostingTypes.PostingTypeID));
            foreach (var type in _context.PostingTypes)
            {
                if (selectedTypesHS.Contains(type.PostingTypeID.ToString()))
                {
                    if (!postingTypes.Contains(type.PostingTypeID))
                    {
                        postingToUpdate.Posting_PostingTypes.Add(new Posting_PostingType { PostingID = postingToUpdate.PostingID, PostingTypeID = type.PostingTypeID });
                    }
                }
                else
                {
                    if (postingTypes.Contains(type.PostingTypeID))
                    {
                        Posting_PostingType typeToRemove = postingToUpdate.Posting_PostingTypes.FirstOrDefault(i => i.PostingTypeID == type.PostingTypeID);
                        _context.Remove(typeToRemove);
                    }
                }
            }
        }

        #endregion

        #region PostingsGets&PostsFunctions 
        // GET: Postings INDEX
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ViewModel();
            viewModel.Postings = await _context.Postings
                  .Include(i => i.Posting_PostingTypes)
                  .ThenInclude(i => i.PostingTypes)
                  .Include(i => i.PostingOwner)
                   .AsNoTracking()
                   .ToListAsync();

            return View(viewModel);
        }

        // GET: Postings/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posting = await _context.Postings
                  .Include(i => i.PostingOwner)
                  .Include(i => i.Posting_PostingTypes)
                  .ThenInclude(i => i.PostingTypes)
                  .Include(i => i.StarredPostings)
                  .ThenInclude(i => i.User)
                  .ThenInclude( i => i.Postings)
                  .ThenInclude( i =>i.ProjectPostings)
                  .Include(i => i.IntrestedCandidates)
                   .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PostingID == id);
            if (posting == null)
            {
                return NotFound();
            }
            PopulateStarredPostingData(posting);
            PopulateIntrestData(posting);
            return View(posting);
        }

        // GET: Postings/Create
        public IActionResult Create()
        {
            var posting = new Posting();
            posting.Posting_PostingTypes = new List<Posting_PostingType>();
            PopulateAssignedTypeData(posting);
            return View();
        }

        // POST: Postings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostingID,Description,PostingFor,PostingTitle, HidePosting, SharableToTeam")] Posting posting, string[] selectedTypes)
        {
            if (selectedTypes != null)
            {
                posting.Posting_PostingTypes = new List<Posting_PostingType>();
                foreach (var type in selectedTypes)
                {
                    var typesToAdd = new Posting_PostingType{ PostingID = posting.PostingID, PostingTypeID = int.Parse(type)};
                    posting.Posting_PostingTypes.Add(typesToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUser == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                var thisUser = await _context.User
               .FirstOrDefaultAsync(m => m.Id == currentUser);
                posting.PostingOwner = thisUser;
                _context.Add(posting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedTypeData(posting);
            return View(posting);
        }
        
        // GET: Postings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var posting = await _context.Postings
                .Include(i => i.Posting_PostingTypes)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PostingID == id);
                
              if (posting == null)
            {
                return NotFound();
            }
            PopulateAssignedTypeData(posting);
            return View(posting);
        }

        // POST: Postings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PostingID,Description,PostingFor,PostingTitle, HidePosting, SharableToTeam")] Posting posting, string[] selectedTypes)
        {
            if (id != posting.PostingID)
            {
                return NotFound();
            }

            var postingToUpdate = await _context.Postings
             .Include(i => i.Posting_PostingTypes)
              .ThenInclude(i => i.PostingTypes)
             .FirstOrDefaultAsync(s => s.PostingID == id);

            if (await TryUpdateModelAsync<Posting>(
                postingToUpdate,
                "",
                i => i.PostingTitle, i => i.PostingFor, i => i.Description, i => i.HidePosting, i => i.SharableToTeam))
            {
                UpdatePostingTypes(selectedTypes, postingToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostingExists(posting.PostingID))
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
            UpdatePostingTypes(selectedTypes, postingToUpdate);
            PopulateAssignedTypeData(posting);
            return View(posting);
        }
   
        // GET: Postings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posting = await _context.Postings
                .FirstOrDefaultAsync(m => m.PostingID == id);
            if (posting == null)
            {
                return NotFound();
            }

            return View(posting);
        }

        // POST: Postings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posting = await _context.Postings.FindAsync(id);
            _context.Postings.Remove(posting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostingExists(int id)
        {
            return _context.Postings.Any(e => e.PostingID == id);
        }
        #endregion
    }
}
