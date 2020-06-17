using ConstellationWebApp.Data;
using ConstellationWebApp.Models;
using ConstellationWebApp.Models.ViewModels;
using ConstellationWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConstellationWebApp.Controllers
{
    [Authorize]
    public class UserSkillsController : Controller
    {
        private readonly ConstellationWebAppContext _context;

        public UserSkillsController(ConstellationWebAppContext context)
        {
            _context = context;
        }

        //The user does not need to Create Skills, disciplines, of skillsDisciplines relationship. The only aspect needed for a user
        //is to be able to see the disciplines by section, then see the skills connect by the skill discipline. Those that are added to a user
        //should be listed and have option to update/remove, those not listed for a user should be listed and have option to add. 
        
            //index User 
            //profiles which the list of the Disciplines along with the skill selected
            //

        // GET: UserSkills 
        //This Page will only be displayed for the Logged in User to see, edit, etc.
        //The created data will be included in the user details & index
        //The search ability for the user will be based upon the discipline
        //?should this also include the ability to seach for just a SkillName
        public async Task<IActionResult> Index(string disciplineSearchString, string skillSearchString)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new ViewModel
            {
                SkillDisciplines = _context.SkillDisciplines,
                Skills = _context.Skills,
                UserSkills = _context.UserSkills.Where(i => i.UserID == currentUser),
                Users = _context.User.Where(i => i.Id == currentUser)
            };

            //Pull the discipline searched for, or pull the first discipline. 
            //This might be best implemented later if we include a discipline focus of the student and then
            //have that as the inital one shown.
            if (!String.IsNullOrEmpty(disciplineSearchString))
            {
                viewModel.Disciplines = _context.Disciplines.Where(s => s.DisciplineName.Contains(disciplineSearchString));
            }
            else
            {
                User userDiscipline = _context.User.Where(i => i.Id == currentUser).FirstOrDefault();
                viewModel.Disciplines = _context.Disciplines.Where(s => s.DisciplineName == userDiscipline.AreaOfDiscipline);
            }

            //Right now I am not going to have the ability to have a search string for skills, maybe later
            //if (!String.IsNullOrEmpty(skillSearchString))
            //{
            //    viewModel.Skills = _context.Skills.Where(s => s.SkillName.Contains(skillSearchString));
            //}

            return View(viewModel);
        }

        // GET: UserSkills/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSkill = await _context.UserSkills
                .Include(u => u.SkillLinks)
                .Include(u => u.Skills)
                .Include(u => u.Users)
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userSkill == null)
            {
                return NotFound();
            }

            return View(userSkill);
        }

        // GET: UserSkills/Create
        public IActionResult Create()
        {
            ViewData["SkillLinkID"] = new SelectList(_context.SkillLinks, "SkillLinkID", "SkillLinkID");
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillID");
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: UserSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SkillID,UserID,SkillLinkID")] UserSkill userSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkillLinkID"] = new SelectList(_context.SkillLinks, "SkillLinkID", "SkillLinkID", userSkill.SkillLinkID);
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillID", userSkill.SkillID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", userSkill.UserID);
            return View(userSkill);
        }

        // GET: UserSkills/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSkill = await _context.UserSkills.FindAsync(id);
            if (userSkill == null)
            {
                return NotFound();
            }
            ViewData["SkillLinkID"] = new SelectList(_context.SkillLinks, "SkillLinkID", "SkillLinkID", userSkill.SkillLinkID);
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillID", userSkill.SkillID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", userSkill.UserID);
            return View(userSkill);
        }

        // POST: UserSkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SkillID,UserID,SkillLinkID")] UserSkill userSkill)
        {
            if (id != userSkill.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSkillExists(userSkill.UserID))
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
            ViewData["SkillLinkID"] = new SelectList(_context.SkillLinks, "SkillLinkID", "SkillLinkID", userSkill.SkillLinkID);
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillID", userSkill.SkillID);
            ViewData["UserID"] = new SelectList(_context.User, "Id", "Id", userSkill.UserID);
            return View(userSkill);
        }

        // GET: UserSkills/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSkill = await _context.UserSkills
                .Include(u => u.SkillLinks)
                .Include(u => u.Skills)
                .Include(u => u.Users)
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userSkill == null)
            {
                return NotFound();
            }

            return View(userSkill);
        }

        // POST: UserSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userSkill = await _context.UserSkills.FindAsync(id);
            _context.UserSkills.Remove(userSkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSkillExists(string id)
        {
            return _context.UserSkills.Any(e => e.UserID == id);
        }
    }
}
