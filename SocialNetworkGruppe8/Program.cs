using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkGruppe8.Models;
using SocialNetworkGruppe8.Services;
namespace SocialNetworkGruppe8
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new SvendService();
            var u1 = new User() {UserCircle = new List<string>(), Name = "Peter"};
            var u2 = new User() { UserCircle = new List<string>(), Name = "Felix"};
            var u3 = new User() { UserCircle = new List<string>(), Name = "Silas" };
            var u4 = new User() { UserCircle = new List<string>(), Name = "Thor" };

            service.Create(u2);
            service.Create(u3);
            var userList = new List<User>();
            foreach (var user in service.Get())
            {
                Console.WriteLine(user.Name);
                userList.Add(user);
            }

            u1.UserCircle.Add(userList.Where(u => u.Name == "Silas").FirstOrDefault().Id);
            u1.Followers.Add(userList.Where(u => u.Name == "Silas").FirstOrDefault().Id);
            u1.Followers.Add(userList.Where(u => u.Name == "Felix").FirstOrDefault().Id);
            service.Create(u1);
            //service.Update(userList.Where(u => u.Name == "Peter").FirstOrDefault().Id, u1);

            //
            service.Create(userList.FirstOrDefault().Id, new Post() { IsPublic = true, Text = "Public post", UserId = userList.FirstOrDefault().Id });
            service.Create(userList.FirstOrDefault().Id, new Post() { IsPublic = false, Text = "Private post", UserId = userList.FirstOrDefault().Id });


            var postList = new List<Post>();
            foreach (var postAndcomments in service.GetWall(userList.FirstOrDefault().Id, userList.Where(u => u.Name == "Silas").FirstOrDefault().Id))
            {
                Console.WriteLine("Silas can see post: " + postAndcomments.Item1.Text);
                postList.Add(postAndcomments.Item1);
            }

            foreach (var postAndcomments in service.GetWall(userList.FirstOrDefault().Id, userList.Where(u => u.Name == "Felix").FirstOrDefault().Id))
            {
                Console.WriteLine("Felix can see post: " + postAndcomments.Item1.Text);
                postList.Add(postAndcomments.Item1);
            }

            service.Create(postList.FirstOrDefault().Id, "My comment");
            foreach (var postAndcomments in service.GetWall(userList.FirstOrDefault().Id, userList.Where(u => u.Name == "Silas").FirstOrDefault().Id))
            {

                foreach (var comment in postAndcomments.Item2)
                {
                    Console.WriteLine("Silas can see comments for post: " + comment.Text);
                }
            }

            Console.ReadKey();
        }
    }
}
