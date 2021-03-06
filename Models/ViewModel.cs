﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstellationWebApp.Models
{
    public class ViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<UserProject> UserProjects { get; set; }
        public IEnumerable<ContactLink> ContactLinks { get; set; }
        public IEnumerable<ProjectLink> ProjectLinks { get; set; }
        public IEnumerable<Posting> Postings { get; set; }
        public IEnumerable<PostingType> PostingTypes { get; set; }
        public IEnumerable<Posting_PostingType> Posting_PostingTypes { get; set; }
        public IEnumerable<StarredPosting> StarredPostings { get; set; }
        public IEnumerable<StarredUser> StarredUsers { get; set; }
        public IEnumerable<StarredProject> StarredProjects { get; set; }
        public IEnumerable<IntrestedCandidate> intrestedCandidates { get; set; }
        public IEnumerable<RecruiterPicks> RecruiterPicks { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<UserSkill> UserSkills { get; set; }
        public IEnumerable<Discipline> Disciplines { get; set; }
        public IEnumerable<SkillDiscipline> SkillDisciplines { get; set; }
        public IEnumerable<SkillLink> SkillLinks { get; set; }
        public User currentUser { get; set; }
        public Discipline currentDiscipline { get; set; }
        public IEnumerable<UserSkillLink> UserSkillLinks { get; set; }

        public IEnumerable<ChatUser> ChatUsers { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
        public IEnumerable<ChatMessage> ChatMessages { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public Chat SelectedChat { get; set; }
        public IEnumerable<ChatUser> SelectedChatUsers { get; set; }

        public IEnumerable<User> NonSelectedChatUsers { get; set; }



    }
}
