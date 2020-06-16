﻿// <auto-generated />
using System;
using ConstellationWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConstellationWebApp.Migrations
{
    [DbContext(typeof(ConstellationWebAppContext))]
    [Migration("20200616191103_SkillLink")]
    partial class SkillLink
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConstellationWebApp.Models.ContactLink", b =>
                {
                    b.Property<int>("ContactLinkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactLinkLabel")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ContactLinkUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ContactLinkID");

                    b.HasIndex("UsersId");

                    b.ToTable("ContactLinks");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.Discipline", b =>
                {
                    b.Property<int>("DisciplineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisciplineName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("DisciplineID");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.IntrestedCandidate", b =>
                {
                    b.Property<int>("IntrestedCandidateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostingID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IntrestedCandidateID");

                    b.HasIndex("PostingID");

                    b.HasIndex("UserID");

                    b.ToTable("IntrestedCandidate");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.Posting", b =>
                {
                    b.Property<int>("PostingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HidePosting")
                        .HasColumnType("bit");

                    b.Property<string>("PostingFor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostingOwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PostingTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SharableToTeam")
                        .HasColumnType("bit");

                    b.HasKey("PostingID");

                    b.HasIndex("PostingOwnerId");

                    b.ToTable("Posting");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.PostingType", b =>
                {
                    b.Property<int>("PostingTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PostingTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostingTypeID");

                    b.ToTable("PostingType");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.Posting_PostingType", b =>
                {
                    b.Property<int>("Posting_PostingTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Assigned")
                        .HasColumnType("bit");

                    b.Property<int>("PostingID")
                        .HasColumnType("int");

                    b.Property<int>("PostingTypeID")
                        .HasColumnType("int");

                    b.HasKey("Posting_PostingTypeID");

                    b.HasIndex("PostingID");

                    b.HasIndex("PostingTypeID");

                    b.ToTable("Posting_PostingType");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ProjectID");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.ProjectLink", b =>
                {
                    b.Property<int>("ProjectLinkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProjectLinkLabel")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ProjectLinkUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectsProjectID")
                        .HasColumnType("int");

                    b.HasKey("ProjectLinkID");

                    b.HasIndex("ProjectsProjectID");

                    b.ToTable("ProjectLinks");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.ProjectPosting", b =>
                {
                    b.Property<int>("ProjectPostingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostingID")
                        .HasColumnType("int");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.HasKey("ProjectPostingID");

                    b.HasIndex("PostingID");

                    b.HasIndex("ProjectID");

                    b.ToTable("ProjectPosting");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.RecruiterPicks", b =>
                {
                    b.Property<int>("RecuiterPicksID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CandidateID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ListTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostingID")
                        .HasColumnType("int");

                    b.Property<string>("RecuiterID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecuiterPicksID");

                    b.HasIndex("CandidateID");

                    b.HasIndex("PostingID");

                    b.HasIndex("RecuiterID");

                    b.ToTable("RecruiterPicks");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.Skill", b =>
                {
                    b.Property<int>("SkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisciplineID")
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkillID");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.SkillDiscipline", b =>
                {
                    b.Property<int>("DisciplineID")
                        .HasColumnType("int");

                    b.Property<int>("SkillID")
                        .HasColumnType("int");

                    b.HasKey("DisciplineID", "SkillID");

                    b.HasIndex("SkillID");

                    b.ToTable("SkillDiscipline");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.SkillLink", b =>
                {
                    b.Property<int>("SkillLinkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SkilLinkLabel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkillLinkUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkillLinkID");

                    b.ToTable("SkillLinks");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.StarredPosting", b =>
                {
                    b.Property<int>("StarredPostingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostingID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StarredPostingID");

                    b.HasIndex("PostingID");

                    b.HasIndex("UserID");

                    b.ToTable("StarredPosting");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.StarredProject", b =>
                {
                    b.Property<int>("StarredProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StarredProjectID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("UserID");

                    b.ToTable("StarredProject");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.StarredUser", b =>
                {
                    b.Property<int>("StarredUserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StarredOwnerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserStarredID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StarredUserID");

                    b.HasIndex("StarredOwnerID");

                    b.HasIndex("UserStarredID");

                    b.ToTable("StarredUser");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.UserProject", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "ProjectID");

                    b.HasIndex("ProjectID");

                    b.ToTable("UserProjects");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.UserSkill", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SkillID")
                        .HasColumnType("int");

                    b.Property<int>("SkillLinkID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "SkillID");

                    b.HasIndex("SkillID");

                    b.HasIndex("SkillLinkID")
                        .IsUnique();

                    b.ToTable("UserSkill");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResumePhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Seeking")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<bool>("displayMyProfile")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.ContactLink", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.User", "Users")
                        .WithMany("ContactLinks")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.IntrestedCandidate", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.Posting", "Posting")
                        .WithMany("IntrestedCandidates")
                        .HasForeignKey("PostingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstellationWebApp.Models.User", "User")
                        .WithMany("IntrestedCandidates")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.Posting", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.User", "PostingOwner")
                        .WithMany("Postings")
                        .HasForeignKey("PostingOwnerId");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.Posting_PostingType", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.Posting", "Postings")
                        .WithMany("Posting_PostingTypes")
                        .HasForeignKey("PostingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstellationWebApp.Models.PostingType", "PostingTypes")
                        .WithMany("Posting_PostingTypes")
                        .HasForeignKey("PostingTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstellationWebApp.Models.ProjectLink", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.Project", "Projects")
                        .WithMany("ProjectLinks")
                        .HasForeignKey("ProjectsProjectID");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.ProjectPosting", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.Posting", "Posting")
                        .WithMany("ProjectPostings")
                        .HasForeignKey("PostingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstellationWebApp.Models.Project", "Project")
                        .WithMany("ProjectPostings")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstellationWebApp.Models.RecruiterPicks", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.User", "Candidate")
                        .WithMany("Candidates")
                        .HasForeignKey("CandidateID");

                    b.HasOne("ConstellationWebApp.Models.Posting", "Posting")
                        .WithMany("RecruiterPicks")
                        .HasForeignKey("PostingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstellationWebApp.Models.User", "Recuiter")
                        .WithMany("Recuiter")
                        .HasForeignKey("RecuiterID");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.SkillDiscipline", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.Discipline", "Disciplines")
                        .WithMany("SkillDiscipline")
                        .HasForeignKey("DisciplineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstellationWebApp.Models.Skill", "Skills")
                        .WithMany("SkillDisciplines")
                        .HasForeignKey("SkillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstellationWebApp.Models.StarredPosting", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.Posting", "Posting")
                        .WithMany("StarredPostings")
                        .HasForeignKey("PostingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstellationWebApp.Models.User", "User")
                        .WithMany("StarredPostings")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.StarredProject", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.Project", "Project")
                        .WithMany("StarredProjects")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstellationWebApp.Models.User", "User")
                        .WithMany("StarredProjects")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.StarredUser", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.User", "StarOwner")
                        .WithMany("StarredOwner")
                        .HasForeignKey("StarredOwnerID");

                    b.HasOne("ConstellationWebApp.Models.User", "StarredPerson")
                        .WithMany("StarredUsers")
                        .HasForeignKey("UserStarredID");
                });

            modelBuilder.Entity("ConstellationWebApp.Models.UserProject", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.Project", "Project")
                        .WithMany("UserProjects")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstellationWebApp.Models.User", "User")
                        .WithMany("UserProjects")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstellationWebApp.Models.UserSkill", b =>
                {
                    b.HasOne("ConstellationWebApp.Models.Skill", "Skills")
                        .WithMany("UserSkills")
                        .HasForeignKey("SkillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstellationWebApp.Models.SkillLink", "SkillLinks")
                        .WithOne("UserSkills")
                        .HasForeignKey("ConstellationWebApp.Models.UserSkill", "SkillLinkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstellationWebApp.Models.User", "Users")
                        .WithMany("UserSkills")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
