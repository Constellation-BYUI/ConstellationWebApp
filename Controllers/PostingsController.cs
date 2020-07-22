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
        public async Task<IActionResult> Index(string titleSearch, string typeSearch, string postingBySearch)
        {
            var viewModel = new ViewModel();

            if(titleSearch != null)
            {
                viewModel.Postings = await _context.Postings.Where(i => i.HidePosting == false && i.PostingTitle.Contains(titleSearch))
                     .Include(i => i.Posting_PostingTypes)
                     .ThenInclude(i => i.PostingTypes)
                     .Include(i => i.PostingOwner)
                      .AsNoTracking()
                      .ToListAsync();
                viewModel.PostingTypes = _context.PostingTypes;
                return View(viewModel);
            }
            else if(typeSearch != null)
            {
                PostingType postingID = _context.PostingTypes.Where(i => i.PostingTypeName == typeSearch).FirstOrDefault();
                viewModel.Postings = await _context.Postings.Where(i => i.HidePosting == false && i.Posting_PostingTypes.Any(e => e.PostingTypeID == postingID.PostingTypeID))
                   .Include(i => i.Posting_PostingTypes)
                  .ThenInclude(i => i.PostingTypes)
                  .Include(i => i.PostingOwner)
                   .AsNoTracking()
                   .ToListAsync();
                viewModel.PostingTypes = _context.PostingTypes;
                return View(viewModel);
            }
            else if (postingBySearch != null)
            {
                viewModel.Postings = await _context.Postings.Where(i => i.HidePosting == false && (i.PostingOwner.FirstName.Contains(postingBySearch) || i.PostingOwner.LastName.Contains(postingBySearch) || i.PostingOwner.UserName.Contains(postingBySearch)))
                   .Include(i => i.Posting_PostingTypes)
                  .ThenInclude(i => i.PostingTypes)
                  .Include(i => i.PostingOwner)
                   .AsNoTracking()
                   .ToListAsync();
                viewModel.PostingTypes = _context.PostingTypes;
                return View(viewModel);
            }
            else
            {
                viewModel.Postings = await _context.Postings.Where(i => i.HidePosting == false)
                  .Include(i => i.Posting_PostingTypes)
                  .ThenInclude(i => i.PostingTypes)
                  .Include(i => i.PostingOwner)
                   .AsNoTracking()
                   .ToListAsync();
                viewModel.PostingTypes = _context.PostingTypes;
                return View(viewModel);
            }            
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

            List<Skill> Skills = _context.Skills.ToList();
            ViewBag.Skills = Skills;

            List<Discipline> disciplines = _context.Disciplines.ToList();
            ViewBag.disciplines = disciplines;

            List<SkillDiscipline> skillDisciplines = _context.SkillDisciplines.ToList();
            ViewBag.skillDisciplines = skillDisciplines;

            List<PostingSkills> postingSkills = _context.PostingSkills.Where(i => i.PostingID == id).ToList();
            ViewBag.postingSkills = postingSkills;


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
        public async Task<IActionResult> Create([Bind("PostingID,Description,PostingFor,PostingTitle, HidePosting, SharableToTeam, ApplicationURL, ApplicationDeadline, Location")] Posting posting, string[] selectedTypes)
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

                var postingURL = posting.ApplicationURL.ToLower();
                if (!postingURL.Contains("constellation.citwdd.net") && !(postingURL.Contains("http://") || postingURL.Contains("https://")))
                {
                    posting.ApplicationURL = "http://" + posting.ApplicationURL;
                    _context.Add(posting);

                }

                _context.Add(posting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Postings", new { id = posting.PostingID });
            }
            PopulateAssignedTypeData(posting);
            return RedirectToAction("Edit", "Postings", new {id = posting.PostingID });
        }
        
        // GET: Postings/Edit/5
        public async Task<IActionResult> Edit(int? id, string disciplineSearchString)
        {
            if (id == null)
            {
                return NotFound();
            }
            var posting = await _context.Postings
                .Include(i=> i.PostingOwner)
                .Include(i => i.Posting_PostingTypes)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PostingID == id);
                
              if (posting == null)
            {
                return NotFound();
            }
            PopulateAssignedTypeData(posting);

            List<Skill> Skills = _context.Skills.ToList();
            ViewBag.Skills = Skills;

            if (disciplineSearchString != null)
            {
                Discipline currentDiscipline = _context.Disciplines.Where(i => i.DisciplineName == disciplineSearchString).FirstOrDefault();
                ViewBag.currentDiscipline = currentDiscipline;

            }
            else
            {
                Discipline currentDiscipline = _context.Disciplines.FirstOrDefault();
                ViewBag.currentDiscipline = currentDiscipline;

            }

            List<Discipline> disciplines = _context.Disciplines.ToList();
            ViewBag.disciplines = disciplines;

            List<SkillDiscipline> skillDisciplines = _context.SkillDisciplines.ToList();
            ViewBag.skillDisciplines = skillDisciplines;

            List<PostingSkills> postingSkills = _context.PostingSkills.Where(i => i.PostingID == id).ToList();
            ViewBag.postingSkills = postingSkills;


            return View(posting);
        }


        // POST: Postings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PostingID,Description,PostingFor,PostingTitle, HidePosting, SharableToTeam, ApplicationURL, ApplicationDeadline, Location")] Posting posting, string[] selectedTypes)
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
                i => i.PostingTitle, i => i.PostingFor, i => i.Description, i => i.HidePosting, i => i.SharableToTeam, i => i.ApplicationURL, i => i.ApplicationDeadline, i => i.Location))
            {
                UpdatePostingTypes(selectedTypes, postingToUpdate);

                try
                {
                    await _context.SaveChangesAsync();

                    var postingURL = postingToUpdate.ApplicationURL.ToLower();
                    if (!postingURL.Contains("constellation.citwdd.net") && !(postingURL.Contains("http://") || postingURL.Contains("https://")))
                    {
                        postingToUpdate.ApplicationURL = "http://" + postingURL;
                    }
                    _context.Update(postingToUpdate);
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
                .Include(i => i.PostingOwner)
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
            await RemoveRelationalDataAsync(posting);       
            _context.Postings.Remove(posting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task RemoveRelationalDataAsync(Posting posting)
        {
            //delete posting skill
            IEnumerable<PostingSkills> postingSkills =  _context.PostingSkills.Where(i => i.PostingID == posting.PostingID); 
            foreach(var ps in postingSkills)
            {
                _context.PostingSkills.Remove(ps);
            }
            await _context.SaveChangesAsync();


            //delete intrested candidates
            IEnumerable<IntrestedCandidate> intrestedCandidate = _context.IntrestedCandidate.Where(i => i.PostingID == posting.PostingID);
            foreach (var ps in intrestedCandidate)
            {
                _context.IntrestedCandidate.Remove(ps);
            }
            await _context.SaveChangesAsync();

            //delete recuiters pick
            IEnumerable<RecruiterPicks> recruiterPicks = _context.RecruiterPicks.Where(i => i.PostingID == posting.PostingID);
            foreach (var ps in recruiterPicks)
            {
                _context.RecruiterPicks.Remove(ps);
            }
            await _context.SaveChangesAsync();

            //delete starred postings
            IEnumerable<StarredPosting> starredPosting = _context.StarredPosting.Where(i => i.PostingID == posting.PostingID);
            foreach (var ps in starredPosting)
            {
                _context.StarredPosting.Remove(ps);
            }
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateManyPostingSkills(int[] skills, int postingID, string disciplineSearchString, int priorityValue)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUser != null)
            {
                foreach (var skill in skills)
                {
                    PostingSkills postingSkills = (_context.PostingSkills.Where(i => i.PostingID == postingID && i.SkillID == skill).FirstOrDefault());
                    if (postingSkills == null)
                    {
                        PostingSkills thisPS = new PostingSkills();
                        thisPS.PostingID = postingID;
                        thisPS.SkillID = skill;
                        thisPS.PriorityLevel = priorityValue;
                        _context.Add(thisPS);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Edit", "Postings", new { disciplineSearchString = disciplineSearchString, id = postingID });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveManyPostingSkills(int[] skills, int postingID, string disciplineSearchString)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                foreach (var skill in skills)
                {
                    PostingSkills postingSkills = (_context.PostingSkills.Where(i => i.PostingID == postingID && i.SkillID == skill).FirstOrDefault());
                    if (postingSkills != null)
                    {
                        _context.Remove(postingSkills);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Edit", "Postings", new { disciplineSearchString = disciplineSearchString, id = postingID });

        }
        private bool PostingExists(int id)
        {
            return _context.Postings.Any(e => e.PostingID == id);
        }
        #endregion
    }
}
