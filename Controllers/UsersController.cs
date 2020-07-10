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
using Microsoft.AspNetCore.Identity;

namespace ConstellationWebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ConstellationWebAppContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public UsersController(ConstellationWebAppContext context, IWebHostEnvironment hostingEnvironment, UserManager<User> userManager,
                                 SignInManager<User> signInManager)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }

        //Region of all Methods that perform a recieve/return function
        #region SimpleProcessAndReturnFunctions
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
                    createdLinkUrls[i] = createdLinkUrls[i].ToLower();
                    if (!createdLinkUrls[i].Contains("constellation.citwdd.net") && !(createdLinkUrls[i].Contains("http://") || createdLinkUrls[i].Contains("https://"))) 
                    {
                        createdLinkUrls[i] = "http://" + createdLinkUrls[i];
                    }

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



        private void DeletePhotoAndResume(UserEditViewModel model)
        {
            if (model.OldPhotoPath != null)
            {
                string filePath = Path.Combine(hostingEnvironment.WebRootPath, "image", model.OldPhotoPath);
                System.IO.File.Delete(filePath);
            }
            if (model.ResumePhotoPath != null)
            {
                string resumefilePath = Path.Combine(hostingEnvironment.WebRootPath, "image", model.ResumePhotoPath);
                System.IO.File.Delete(resumefilePath);
            }
        }

        private void DeletePhoto(UserEditViewModel model)
        {            if (model.OldPhotoPath != null)
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

        public List<Skill> UserSkillsLookup(string id)
        {
            List<Skill> currentSkills = new List<Skill>();


            //find all disciplines
            foreach (var skill in _context.UserSkills.Where(i => i.UserID == id))
            {
                Skill SkillOfUser = _context.Skills.Where(i => i.SkillID == skill.SkillID).FirstOrDefault();

                if (!currentSkills.Contains(SkillOfUser))
                {
                    currentSkills.Add(SkillOfUser);
                }
                else
                {
                    continue;
                }

            }
            return currentSkills;
        }

        public List<SkillDiscipline> SkillDisciplineLookup(List<Skill> skills)
        {
            List<Discipline> currentDisciplines = new List<Discipline>();
            List<SkillDiscipline> skillDisciplineLookup = new List<SkillDiscipline>();
            //find all disciplines
            foreach (var skill in skills)
            {
                SkillDiscipline skillDisciplineForSkill = _context.SkillDisciplines.Where(i => i.SkillID == skill.SkillID).FirstOrDefault();
                if (!skillDisciplineLookup.Contains(skillDisciplineForSkill))
                {
                    skillDisciplineLookup.Add(skillDisciplineForSkill);
                }
                else
                {
                    continue;
                }
            }
            return (skillDisciplineLookup);
        }

        public List<Discipline> UserDisciplineLookup(List<SkillDiscipline> skillDisciplines)
        {
            List<Discipline> currentDisciplines = new List<Discipline>();
            //find all disciplines
            foreach (var sd in skillDisciplines)
            {
                Discipline disciplineForSkill = _context.Disciplines.Where(i => i.DisciplineID == sd.DisciplineID).FirstOrDefault();
                if (!currentDisciplines.Contains(disciplineForSkill))
                {
                    currentDisciplines.Add(disciplineForSkill);
                }
                else
                {
                    continue;
                }
            }
            return (currentDisciplines);
        }


        #endregion


        // Post & Get Functions (includeing the starredUsers index Get)
        #region UserEndpoints
        // GET: Users/Index
        [AllowAnonymous]
        public async Task<IActionResult> Index(string discplineSearch, string nameSearch, string skillSearch)
        {
            var viewModel = new ViewModel();
            viewModel.Skills = _context.Skills;
            viewModel.Disciplines = _context.Disciplines;

            if (discplineSearch != null)
            {
              viewModel.Users = await _context.User.Where(i => i.AreaOfDiscipline.Contains(discplineSearch))
             .Include(i => i.ContactLinks)
             .Include(i => i.UserSkills)
              .AsNoTracking()
              .OrderBy(i => i.Id)
              .ToListAsync();
                return View(viewModel);
            }
            else if(nameSearch != null)
            {
                viewModel.Users = await _context.User.Where(i => i.UserName.Contains(nameSearch) || i.FirstName.Contains(nameSearch) || i.LastName.Contains(nameSearch))
             .Include(i => i.ContactLinks)
             .Include(i => i.UserSkills)
              .AsNoTracking()
              .OrderBy(i => i.Id)
              .ToListAsync();
                return View(viewModel);
            }
            else if (skillSearch != null)
            {
                var foundSkill = _context.Skills.Where(i => i.SkillName == skillSearch).FirstOrDefault();
                viewModel.Users = await _context.User.Where(i => i.UserSkills.Any(e => e.SkillID == foundSkill.SkillID))
             .Include(i => i.ContactLinks)
             .Include(i => i.UserSkills)
              .AsNoTracking()
              .OrderBy(i => i.Id)
              .ToListAsync();
                return View(viewModel);
            }
            else
            {
                viewModel.Users = await _context.User
                .Include(i => i.ContactLinks)
                 .Include(i => i.UserSkills)
                 .AsNoTracking()
                 .OrderBy(i => i.Id)
                 .ToListAsync();
                return View(viewModel);
            }           
        }

        private List<Message> MessagesLookup(List<ChatMessage> chatMessages)
        {
            List<Message> messages = new List<Message>();
            foreach (var chatMessage in chatMessages)
            {
                List<Message> messageHolder = _context.Messages.Where(i => i.MessageID == chatMessage.MessageID).ToList();
                foreach (var message in messageHolder)
                {
                    messages.Add(message);
                }
            }
            return (messages);
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
          .Include(s => s.UserSkills)
          .AsNoTracking()
          .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            User userDetails = _context.User.Where(i => i.Id == id).FirstOrDefault();

            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<ConstellationWebApp.Models.UserProject> collaboratorsList = new List<UserProject>();
            List<UserProject> userProjectList = _context.UserProjects.ToList();
            List<User> usersList = _context.User.ToList();
            List<Posting> currentUserPostings = _context.Postings.Where(i => i.PostingOwner.Id == currentUser).ToList();
            List<RecruiterPicks> recruitersData = _context.RecruiterPicks.Where(i => i.RecuiterID == currentUser).ToList();

            List<UserSkill> userSkills = _context.UserSkills.Where(i => i.UserID == id).ToList();
            List<UserSkillLink> userSkillLinks = _context.UserSkillLinks.Where(i => i.UserSkills.UserID == id).ToList();
            List<SkillLink> skillLinks = _context.SkillLinks.Where(i => i.SkillLinkOwner == id).ToList();
            List<Skill> skills = UserSkillsLookup(id);
            List<SkillDiscipline> skillDisciplines = SkillDisciplineLookup(skills);
            List<Discipline> disciplines = UserDisciplineLookup(skillDisciplines);

            //find the messages between the current user and the user being viewed
            Chat selectedChat = _context.Chats.Where(i => i.ChatUsers.Any(c => c.UserID == currentUser) && i.ChatUsers.Any(c => c.UserID == id) && i.ChatUsers.Count() == 2).FirstOrDefault();
            if (selectedChat != null)
            {
                List<ChatMessage> chatmessageThread = (_context.ChatMessages.Where(m => m.ChatID == selectedChat.ChatID)).ToList();
                List<Message> messages = MessagesLookup(chatmessageThread);
                ViewBag.selectedChat = selectedChat;
                ViewBag.Messages = messages;
            }
            ViewBag.collaborators = userProjectList;
            ViewBag.allUsers = usersList;
            ViewBag.allNeededDisciplines = disciplines;
            ViewBag.allNeededSKills = skills;
            ViewBag.allNeededSKillDisciplines = skillDisciplines;

            ViewBag.currentUserPostings = currentUserPostings;
            ViewBag.recruitersData = recruitersData;
            ViewBag.thisSkills = userSkills;
            ViewBag.thisUserSkillLinks = userSkillLinks;
            ViewBag.thisSkillLinks = skillLinks;

          

            List<StarredUser> thisSU = _context.StarredUsers.Where(i => i.StarredOwnerID == currentUser).ToList();
            ViewBag.StarredUsers = thisSU;
            return View(user);
        }
        
        // POST: Users/Create is handeled by the Account controller

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
            //Fist remove relational data: join tables
            RemoveAllStarredData(id);
            RemoveAllContactLinks(id);
            RemoveAllUserSkills(id);
            //Second remove owner data: Projects/Postings/Profile Image & Resume
            RemoveAllPostingandProjects(id);
            var user = await _context.User.FindAsync(id);
            var viewModel = userToEditViewModel(user);
            DeletePhotoAndResume(viewModel);
            //Finally remove entitiy Data: User
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        private void RemoveAllPostingandProjects(string id)
        {
            var userProjectsData = _context.UserProjects.Where(s => s.UserID == id);
            foreach (var instanceOfuserProjects in userProjectsData)
            {
                _context.UserProjects.Remove(instanceOfuserProjects);
            }

            //Only delete Projects which are not shared by other Collaborators
            var SoloProjectsData = _context.Projects.Where(s => s.UserProjects.Count < 1 && s.UserProjects.Any(u => u.UserID == id));
            foreach (var instanceOfSoloProject in SoloProjectsData)
            {
                _context.Projects.Remove(instanceOfSoloProject);
            }

            var PostingsData = _context.Postings.Where(s => s.PostingOwner.Id == id);
            foreach (var instanceOfPosting in PostingsData)
            {
                _context.Postings.Remove(instanceOfPosting);
            }
        }

        private void RemoveAllStarredData(string id)
        {
            var starredUserData = _context.StarredUsers.Where(s => s.StarredOwnerID == id || s.UserStarredID == id);
            foreach (var instanceOfStarredUser in starredUserData)
            {
                _context.StarredUsers.Remove(instanceOfStarredUser);
            }
            var starredProjectData = _context.StarredProjects.Where(s => s.UserID == id);
            foreach (var instanceOfstarredProject in starredProjectData)
            {
                _context.StarredProjects.Remove(instanceOfstarredProject);
            }

            var starredPostingsData = _context.StarredPosting.Where(s => s.UserID == id);
            foreach (var instanceOfStarredPosting in starredPostingsData)
            {
                _context.StarredPosting.Remove(instanceOfStarredPosting);
            }

            var intrestedCandidateData = _context.IntrestedCandidate.Where(s => s.UserID == id);
            foreach (var instanceOfintrestedCandidate in intrestedCandidateData)
            {
                _context.IntrestedCandidate.Remove(instanceOfintrestedCandidate);
            }

            var recuiterPickData = _context.RecruiterPicks.Where(s => s.CandidateID == id || s.RecuiterID == id);
            foreach (var instanceOfrecuiterPick in recuiterPickData)
            {
                _context.RecruiterPicks.Remove(instanceOfrecuiterPick);
            }
        }

        private void RemoveAllUserSkills(string id)
        {
            var userSkilllinks = _context.UserSkillLinks.Where(s => s.UserSkills.UserID == id);
            foreach (var uslink in userSkilllinks)
            {
                _context.UserSkillLinks.Remove(uslink);
            }

            var userSkills = _context.UserSkills.Where(s => s.UserID == id);
            foreach (var userSkill in userSkills)
            {
                _context.UserSkills.Remove(userSkill);
            }

            var skillLinks = _context.SkillLinks.Where(s => s.SkillLinkOwner == id);
            foreach (var skillLink in skillLinks)
            {
                _context.SkillLinks.Remove(skillLink);
            }
        }

        // POST: UserProjects/Delete/5
        [HttpPost, ActionName("DeleteLink")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLink(string userID, int contactLinkID)
        {
            ContactLink thisCL = ((_context.ContactLinks.Where(i => (i.Users.Id == userID) && (i.ContactLinkID == contactLinkID)).FirstOrDefault()));
            _context.ContactLinks.Remove(thisCL);
            await _context.SaveChangesAsync();
            var returnPath = "../Users/Edit/" + userID.ToString();
            return Redirect(returnPath);
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CreateChat(string StartingMessage, string recipientID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            User theUser = _context.User.Where(i => i.Id == currentUser).FirstOrDefault();           

            //#1 Create the Chat. Only when there was not chat with the specified user list OR the user chose to ignore the default
            if (ModelState.IsValid)
            {
                Chat newChat = new Chat();
                newChat.CreationDate = DateTime.Now;
                newChat.LastActivity = DateTime.Now;
                _context.Add(newChat);
                await _context.SaveChangesAsync();

                //#2 append the ChatUsers
                ChatUser chatUser1 = new ChatUser
                {
                    ChatID = newChat.ChatID,
                    UserID = currentUser
                };
                _context.Add(chatUser1);
                await _context.SaveChangesAsync();

                ChatUser chatUser2 = new ChatUser
                {
                    ChatID = newChat.ChatID,
                    UserID = recipientID
                };
                _context.Add(chatUser2);
                await _context.SaveChangesAsync();

                //#3 include a chat message if entered
                if (StartingMessage != null)
                {
                    Message newMessage = new Message();
                    newMessage.MessageText = StartingMessage;
                    newMessage.SenderID = currentUser;
                    newMessage.SentTime = DateTime.Now;
                    _context.Add(newMessage);
                    await _context.SaveChangesAsync();

                    ChatMessage newChatMesssage = new ChatMessage();
                    newChatMesssage.ChatID = newChat.ChatID;
                    newChatMesssage.MessageID = newMessage.MessageID;
                    _context.Add(newChatMesssage);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Details", "USers", new { @id = recipientID });
            }
            return RedirectToAction("Details", "USers", new { @id = recipientID });
        }

        public async Task<IActionResult> CreateMessage(string messageText, int chatID, string detailsID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null && ModelState.IsValid) { 
            Message newMessage = new Message();
            newMessage.MessageText = messageText;
            newMessage.SenderID = currentUser;
            newMessage.SentTime = DateTime.Now;
            _context.Add(newMessage);
            await _context.SaveChangesAsync();

            ChatMessage newChatMesssage = new ChatMessage();
            newChatMesssage.ChatID = chatID;
            newChatMesssage.MessageID = newMessage.MessageID;
            _context.Add(newChatMesssage);
            await _context.SaveChangesAsync();

            Chat updateChat = _context.Chats.Where(i => i.ChatID == chatID).FirstOrDefault();
            updateChat.LastActivity = DateTime.Now;
            _context.Update(updateChat);
            await _context.SaveChangesAsync();

            int selectedChat = chatID;

            return RedirectToAction("Details", "Users", new { @id = detailsID });
        }
            return RedirectToAction("Details", "Users", new { @id = detailsID });

        }
        #endregion

    }
}
