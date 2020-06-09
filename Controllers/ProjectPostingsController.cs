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
    public class ProjectPostingsController : Controller
    {
        private readonly ConstellationWebAppContext _context;

        public ProjectPostingsController(ConstellationWebAppContext context)
        {
            _context = context;
        }

        // POST: ProjectPostings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int postingID, int projectID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ProjectPosting thisPP = (_context.ProjectPosting.Where(i => i.ProjectID == projectID && i.PostingID == postingID).FirstOrDefault());

            if (thisPP == null && currentUser != null)
            {
                ProjectPosting projectPosting = new ProjectPosting();
                projectPosting.ProjectID = projectID;
                projectPosting.PostingID = postingID;
                _context.Add(projectPosting);
                await _context.SaveChangesAsync();
            }
            var returnPath = "../Projects/Details/" + projectID.ToString();
            return Redirect(returnPath);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateManyProjectPostings(int[] postings,  int  projectID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserProject collaborator = (_context.UserProjects.Where(i => i.ProjectID == projectID && i.UserID == currentUser).FirstOrDefault());

            if (collaborator.UserID == currentUser)
            {
                foreach (var postingID in postings)
                {
                    ProjectPosting thisProjPost = (_context.ProjectPosting.Where(i => i.PostingID == postingID && i.ProjectID == projectID).FirstOrDefault());
                    if (thisProjPost == null)
                    {
                        ProjectPosting newProjPost = new ProjectPosting();
                        newProjPost.PostingID = postingID;
                        newProjPost.ProjectID = projectID;
                        _context.Add(newProjPost);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            var returnPath = "../Projects/Edit/" + projectID.ToString();
            return Redirect(returnPath);
        }

        // POST: ProjectPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int projectPostingID)
        {
            var projectPosting = await _context.ProjectPosting.FindAsync(projectPostingID);
            _context.ProjectPosting.Remove(projectPosting);
            await _context.SaveChangesAsync();
            var returnPath = "../Projects/Details/" + projectPosting.ProjectID.ToString();
            return Redirect(returnPath);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveManyPostings(int[] postings, int projectID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserProject collaborator = (_context.UserProjects.Where(i => i.ProjectID == projectID && i.UserID == currentUser).FirstOrDefault());
            if (collaborator.UserID == currentUser)
            {
                foreach (var postingID in postings)
                {
                    ProjectPosting thisProjPost = (_context.ProjectPosting.Where(i => i.PostingID == postingID && i.ProjectID == projectID).FirstOrDefault());
                    if (thisProjPost != null)
                    {
                     _context.ProjectPosting.Remove(thisProjPost);
                     await _context.SaveChangesAsync();
                    }
                }
            }
            var returnPath = "../Projects/Edit/" + projectID.ToString();
            return Redirect(returnPath);
        }

        private bool ProjectPostingExists(int id)
        {
            return _context.ProjectPosting.Any(e => e.ProjectPostingID == id);
        }
    }
}
