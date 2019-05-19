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
            var s = new SvendService();
            var u1 = new User() {UserCircle = new List<string>()};
            var u2 = new User() { UserCircle = new List<string>() };
            var u3 = new User() { UserCircle = new List<string>() };

            u1.UserCircle.Add(u3.Id);
            u1.Followers.Add(u3.Id);
            u1.Followers.Add(u2.Id);

            s.Create(u1);
            s.Create(u2);
            s.Create(u3);
            var userList = new List<User>();
            foreach (var user in s.Get())
            {
                Console.WriteLine(user.Id);
                userList.Add(user);
            }

            s.Create(userList.FirstOrDefault().Id, new Post() { IsPublic = true, Text = "Public post", UserId = userList.FirstOrDefault().Id });
            s.Create(userList.FirstOrDefault().Id, new Post() { IsPublic = false, Text = "Private post", UserId = userList.FirstOrDefault().Id });


            var postList = new List<Post>();
            foreach (var postAndcomments in s.GetWall(userList.FirstOrDefault().Id, userList.LastOrDefault().Id))
            {
                Console.WriteLine(postAndcomments.Item1.Text);
                postList.Add(postAndcomments.Item1);
            }

            s.Create(postList.FirstOrDefault().Id, "My comment");
            foreach (var postAndcomments in s.GetWall(userList.FirstOrDefault().Id, userList.LastOrDefault().Id))
            {

                foreach (var comment in postAndcomments.Item2)
                {
                    Console.WriteLine(comment.Text);
                }
            }

            Console.ReadKey();
        }
    }
}
