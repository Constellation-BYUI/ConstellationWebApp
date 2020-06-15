using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConstellationWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ConstellationWebApp.Data
{
    public class ConstellationWebAppContext : IdentityDbContext
    {
            public ConstellationWebAppContext (DbContextOptions<ConstellationWebAppContext> options)
                : base(options)
            {
            }

         public DbSet<User> User { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<ContactLink> ContactLinks { get; set; }
        public DbSet<ProjectLink> ProjectLinks { get; set; }
        public DbSet<Posting> Postings { get; set; }

        public DbSet<PostingType> PostingTypes { get; set; }

        public DbSet<Posting_PostingType> Posting_PostingTypes { get; set; }

        public DbSet<ConstellationWebApp.Models.StarredPosting> StarredPosting { get; set; }

        public DbSet<ConstellationWebApp.Models.IntrestedCandidate> IntrestedCandidate { get; set; }

        public DbSet<ConstellationWebApp.Models.StarredProject> StarredProjects { get; set; }

        public DbSet<ConstellationWebApp.Models.StarredUser> StarredUsers { get; set; }

        public DbSet<ConstellationWebApp.Models.RecruiterPicks> RecruiterPicks { get; set; }

        public DbSet<ConstellationWebApp.Models.UserSkill> UserSkills { get; set; }
        public DbSet<ConstellationWebApp.Models.Skill> Skills { get; set; }
        public DbSet<ConstellationWebApp.Models.SkillDiscipline> SkillDisciplines { get; set; }
        public DbSet<ConstellationWebApp.Models.Discipline> Disciplines { get; set; }







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().ToTable("Project");

            modelBuilder.Entity<UserProject>()
       .HasKey(bc => new { bc.UserID, bc.ProjectID });
            modelBuilder.Entity<UserProject>()
                .HasOne(bc => bc.Project)
                .WithMany(b => b.UserProjects)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<UserProject>()
                .HasOne(bc => bc.Project)
                .WithMany(c => c.UserProjects)
                .HasForeignKey(bc => bc.ProjectID);

            modelBuilder.Entity<User>()
               .HasMany(c => c.ContactLinks)
               .WithOne(e => e.Users);

            modelBuilder.Entity<Project>()
             .HasMany(c => c.ProjectLinks)
             .WithOne(e => e.Projects);

            modelBuilder.Entity<PostingType>().ToTable("PostingType");
            modelBuilder.Entity<Posting_PostingType>().ToTable("Posting_PostingType");
            modelBuilder.Entity<Posting>().ToTable("Posting");
            modelBuilder.Entity<StarredPosting>().ToTable("StarredPosting");
            modelBuilder.Entity<StarredPosting>()
                .HasKey(b => b.StarredPostingID);
            modelBuilder.Entity<StarredPosting>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.StarredPostings)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<StarredPosting>()
                .HasOne(bc => bc.Posting)
                .WithMany(c => c.StarredPostings)
                .HasForeignKey(bc => bc.PostingID);

            modelBuilder.Entity<IntrestedCandidate>().ToTable("IntrestedCandidate");
            modelBuilder.Entity<IntrestedCandidate>()
                .HasKey(b => b.IntrestedCandidateID);
            modelBuilder.Entity<IntrestedCandidate>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.IntrestedCandidates)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<IntrestedCandidate>()
                .HasOne(bc => bc.Posting)
                .WithMany(c => c.IntrestedCandidates)
                .HasForeignKey(bc => bc.PostingID);

            modelBuilder.Entity<StarredProject>().ToTable("StarredProject");
            modelBuilder.Entity<StarredProject>()
                .HasKey(b => b.StarredProjectID);
            modelBuilder.Entity<StarredProject>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.StarredProjects)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<StarredProject>()
                .HasOne(bc => bc.Project)
                .WithMany(c => c.StarredProjects)
                .HasForeignKey(bc => bc.ProjectID);

            modelBuilder.Entity<StarredUser>().ToTable("StarredUser");
            //Primary Key
            modelBuilder.Entity<StarredUser>()
                .HasKey(b => b.StarredUserID);

            //User who is starring the other person
            modelBuilder.Entity<StarredUser>()
                .HasOne(bc => bc.StarOwner)
                .WithMany(b => b.StarredOwner)
                .HasForeignKey(bc => bc.StarredOwnerID);

            //the other User who is being starred
            modelBuilder.Entity<StarredUser>()
                .HasOne(bc => bc.StarredPerson)
                .WithMany(c => c.StarredUsers)
                .HasForeignKey(bc => bc.UserStarredID);

            modelBuilder.Entity<RecruiterPicks>().ToTable("RecruiterPicks");
            //Primary Key
            modelBuilder.Entity<RecruiterPicks>()
                .HasKey(b => b.RecuiterPicksID);

            //User who is starring the other person
            modelBuilder.Entity<RecruiterPicks>()
                .HasOne(bc => bc.Recuiter)
                .WithMany(b => b.Recuiter)
                .HasForeignKey(bc => bc.RecuiterID);

            //the other User who is being starred
            modelBuilder.Entity<RecruiterPicks>()
                .HasOne(bc => bc.Candidate)
                .WithMany(c => c.Candidates)
                .HasForeignKey(bc => bc.CandidateID);

            modelBuilder.Entity<RecruiterPicks>()
             .HasOne(bc => bc.Posting)
             .WithMany(c => c.RecruiterPicks)
             .HasForeignKey(bc => bc.PostingID);

            modelBuilder.Entity<ProjectPosting>()
               .HasKey(b => b.ProjectPostingID);
            modelBuilder.Entity<ProjectPosting>()
                .HasOne(bc => bc.Project)
                .WithMany(b => b.ProjectPostings)
                .HasForeignKey(bc => bc.ProjectID);
            modelBuilder.Entity<ProjectPosting>()
                .HasOne(bc => bc.Posting)
                .WithMany(c => c.ProjectPostings)
                .HasForeignKey(bc => bc.PostingID);

            modelBuilder.Entity<Skill>().ToTable("Skill");

            modelBuilder.Entity<UserSkill>().ToTable("UserSkill");
            modelBuilder.Entity<UserSkill>()
     .HasKey(bc => new { bc.UserID, bc.SkillID });
            modelBuilder.Entity<UserSkill>()
                .HasOne(bc => bc.Skills)
                .WithMany(b => b.UserSkills)
                .HasForeignKey(bc => bc.SkillID);
            modelBuilder.Entity<UserSkill>()
                .HasOne(bc => bc.Users)
                .WithMany(c => c.UserSkills)
                .HasForeignKey(bc => bc.UserID);

            modelBuilder.Entity<UserSkill>().ToTable("UserSkill");

            modelBuilder.Entity<SkillDiscipline>().ToTable("SkillDiscipline");
            modelBuilder.Entity<SkillDiscipline>()
     .HasKey(bc => new { bc.DisciplineID, bc.SkillID });
            modelBuilder.Entity<SkillDiscipline>()
                .HasOne(bc => bc.Skills)
                .WithMany(b => b.SkillDisciplines)
                .HasForeignKey(bc => bc.SkillID);
            modelBuilder.Entity<SkillDiscipline>()
                .HasOne(bc => bc.Disciplines)
                .WithMany(c => c.SkillDiscipline)
                .HasForeignKey(bc => bc.DisciplineID);      
        }


        public DbSet<ConstellationWebApp.Models.ProjectPosting> ProjectPosting { get; set; }
                                 
    }
    
}
