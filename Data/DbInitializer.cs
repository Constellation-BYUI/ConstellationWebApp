using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ConstellationWebApp.Models;

namespace ConstellationWebApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(ConstellationWebAppContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.User.Any())
            {
                return;   // DB has been seeded
            }
            else
            {

                var postingType = new PostingType[]
                {
              new PostingType { PostingTypeName = "Internship"},
              new PostingType { PostingTypeName = "Full-Time"},
              new PostingType { PostingTypeName = "Part-Time"},
              new PostingType { PostingTypeName = "Salary"},
              new PostingType { PostingTypeName = "Hourly Wage"},
              new PostingType { PostingTypeName = "Remote"},
              new PostingType { PostingTypeName = "On Site"}
                };

                foreach (PostingType s in postingType)
                {
                    context.PostingTypes.Add(s);
                }
                context.SaveChanges();

                var disciplines = new Discipline[]
               {
              new Discipline { DisciplineName = "Software Engineering"},
              new Discipline { DisciplineName = "Project Engineering"},
              new Discipline { DisciplineName = "Industrial Engineering"},
              new Discipline { DisciplineName = "Energy Engineering"},
              new Discipline { DisciplineName = "Biological   Engineering"},
              new Discipline { DisciplineName = "BioMeidcial Engineering"},
              new Discipline { DisciplineName = "Applied Engineering"},
              new Discipline { DisciplineName = "Agricultural Engineering"},
              new Discipline { DisciplineName = "Aerospace Engineering"},
              new Discipline { DisciplineName = "Information Engineering"},
              new Discipline { DisciplineName = "Military Engineering"},
              new Discipline { DisciplineName = "Supply chain   Engineering"},
              new Discipline { DisciplineName = "Systems Engineering"},
              new Discipline { DisciplineName = "Textile Engineering"}
               };
                foreach (Discipline dis in disciplines)
                {
                    context.Disciplines.Add(dis);
                }
                context.SaveChanges();

                var skills = new Skill[]
            {
            //Software Engineering
              new Skill { SkillName = "Object-oriented design"},
              new Skill { SkillName = "Software testing and debugging"},
              new Skill { SkillName = "Problem solving and logical thinking"},
              new Skill { SkillName = "Written and verbal communication"},
              new Skill { SkillName = "Teamwork"},
              new Skill { SkillName = "C#"},
              new Skill { SkillName = "Node.js"},
              new Skill { SkillName = "Java"},
              new Skill { SkillName = "Python"},
              new Skill { SkillName = "C"},
              new Skill { SkillName = "Ruby"},
              new Skill { SkillName = "SQL"},
              new Skill { SkillName = "Postgres"},
              new Skill { SkillName = "MongoDB" },
            //Project Engineering
              new Skill { SkillName = "Scrum"},
              new Skill { SkillName = "IT skills"},
              new Skill { SkillName = "Organisation"},
              new Skill { SkillName = "Explain design ideas and plans clearly"},
              new Skill { SkillName = "Teamwork"},
              new Skill { SkillName = "Confident decision-making ability"},
              new Skill { SkillName = "Excellent communication skills"},
              new Skill { SkillName = "Agile"},
              new Skill { SkillName = "Waterfall"},
              new Skill { SkillName = "knowledge of relevant legal regulations"},

            //Industrial Engineering
              new Skill { SkillName = "Mathematics"},
              new Skill { SkillName = "Time Management"},
              new Skill { SkillName = "Mechanical aptitude"},
              new Skill { SkillName = "Written and verbal communication"},
              new Skill { SkillName = "Teamwork"},
              new Skill { SkillName = "Organization and efficiency"},
              new Skill { SkillName = "Statistical analysis"},
              new Skill { SkillName = "Material handling"},
              new Skill { SkillName = "Facility planning"},
              new Skill { SkillName = "Process analysis"},
              new Skill { SkillName = "Inventory control"},
              new Skill { SkillName = "Production control"},
              new Skill { SkillName = "Quantitative Reasoning"},
              new Skill { SkillName = "Creative problem solving" },

            //BioMeidcial Engineering"}
            new Skill { SkillName = "Analytical "},
              new Skill { SkillName = "Curiosity"},
              new Skill { SkillName = "Interpersonal Skills"},
              new Skill { SkillName = "Written and verbal communication"},
              new Skill { SkillName = "Creativity"},
              new Skill { SkillName = "Organization and efficiency"},
              new Skill { SkillName = "Detail-oriented"},
              new Skill { SkillName = "Biology "},
              new Skill { SkillName = "Chemistry"},
              new Skill { SkillName = "Mathematics"},
              new Skill { SkillName = "Science"},
              new Skill { SkillName = "Technology"}
            };

                foreach (Skill sk in skills)
                {
                    context.Skills.Add(sk);
                }
                context.SaveChanges();

            }
        }
    }
}
