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
            context.Database.EnsureCreated();

           //Look for any students.
           if (context.User.Any() || context.PostingTypes.Any())
           {
               return;   // DB has been seeded
           }

           
            var postingType = new PostingType[]
            {
              new PostingType { PostingTypeName = "Internship"},
              new PostingType { PostingTypeName = "Full-Time employement"},
              new PostingType { PostingTypeName = "Part-Time employement"},
              new PostingType { PostingTypeName = "Salary base pay"},
              new PostingType { PostingTypeName = "Hourly wage"},
              new PostingType { PostingTypeName = "Remote"},
              new PostingType { PostingTypeName = "On Site"},
              new PostingType { PostingTypeName = "Community Service"},
              new PostingType { PostingTypeName = "Society Project"},
              new PostingType { PostingTypeName = "School Project"},
              new PostingType { PostingTypeName = "Volunteer"}
            };

            foreach (PostingType s in postingType)
            {
                context.PostingTypes.Add(s);
            }
            context.SaveChanges();

            var disciplines = new Discipline[]
           {
              new Discipline { DisciplineName = "Network Engineering"},
              new Discipline { DisciplineName = "Cyber Security"},
              new Discipline { DisciplineName = "Database Administration"},
              new Discipline { DisciplineName = "Quality Assurance Engineering"},
              new Discipline { DisciplineName = "Software Development"},
              new Discipline { DisciplineName = "System Administration"},
              new Discipline { DisciplineName = "Soft Skills"}
           };            
            foreach (Discipline dis in disciplines)
            {
                context.Disciplines.Add(dis);
            }
            context.SaveChanges();

            var skills = new Skill[]
            {
              //Network Engineering 11
              new Skill {  SkillName = "Troubleshooting of Computer hardware" },
              new Skill {  SkillName = "Managing / Maintaining Servers | Routers | Switches" },
              new Skill {  SkillName = "Security administration port security on switch and IP security on Router via Access list"},
              new Skill {  SkillName = "Email troubleshooting and maintenance" },
              new Skill {  SkillName = "Configuring / Managing / Maintaining Networking Equipment" },
              new Skill {  SkillName = "Network processing: centralized / distributive network connection" },
              new Skill {  SkillName = "Installing / configuring / administering network technologies" },
              new Skill {  SkillName = "Installing / configuring workstations for IP / IPX based LAN" },
              new Skill {  SkillName = "Installing / configuring DHCP Client / Server" },
              new Skill {  SkillName = "Backup Management / Reporting / Recovery" },
              new Skill {  SkillName = "Disaster / Recovery" },
              new Skill {  SkillName = "Security Incident Handling & Response" },
              //Cyber security 10
              new Skill {  SkillName = "SIEM Management" },
              new Skill {  SkillName = "Audit & Compliance" },
              new Skill {  SkillName = "Analytics & Intelligence" },
              new Skill {  SkillName = "Firewall / IDS / IPS Skills" },
              new Skill {  SkillName = "Intrusion Detection" },
              new Skill {  SkillName = "Application Security Developmentrly"},
              new Skill {  SkillName = "Advanced Malware Prevention" },
              new Skill {  SkillName = "Mobile Device Management" },
              new Skill {  SkillName = "Data Management Protection" },
              new Skill {  SkillName = "Digital Forensics" },
              new Skill {  SkillName = "Identity & Access Management" },
              //Data 11
              new Skill {  SkillName = "Data Modellings"},
              new Skill {  SkillName = "Extract, Transform, Load various data types and sources" },
              new Skill {  SkillName = "Design and test Database plans" },
              new Skill {  SkillName = "Database Security" },
              new Skill {  SkillName = "Optimize database performance" },
              new Skill {  SkillName = "PostgreSQL" },
              new Skill {  SkillName = "SQL" },
              new Skill {  SkillName = "SqlServer" },
              new Skill {  SkillName = "MySQL" },
              new Skill {  SkillName = "PLSQL" },
              new Skill {  SkillName = "Oracle" },
              new Skill {  SkillName = "MongoDB" },
              //QA 12
              new Skill {  SkillName = "Understanding of variation introduced by measurement devices" },
              new Skill {  SkillName = "Effective usage of data analysis tools" },
              new Skill {  SkillName = "Analytical and research skills" },
              new Skill {  SkillName = "Effective interaction with other departments / suppliers" },
              new Skill {  SkillName = "Ability to manage multiple priorities" },
              new Skill {  SkillName = "Drafting/interpreting/implementing quality assurance procedures for the organization" },
              new Skill {  SkillName = "Evaluating new/existing regulations to ensure your quality assurance protocols" },
              new Skill {  SkillName = "Ensuring product quality through regular auditing and testing" },
              new Skill {  SkillName = "Recording the results of your internal audits for reference: statistical data/quality" },
              new Skill {  SkillName = "Identifying areas that can be addressed to improve product quality and safety" },
              new Skill {  SkillName = "Developing training processes for each individual who handles or interacts with the product" },
              new Skill {  SkillName = "Ensuring ongoing compliance and risk management across the organization" },
              //Sofware 12
              new Skill {  SkillName = "Data Structures and Algorithms" },
              new Skill {  SkillName = "JS/HTML/CSS" },
              new Skill {  SkillName = "Python" },
              new Skill {  SkillName = "SQL" },
              new Skill {  SkillName = "Java" },
              new Skill {  SkillName = "BashShell/Powershell" },
              new Skill {  SkillName = "C#" },
              new Skill {  SkillName = "PHP" },
              new Skill {  SkillName = "C++" },
              new Skill {  SkillName = "TypeScript" },
              new Skill {  SkillName = "Unit Testing" },
              new Skill {  SkillName = "Integration testing" },
              //sys admin 11
              new Skill {  SkillName = "Desktop Imaging" },
              new Skill {  SkillName = "Installing configuring administrating software applications" },
              new Skill {  SkillName = "Shell Scripting" },
              new Skill {  SkillName = "Linux" },
              new Skill {  SkillName = "Windows" },
              new Skill {  SkillName = "Mac" },
              new Skill {  SkillName = "Vmware" },
              new Skill {  SkillName = "Active Directory" },
              new Skill {  SkillName = "Infrastructure" },
              new Skill {  SkillName = "Domain Controllers" },
              new Skill {  SkillName = "Group Policy" },
              new Skill {  SkillName = "Cloud services." },
              //soft 12
              new Skill {  SkillName = "Analytical thinking, planning." },
              new Skill {  SkillName = "Strong verbal and personal communication skills" },
              new Skill {  SkillName = "Accuracy and Attention to detail" },
              new Skill {  SkillName = "Organization and prioritization skills" },
              new Skill {  SkillName = "Effective problem analysis" },
              new Skill {  SkillName = "Self motivated" },
              new Skill {  SkillName = "Tolerant and flexible" },
              new Skill {  SkillName = "Enthusiasm" },
              new Skill {  SkillName = "Confidence and assertiveness" },
              new Skill {  SkillName = "Compasionate" },
              new Skill {  SkillName = "Dependable" },
              new Skill {  SkillName = "Leadership" }
        };
            foreach (Skill sk in skills)
            {
                context.Skills.Add(sk);
            }
            context.SaveChanges();

            var skillDisciplines = new SkillDiscipline[]
            {
              //networking : 11
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Troubleshooting of Computer hardware").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Managing / Maintaining Servers | Routers | Switches").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Security administration port security on switch and IP security on Router via Access list").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Email troubleshooting and maintenance").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Configuring / Managing / Maintaining Networking Equipment").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Network processing: centralized / distributive network connection").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Installing / configuring / administering network technologies").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Installing / configuring workstations for IP / IPX based LAN").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Installing / configuring DHCP Client / Server").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Backup Management / Reporting / Recovery").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Disaster / Recovery").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Network Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Security Incident Handling & Response").SkillID },
              // security : 10                                
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "SIEM Management").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Audit & Compliance").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Analytics & Intelligence").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Firewall / IDS / IPS Skills").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Intrusion Detection").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Application Security Developmentrly").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Advanced Malware Prevention").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Mobile Device Management").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Data Management Protection").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Digital Forensics").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Cyber Security").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Identity & Access Management").SkillID},
              //Data 11
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Data Modellings").SkillID },                                                                 
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Extract, Transform, Load various data types and sources").SkillID},
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Design and test Database plans").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Database Security").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Optimize database performance").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "PostgreSQL").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "SQL").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "SqlServer").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "MySQL").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "PLSQL").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Oracle").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Database Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "MongoDB").SkillID },
             //QA  12     
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Understanding of variation introduced by measurement devices").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Effective usage of data analysis tools").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Analytical and research skills").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Effective interaction with other departments / suppliers").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Ability to manage multiple priorities").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Drafting/interpreting/implementing quality assurance procedures for the organization").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Evaluating new/existing regulations to ensure your quality assurance protocols").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Ensuring product quality through regular auditing and testing").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Recording the results of your internal audits for reference: statistical data/quality").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Identifying areas that can be addressed to improve product quality and safety").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Developing training processes for each individual who handles or interacts with the product").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Quality Assurance Engineering").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Ensuring ongoing compliance and risk management across the organization").SkillID },
               //Sofware 12
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Data Structures and Algorithms").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "JS/HTML/CSS").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Python").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "SQL").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Java").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "BashShell/Powershell").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "C#").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "PHP").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "C++").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "TypeScript").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Unit Testing").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Software Development").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Integration testing").SkillID },
               //sys admin 11
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Desktop Imaging").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Installing configuring administrating software applications").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Shell Scripting").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Linux").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Windows").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Mac").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Vmware").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Active Directory").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Infrastructure").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Domain Controllers").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Group Policy").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "System Administration").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Cloud services.").SkillID },
              //soft 12
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Analytical thinking, planning.").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Strong verbal and personal communication skills").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Accuracy and Attention to detail").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Organization and prioritization skills").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Effective problem analysis").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Self motivated").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Tolerant and flexible").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Enthusiasm").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Confidence and assertiveness").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Compasionate").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Dependable").SkillID },
              new SkillDiscipline{ DisciplineID = disciplines.Single( d => d.DisciplineName == "Soft Skills").DisciplineID, SkillID = skills.Single( s => s.SkillName == "Leadership").SkillID }
            };
            foreach (SkillDiscipline sd in skillDisciplines)
            {
                context.SkillDisciplines.Add(sd);
            }
            context.SaveChanges();
        }
    }
}
