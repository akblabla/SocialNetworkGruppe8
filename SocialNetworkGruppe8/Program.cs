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
            var userList = service.Get();
            bool isInThere = false;


            Console.WriteLine("Wellcome to the SvendDB console interface");
            Console.WriteLine("Your choice of commands are:" +
                              "\n\"1\" : Create user" +
                              "\n\"2\" : Have a user follow another user" +
                              "\n\"3\" : Have a user block another user" +
                              "\n\"4\" : Have a user add anther user to its cirkles" +
                              "\n\"5\" : Have a user create a post" +
                              "\n\"6\" : Have a user create a comment to a post" +
                              "\n\"7\" : Have a user get another users wall" +
                              "\n\"8\" : Have a user get its feed");
            do
            {
                try
                {
                    Console.Write(" ");
                    var command = Console.ReadLine();
                    switch (command)
                    {
                        case "1":
                            Console.WriteLine("What is the users name?");
                            string name = Console.ReadLine();
                            Console.WriteLine("What is the users gender? Male, Female or Other?(M/F/O)");
                            char gender = Convert.ToChar(Console.ReadLine());
                            Console.WriteLine("What is the users age?");
                            int age = Convert.ToInt32(Console.ReadLine());
                            var userInTheMaking = new User() { UserCircle = new List<string>(), Name = name, Gender = gender, Age = age };
                            service.Create(userInTheMaking);
                            break;

                        case "2":
                            Console.WriteLine("What is the name of the user who wants to follow someone?");
                            string followerName = Console.ReadLine();
                            Console.WriteLine("What is the name of the user that is to be followed?");
                            string followedName = Console.ReadLine();

                            User UserToBeFollowed = new User();
                            isInThere = false;
                            foreach (var user in service.Get())
                            {
                                if (user.Name == followedName)
                                {
                                    UserToBeFollowed = user;
                                    isInThere = true;
                                    break;
                                }
                            }

                            if (!isInThere)
                            {
                                Console.WriteLine("User does not exist!");
                                break;
                            }
                            isInThere = false;
                            UserToBeFollowed.Followers.Add(userList.Where(u => u.Name == followerName).FirstOrDefault().Id);
                            break;
                            
                        case "3":
                            Console.WriteLine("What is the name of the user who wants to block someone?");
                            string blockerName = Console.ReadLine();
                            Console.WriteLine("What is the name of the user that is to be blocked?");
                            string blockededName = Console.ReadLine();

                            User UserToBlock = new User();
                            isInThere = false;
                            foreach (var user in service.Get())
                            {
                                if (user.Name == blockerName)
                                {
                                    UserToBlock = user;
                                    isInThere = true;
                                    break;
                                }
                            }

                            if (!isInThere)
                            {
                                Console.WriteLine("User does not exist!");
                                break;
                            }
                            isInThere = false;
                            UserToBlock.BlockedUsers.Add(userList.Where(u => u.Name == blockededName).FirstOrDefault().Id);
                            break;

                        case "4":
                            Console.WriteLine("What is the name of the user who wants add someone to its cirkle?");
                            string CirkleOwnerName = Console.ReadLine();
                            Console.WriteLine("What is the name of the user that is to be added?");
                            string SoonToBeInCirkleName = Console.ReadLine();
                            userList = service.Get();
                            
                            User UserToAddToCirkle = new User();
                            isInThere = false;
                            foreach (var user in service.Get())
                            {
                                if (user.Name == CirkleOwnerName)
                                {
                                    UserToAddToCirkle = user;
                                    isInThere = true;
                                    break;
                                }
                            }

                            if (!isInThere)
                            {
                                Console.WriteLine("User does not exist!");
                                break;
                            }
                            isInThere = false;
                            UserToAddToCirkle.UserCircle.Add(userList.Where(u => u.Name == SoonToBeInCirkleName).FirstOrDefault().Id);
                            break;

                        case "5":
                            Console.WriteLine("What is the name of the user that is going to create a post?");
                            string post = Console.ReadLine();


                            break;

                        case "6":
                            break;

                        case "7":
                            break;

                        case "8":
                            break;

                        default:
                            Console.WriteLine("Unknown command");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid database request" +
                                      "\nWant to review exeption?" +
                                      "\ny/N");
                    string answer = Console.ReadLine();
                    if (answer == "y" || answer == "Y")
                        Console.WriteLine(e);
                    else if (answer == "n" || answer == "N" || answer == "") { }
                    else
                    {
                        Console.WriteLine("Invalid command, try again");
                    }

                }

            } while (true);
            /*
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
            userList.Clear();
            foreach (var user in service.Get())
            {
                Console.WriteLine(user.Name);
                userList.Add(user);
            }

            service.Create(userList.Where(u => u.Name == "Peter").FirstOrDefault().Id, new Post() { IsPublic = true, Text = "Public post", UserId = userList.Where(u => u.Name == "Peter").FirstOrDefault().Id });
            service.Create(userList.Where(u => u.Name == "Peter").FirstOrDefault().Id, new Post() { IsPublic = false, Text = "Private post", UserId = userList.Where(u => u.Name == "Peter").FirstOrDefault().Id });


            var postList = new List<Post>();
            foreach (var postAndcomments in service.GetWall(userList.Where(u => u.Name == "Peter").FirstOrDefault().Id, userList.Where(u => u.Name == "Silas").FirstOrDefault().Id))
            {
                Console.WriteLine("Silas can see post: " + postAndcomments.Item1.Text);
                postList.Add(postAndcomments.Item1);
            }

            foreach (var postAndcomments in service.GetWall(userList.Where(u => u.Name == "Peter").FirstOrDefault().Id, userList.Where(u => u.Name == "Felix").FirstOrDefault().Id))
            {
                Console.WriteLine("Felix can see post: " + postAndcomments.Item1.Text);
                postList.Add(postAndcomments.Item1);
            }

            service.Create(postList.FirstOrDefault().Id, new Comment() { Text = "My Comment"});
            foreach (var postAndcomments in service.GetWall(userList.Where(u => u.Name == "Peter").FirstOrDefault().Id, userList.Where(u => u.Name == "Silas").FirstOrDefault().Id))
            {

                foreach (var comment in postAndcomments.Item2)
                {
                    Console.WriteLine("Silas can see comments for post: " + comment.Text);
                }
            }

            Console.ReadKey();
            */
        }
    }
}
