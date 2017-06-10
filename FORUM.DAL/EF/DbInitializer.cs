using System.Data.Entity;
using FORUM.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FORUM.DAL.EF
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ForumDbContext>
    {
        protected override void Seed(ForumDbContext db)
        {
            Forum java = db.Forums.Add(new Forum {Name = "Java", Description = "Java (not to be confused with JavaScript or JScript) is a general-purpose object-oriented programming language designed to be used in conjunction with the Java Virtual Machine (JVM). Java platform is the name for a computing system that has installed tools for developing and running Java programs. Use this tag for questions referring to the Java programming language or Java platform tools." });
            Forum csharp = db.Forums.Add(new Forum { Name = "C#", Description = "C# (pronounced C sharp) is an object-oriented programming language that is designed for building a variety of applications that run on the .NET Framework. C# is simple, powerful, type-safe, and object-oriented." });
            Forum cpp = db.Forums.Add(new Forum { Name = "C++", Description = "C++ is a general-purpose programming language. It was originally designed as an extension to C, and keeps a similar syntax, but is now a completely different language. Use this forum for questions about code (to be) compiled with a C++ compiler." });
            Forum python= db.Forums.Add(new Forum { Name = "Python", Description = "Python is a dynamic and strongly typed programming language designed to emphasize usability." });
            Forum php = db.Forums.Add(new Forum { Name = "PHP", Description = "PHP is a general-purpose programming language primarily designed for server-side web development." });
            Forum js = db.Forums.Add(new Forum { Name = "JavaScript", Description = "JavaScript (not to be confused with Java) is a dynamic, weakly-typed language used for both client-side and server-side scripting. Use this forum for questions regarding ECMAScript and its various dialects/implementations (excluding ActionScript and Google-Apps-Script)." });
            Forum css = db.Forums.Add(new Forum { Name = "CSS", Description = "CSS (Cascading Style Sheets) is a style sheet language used for describing the look and formatting of HTML (Hyper Text Markup Language), XML (Extensible Markup Language) documents and SVG elements including (but not limited to) colors, layout, and fonts." });
            Forum test = db.Forums.Add(new Forum { Name = "Test", Description = "TEST" });
            db.Forums.AddRange(new List<Forum> {java, csharp, cpp, python, php, js, css, test});

            Topic topic1 = db.Topics.Add(new Topic { Title = "Java how not to print string from for loop? (ARRAY)", Description = "Problem with this...",Forum = java, CreationDate = DateTime.Now, UserName = "User"});
            Topic topic2 = db.Topics.Add(new Topic { Title = "Set cell size in Google sheets using Java API", Description = "Problem with this...", Forum = java, CreationDate = DateTime.Now, UserName = "User" });
            Topic topic3 = db.Topics.Add(new Topic { Title = "UnsupportedOperationException AudioEffect: invalid parameter operation", Description = "Problem with this...", Forum = java, CreationDate = DateTime.Now, UserName = "User" });
            Topic topic4 = db.Topics.Add(new Topic { Title = "Scroll to bottom in recyclerview with multiple view types", Description = "Problem with this...", Forum = java, CreationDate = DateTime.Now, UserName = "User" });
            Topic topic5 = db.Topics.Add(new Topic { Title = "Android 5.x ClassNotFoundException works fine on 6.0+", Description = "Problem with this...", Forum = java, CreationDate = DateTime.Now, UserName = "User" });
            Topic topic6 = db.Topics.Add(new Topic { Title = "How not to print string from for loop? (ARRAY)", Description = "Problem with this...", Forum = csharp, CreationDate = DateTime.Now, UserName = "User" });
            Topic topic7 = db.Topics.Add(new Topic { Title = "Set cell size in Google sheets using API", Description = "Problem with this...", Forum = csharp, CreationDate = DateTime.Now, UserName = "User" });
            List<Topic> threadList = new List<Topic>() { topic1, topic2, topic3, topic4, topic5, topic6, topic7 };
            
            Post post1 = db.Posts.Add(new Post { Message = "I have a recyclerview with multiple items. and recyclerview has different viewtypes with different heights..", Topic = topic1, UserName = "User", PostTime = DateTime.Now});
            Post post2 = db.Posts.Add(new Post { Message = "I don't know the reason, But I changed my RecyclerView height to match_parent and it's working properly.", Topic =topic1, UserName = "User", PostTime = DateTime.Now});
            List<Post> postList = new List<Post>() {post1, post2};
            
            var userManager = new UserManager<User>(new UserStore<User>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var role1 = new IdentityRole { Name = "Administrator" };
            var role2 = new IdentityRole { Name = "Moderator" };
            var role3 = new IdentityRole { Name = "User" };
         

            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);
         

            string password = "123Qwerty";

            var adminUser = new User() { Email = "admin@forum.com", UserName = "Administrator"};
            var moderatorUser = new User() { Email = "moderator@forum.com", UserName = "Moderator" };
            var userUser = new User() { Email = "user@forum.com", UserName = "User" };

            var result1 = userManager.Create(adminUser, password);
            var result2 = userManager.Create(moderatorUser, password);
            var result3 = userManager.Create(userUser, password);
 

            if (result1.Succeeded)
            {
                userManager.AddToRole(adminUser.Id, role1.Name);
                userManager.AddToRole(adminUser.Id, role2.Name);
                userManager.AddToRole(adminUser.Id, role3.Name);
            }
            if (result2.Succeeded)
            {
                userManager.AddToRole(moderatorUser.Id, role2.Name);
                userManager.AddToRole(moderatorUser.Id, role3.Name);
            }
            if (result3.Succeeded)
            {
                userManager.AddToRole(userUser.Id, role3.Name);
            }

            db.SaveChanges();
        }

    }
}
