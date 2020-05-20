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

            modelBuilder.Entity<PostingType>().ToTable("PostingTypes");
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
        }




    }
    
}
