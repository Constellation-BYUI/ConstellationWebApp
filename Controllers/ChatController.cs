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

namespace ConstellationWebApp.Controllers
{   
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ConstellationWebAppContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ChatController(ConstellationWebAppContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

    #region ReturnFunctions
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

        private List<ChatMessage> ChatMessagesLookup(List<Chat> myChats)
        {
            List<ChatMessage> chatMessages = new List<ChatMessage>();
            foreach (var myChat in myChats)
            {
                List<ChatMessage> chatMessagesHolder = _context.ChatMessages.Where(i => i.ChatID == myChat.ChatID).ToList();
                foreach (var chatMessage in chatMessagesHolder)
                {
                    chatMessages.Add(chatMessage);
                }
            }
            return (chatMessages);
        }

        private List<ChatUser> ChatUserLookup(List<Chat> myChats)
        {
            List<ChatUser> chatUsers = new List<ChatUser>();
            foreach (var chat in myChats)
            {
                List<ChatUser> chatUserHolder = _context.ChatUsers.Where(i => i.ChatID == chat.ChatID).ToList();
                foreach (var chatUser in chatUserHolder)
                {
                    chatUsers.Add(chatUser);
                }
            }
            return (chatUsers);
        }

        private async Task CreateChatUsers(string[] selectedCollaborators, string currentUser, Chat newChat)
        {
            if (selectedCollaborators != null)
            {
                foreach (var user in selectedCollaborators)
                {
                    if (string.IsNullOrEmpty(user))
                    {
                        continue;
                    }
                    try
                    {
                        User foundUser = _context.User.Where(i => i.UserName == user || i.Id == user).FirstOrDefault();
                        ChatUser chatUsers = new ChatUser
                        {
                            ChatID = newChat.ChatID,
                            UserID = foundUser.Id
                        };
                        _context.Add(chatUsers);
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            //Add the current user to the list if they did not add themselves
            ChatUser currentChatUser = _context.ChatUsers.Where(i => i.UserID == currentUser && i.ChatID == newChat.ChatID).FirstOrDefault();
            if (currentChatUser == null)
            {
                ChatUser loggedInChatUser = new ChatUser
                {
                    ChatID = newChat.ChatID,
                    UserID = currentUser
                };
                _context.Add(loggedInChatUser);
                await _context.SaveChangesAsync();
            }
        }
        #endregion ReturnFunctions

        #region EndPoints
        // GET: Chat

        public async Task<IActionResult> Index(int selectedChat)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new ViewModel();

            ////Find any chats that have the current user included in the group 
            List<Chat> myChats = _context.Chats.Where(i => i.ChatUsers.Any(u => u.UserID == currentUser)).ToList();
            ////Find all ChatMessages of those chats
            List<ChatMessage> chatMessages = ChatMessagesLookup(myChats);
            ////Find all message of those chat messages
            List<Message> Messages = MessagesLookup(chatMessages);
            ////Find all ChatUsers of those chats
            List<ChatUser> chatUsers = ChatUserLookup(myChats);
            viewModel.ChatMessages = chatMessages;
            viewModel.Messages = Messages;
            viewModel.ChatUsers = chatUsers;

            ////Find all users of the App
            if (selectedChat.ToString() == null)
            {
                viewModel.Chats = await _context.Chats.Where(i => i.ChatUsers.Any(c => c.UserID == currentUser))
                     .Include(i => i.ChatUsers)
                     .ThenInclude(i => i.User)
                       .Include(i => i.ChatMessages)
                        .ThenInclude(i => i.Message)
                     .AsNoTracking()
                     .OrderBy(i => i.LastActivity)
                     .ToListAsync();
                viewModel.Users = _context.User.ToList();
                return View(viewModel);
            }
            else
            {
                viewModel.Chats = await _context.Chats.Where(i => i.ChatUsers.Any(c => c.UserID == currentUser))
                     .Include(i => i.ChatUsers)
                     .ThenInclude(i => i.User)
                       .Include(i => i.ChatMessages)
                        .ThenInclude(i => i.Message)
                     .AsNoTracking()
                     .OrderBy(i => i.LastActivity)
                     .ToListAsync();

                viewModel.Users = _context.User.ToList();
                viewModel.SelectedChat = _context.Chats.Where(i => i.ChatID == selectedChat).FirstOrDefault();
                return View(viewModel);
            }
        }

        // Create Chat: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateChat(string[] selectedCollaborators, string StartingMessage, bool IgnoreExistence)
        {
            //#0 eval if there is already an existing chat with the same members
            if(IgnoreExistence == false)
            {

                //First go through list of the selected collaborators and ensure that all values are the PK ID
                if (selectedCollaborators != null)
                {
                List<string> selectedChatUsers;                    
                    foreach (var user in selectedCollaborators)
                    {
                        if (string.IsNullOrEmpty(user))
                        {
                            continue;
                        }
                        try
                        {
                            User foundUser = _context.User.Where(i => i.UserName == user || i.Id == user).FirstOrDefault();
                            selectedChatUsers.Add(foundUser.Id);
                        }
                     }
                }
                Chat lookupChat = _context.Chats.Where(i => i.ChatUsers.)
            }


            //#1 Create the Chat
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var count = selectedCollaborators.Count();

            User theUser = _context.User.Where(i => i.Id == currentUser).FirstOrDefault();

            if(string.IsNullOrEmpty(selectedCollaborators.First()) && selectedCollaborators.Count() == 1)
            {
                return RedirectToAction(nameof(Index));
            }

            if ( selectedCollaborators.Count() == 1 && (selectedCollaborators.Contains(currentUser) || selectedCollaborators.Contains(theUser.UserName)))
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                Chat newChat = new Chat();
                newChat.CreationDate = DateTime.Now;
                newChat.LastActivity = DateTime.Now;
                _context.Add(newChat);
                await _context.SaveChangesAsync();


                //#2 append the ChatUsers
                await CreateChatUsers(selectedCollaborators, currentUser, newChat);


                //#3 include a chat message if entered
                if(StartingMessage != null)
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

                return RedirectToAction("Index", "Chat", new { @selectedChat = newChat.ChatID });
            }


            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> CreateMessage(string[] selectedCollaborators, string messageText, int chatID)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {               
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

                return RedirectToAction("Index", "Chat", new { @selectedChat = chatID });

            }
            return RedirectToAction(nameof(Index));

        }
        // POST: Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
        // POST: Chat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // POST: Chat/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteChat(int chatID)
        {
            ////Find the chat that is to be removed 
            List<Chat> removeThisChat = _context.Chats.Where(i => i.ChatID == chatID).ToList();
            ////Find all ChatMessages to be removed 
            List<ChatMessage> removeChatMessages = ChatMessagesLookup(removeThisChat);
            ////Find all message to be removed 
            List<Message> removeMessages = MessagesLookup(removeChatMessages);
            ////Find all ChatUsers to be removed 
            List<ChatUser> chatUsers = ChatUserLookup(removeThisChat);
         
            //no dependancy
            foreach(var chatUser in chatUsers)
            {
                _context.Remove(chatUser);
            }
            await _context.SaveChangesAsync();

            //no dependancy
            foreach (var chatMessages in removeChatMessages)
            {
                _context.Remove(chatMessages);
            }
            await _context.SaveChangesAsync();

            //dependancy on chatMessages being removed
            foreach (var message in removeMessages)
            {
                _context.Remove(message);
            }
            await _context.SaveChangesAsync();

            //dependancy on chatMessages, chatUsers, messages being removed
            Chat deleteChat = _context.Chats.Where(i => i.ChatID == chatID).FirstOrDefault();
            _context.Remove(deleteChat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        #endregion EndPoints
    }
}