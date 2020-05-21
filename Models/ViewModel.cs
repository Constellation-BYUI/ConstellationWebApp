using System;
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
        public IEnumerable<PostingType> PostingType { get; set; }
        public IEnumerable<Posting_PostingType> Posting_PostingType { get; set; }
        public IEnumerable<StarredPosting> StarredPostings { get; set; }
        public IEnumerable<StarredProject> StarredProjects { get; set; }
        public IEnumerable<IntrestedCandidate> intrestedCandidates { get; set; }


    }
}
