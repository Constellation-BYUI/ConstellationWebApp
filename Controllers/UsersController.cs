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
            return(uniqueFileName);
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
                PhotoPath = userModel.PhotoPath
            };
            return(viewModel);
        }

        private User ViewModeltoUser(UserCreateViewModel model, string uniqueFileName)
        {
            User newUser = new User
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Bio = model.Bio,
                Seeking = model.Seeking,
                PhotoPath = uniqueFileName
            };
            return (newUser);
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

        private void DeletePhoto(UserEditViewModel model)
        {
            if (model.OldPhotoPath != null)
            {
                string filePath = Path.Combine(hostingEnvironment.WebRootPath, "image", model.OldPhotoPath);
                System.IO.File.Delete(filePath);
            }
        }

        private void PopulateAssignedProjectData(Project newProject)
        {
            var allUsers = _context.User;
            var userProjects = new HashSet<string>(newProject.UserProjects.Select(c => c.UserID));
            var viewModel = new List<AssignedProjectData>();
            foreach (var users in allUsers)
            {
                viewModel.Add(new AssignedProjectData
                {
                    UserID = users.Id,
                    UserName = users.UserName,
                    Assigned = userProjects.Contains(users.Id)
                });
            }
            ViewData["UsersOfConstellation"] = viewModel;
        }

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
          .Include(s => s.ContactLinks)
          .Include(s => s.UserProjects)
          .ThenInclude(s => s.Project)
          .ThenInclude(s => s.ProjectLinks)
          .AsNoTracking()
          .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            //var project = new Project();
            //project.UserProjects = new List<UserProject>();
            //PopulateAssignedProjectData(project);

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
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
             var uniqueFileName = ValidateImagePath(model);
             User newUser = ViewModeltoUser(model, uniqueFileName);
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
            return View(viewModel);
        }


        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string OldPhotoPath, string[] createdLinkLabels, string[] createdLinkUrls, UserEditViewModel model)
        {
            var user = await _context.User.FindAsync(id);

            if (id != model.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
