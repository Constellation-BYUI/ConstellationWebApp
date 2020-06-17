using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConstellationWebApp.Data;
using ConstellationWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ConstellationWebApp.ViewModels;
using System.Dynamic;
using ConstellationWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace ConstellationWebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ConstellationWebAppContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public UsersController(ConstellationWebAppContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;

        }

        //Region of all Methods that perform a simple function
        #region SimpleUserFunctions
        private string UploadResume(UserCreateViewModel model)
        {
            string resumeFileName = null;
            if (!(Path.GetExtension(model.ResumeUpload.FileName) == ".pdf"))
            {
                model.ResumeUpload = null;
            }
            if (model.ResumeUpload != null)
            {
                resumeFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(System.IO.Path.GetFileName(model.ResumeUpload.FileName));
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath + "/Resumes");
                string filePath = Path.Combine(uploadsFolder, resumeFileName);
                model.ResumeUpload.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return (resumeFileName);
        }

        private void DeleteResume(UserEditViewModel model)
        {
            if (model.OldResumePath != null)
            {
                string filePath = Path.Combine(hostingEnvironment.WebRootPath, "image", model.OldResumePath);
                System.IO.File.Delete(filePath);
            }
        }

        private void CreateUserLinks(string[] createdLinkLabels, string[] createdLinkUrls, User newUser)
        {
            if (createdLinkLabels != null)
            {
                for (var i = 0; i < createdLinkLabels.Length; i++)
                {
                    ContactLink newContact = new ContactLink
                    {
                        ContactLinkLabel = createdLinkLabels[i],
                        ContactLinkUrl = createdLinkUrls[i],
                        Users = newUser
                    };
                    _context.Add(newContact);
                }
            }
        }

        private string ValidateImagePath(UserCreateViewModel model)
        {
            string uniqueFileName = null;
            if (!(model.Photo.ContentType.Contains("image")))
            {
                model.Photo = null;
            }
            if (model.Photo != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + System.IO.Path.GetFileName(model.Photo.FileName);
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath + "/image");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return (uniqueFileName);
        }

        private User ViewModeltoUser(UserCreateViewModel model, string uniqueFileName, string resumeFileName)
        {
            User newUser = new User
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Bio = model.Bio,
                Seeking = model.Seeking,
                PhotoPath = uniqueFileName,
                ResumePhotoPath = resumeFileName,
                displayMyProfile = model.displayMyProfile,
                AreaOfDiscipline =  model.AreaOfDiscipline
            };
            return (newUser);
        }

        private UserEditViewModel userToEditViewModel(User userModel)
        {
            UserEditViewModel viewModel = new UserEditViewModel
            {
                UserID = userModel.Id,
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Bio = userModel.Bio,
                Seeking = userModel.Seeking,
                OldPhotoPath = userModel.PhotoPath,
                OldResumePath = userModel.ResumePhotoPath,
                PhotoPath = userModel.PhotoPath,
                ResumePhotoPath = userModel.ResumePhotoPath,
                displayMyProfile = userModel.displayMyProfile,
                thisUser = userModel,
                ContactLinks = userModel.ContactLinks,
                AreaOfDiscipline = userModel.AreaOfDiscipline
            };
            return (viewModel);
        }



        private void DeletePhoto(UserEditViewModel model)
        {
            if (model.OldPhotoPath != null)
            {
                string filePath = Path.Combine(hostingEnvironment.WebRootPath, "image", model.OldPhotoPath);
                System.IO.File.Delete(filePath);
            }
        }

        private void RemoveAllContactLinks(string id)
        {
            var contactLinks = from m in _context.ContactLinks
                               select m;

            var linksId = contactLinks.Where(s => s.Users.Id == id);

            foreach (var link in linksId)
            {
                _context.ContactLinks.Remove(link);
            }
        } 

        #endregion


        // Post & Get Functions (includeing the starredUsers index Get)
        #region UserGets&PostsFunctions
        // GET: Users/Index
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ViewModel();
            viewModel.Users = await _context.User
                  .Include(i => i.ContactLinks)
                   .AsNoTracking()
                   .OrderBy(i => i.Id)
                   .ToListAsync();
            viewModel.Disciplines = _context.Disciplines;
            return View(viewModel);
        }

        // GET: User/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
          .Include(s => s.Recuiter)
          .Include(s => s.ContactLinks)
          .Include(s => s.StarredUsers)
          .Include(s => s.StarredOwner)
          .Include(s => s.UserProjects)
          .ThenInclude(s => s.Project)
          .ThenInclude(s => s.ProjectLinks)
          .AsNoTracking()
          .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }


            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<ConstellationWebApp.Models.UserProject> collaboratorsList = new List<UserProject>();
            List<UserProject> userProjectList = _context.UserProjects.ToList();
            List<User> usersList = _context.User.ToList();
            List<Posting> currentUserPostings = _context.Postings.Where(i => i.PostingOwner.Id == currentUser).ToList();
            List<RecruiterPicks> recruitersData = _context.RecruiterPicks.Where(i => i.RecuiterID == currentUser).ToList();

            ViewBag.collaborators = userProjectList;
            ViewBag.allUsers = usersList;
            ViewBag.currentUserPostings = currentUserPostings;
            ViewBag.recruitersData = recruitersData;

            List<StarredUser> thisSU = _context.StarredUsers.ToList();
            ViewBag.StarredUsers = thisSU;
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var viewModel = new UserCreateViewModel();
            viewModel.Disciplines = _context.Disciplines.ToList();
            viewModel.PostingTypes = _context.PostingTypes.ToList();
            return View(viewModel);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model, string[] createdLinkLabels, string[] createdLinkUrls)
        {
            if (ModelState.IsValid)
            {
                var resumeFileName = UploadResume(model);
                var uniqueFileName = ValidateImagePath(model);
                User newUser = ViewModeltoUser(model, uniqueFileName, resumeFileName);
                CreateUserLinks(createdLinkLabels, createdLinkUrls, newUser);
                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entityProjectModel = await _context.User
                .Include(i => i.UserProjects)
                .ThenInclude(i => i.Project)
                .Include(i => i.ContactLinks)
                .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

            if (entityProjectModel == null)
            {
                return NotFound();
            }
            var viewModel = userToEditViewModel(entityProjectModel);
            viewModel.Disciplines = _context.Disciplines.ToList();
            viewModel.PostingTypes = _context.PostingTypes.ToList();
            return View(viewModel);
        }


        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string OldPhotoPath, string[] createdLinkLabels, string[] createdLinkUrls, UserEditViewModel model, string OldResumePath)
        {
            var user = await _context.User.FindAsync(id);

            if (id != model.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueResumePath = OldResumePath;
                if (model.ResumeUpload != null)
                {
                    uniqueResumePath = UploadResume(model);
                    DeleteResume(model);
                }

                string uniqueFileName = OldPhotoPath;
                if (model.Photo != null)
                {
                    uniqueFileName = ValidateImagePath(model);
                    DeletePhoto(model);
                }
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Bio = model.Bio;
                user.Seeking = model.Seeking;
                user.PhotoPath = uniqueFileName;
                user.ResumePhotoPath = uniqueResumePath;
                user.displayMyProfile = model.displayMyProfile;
                user.AreaOfDiscipline = model.AreaOfDiscipline;
                _context.Update(user);
                await _context.SaveChangesAsync();


                if (!(createdLinkLabels[0] == null))
                {
                    CreateUserLinks(createdLinkLabels, createdLinkUrls, user);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            RemoveAllContactLinks(id);

            var user = await _context.User.FindAsync(id);
            var viewModel = userToEditViewModel(user);
            DeletePhoto(viewModel);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Account", "Register");
        }

        // POST: UserProjects/Delete/5
        [HttpPost, ActionName("DeleteLink")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLink(string userID, int contactLinkID)
        {
            ContactLink thisCL = ((_context.ContactLinks.Where(i => (i.Users.Id == userID) && (i.ContactLinkID == contactLinkID)).FirstOrDefault()));
            _context.ContactLinks.Remove(thisCL);
            await _context.SaveChangesAsync();
            var returnPath = "../User/Edit/" + userID.ToString();
            return Redirect(returnPath);
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
        
        //Get of Starred User Index
        public async Task<IActionResult> StarredPostingIndex()
        {
            var viewModel = new ViewModel();
            viewModel.StarredPostings = await _context.StarredPosting
                  .Include(i => i.User)
                  .Include(i => i.Posting)
                   .AsNoTracking()
                   .OrderBy(i => i.UserID)
                   .ToListAsync();
            return View(viewModel);
        }

        #endregion

    }
}
