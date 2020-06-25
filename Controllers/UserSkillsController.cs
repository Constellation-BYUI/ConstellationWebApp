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
        public async Task<IActionResult> Index(string disciplineSearchString)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new ViewModel
            {
                SkillDisciplines = _context.SkillDisciplines,
                Skills = _context.Skills,
                Disciplines = _context.Disciplines,
                UserSkills = _context.UserSkills.Where(i => i.UserID == currentUser),
                Users = _context.User.Where(i => i.Id == currentUser),
                SkillLinks = _context.SkillLinks.Where(i => i.SkillLinkOwner == currentUser)
            };

            //Pull the discipline searched for, or pull the first discipline. 
            //This might be best implemented later if we include a discipline focus of the student and then
            //have that as the inital one shown.
            if (!String.IsNullOrEmpty(disciplineSearchString))
            {
                viewModel.currentDiscipline = _context.Disciplines.Where(s => s.DisciplineName.Contains(disciplineSearchString)).FirstOrDefault();               
            }
            else
            {
                User user = _context.User.Where(i => i.Id == currentUser).FirstOrDefault();

                if (user.AreaOfDiscipline != null)              
                {
                    viewModel.currentDiscipline = _context.Disciplines.Where(s => s.DisciplineName == user.AreaOfDiscipline).FirstOrDefault();
                }
                else
                {
                    viewModel.currentDiscipline = _context.Disciplines.Where(i => i.DisciplineID == 1).FirstOrDefault();
                }                
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateManyUserSkills(int[] skills, string userID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUser == userID)
            {
                foreach (var skill in skills)
                {
                    UserSkill userSkill = (_context.UserSkills.Where(i => i.UserID == userID && i.SkillID == skill).FirstOrDefault());
                    if (userSkill == null)
                    {
                        UserSkill thisUS = new UserSkill();
                        thisUS.UserID = currentUser;
                        thisUS.SkillID = skill;
                        _context.Add(thisUS);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            var returnPath = "../UserSkills/";
            return Redirect(returnPath);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveManyUserSkills(int[] skills, string userID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser == userID)
            {
                foreach (var skill in skills)
                {
                    UserSkill userSkill = (_context.UserSkills.Where(i => i.UserID == userID && i.SkillID == skill).FirstOrDefault());
                    if (userSkill != null)
                    {
                        _context.Remove(userSkill);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            var returnPath = "../UserSkills/";
            return Redirect(returnPath);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSkillLink(int[] skills, string linkLabel, string linkUrl)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var slID = 0;
            //must create the skill link            
            if (linkLabel != null && linkUrl != null)
            {               
                    SkillLink thisSL = new SkillLink();
                    thisSL.SkillLinkOwner = currentUser;
                    thisSL.SkillLinkUrl = linkUrl;
                    thisSL.SkilLinkLabel = linkLabel;
                    _context.Add(thisSL);
                    await _context.SaveChangesAsync();
                //create the UserSkillLink join relationship
                slID = thisSL.SkillLinkID;

                foreach (var skill in skills)
                {
                    try
                    {      
                        UserSkillLink ThisUSL = new UserSkillLink();
                        ThisUSL.LinkID = slID;
                        ThisUSL.UserSkillID = skill;
                        _context.Add(ThisUSL);
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }            
            var returnPath = "../UserSkills/";
            return Redirect(returnPath);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSkillLink(int link)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                var skillLink = _context.SkillLinks.Where(i => i.SkillLinkID == link).FirstOrDefault();

                var userSkillLinks = _context.UserSkillLinks.Where(i => i.LinkID == link);

                foreach (var usl in userSkillLinks)
                {
                    _context.Remove(usl);
                    await _context.SaveChangesAsync();
                }

                _context.Remove(skillLink);
                await _context.SaveChangesAsync();
            }

            var returnPath = "../UserSkills/";
            return Redirect(returnPath);
        }

            private bool UserSkillExists(string id)
        {
            return _context.UserSkills.Any(e => e.UserID == id);
        }
    }
}
