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
                              "\n\"2\" : Have a user create a post" +
                              "\n\"3\" : Have a user create a comment to a post" +
                              "\n\"4\" : Have a user get another users wall" +
                              "\n\"5\" : Have a user get its feed");
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

                            var userInTheMaking = new User()
                            {
                                Name = name,
                                Gender = gender,
                                Age = age
                            };

                            Console.WriteLine("how many follows this user?");
                            int forLoop = Convert.ToInt32(Console.ReadLine());

                            for (int i = 0; i < forLoop; i++)
                            {
                                Console.WriteLine("What is the name of a follower?");
                                var ToBeFollower = Console.ReadLine();
                                userList = service.Get();
                                userInTheMaking.Followers.Add(userList.Where(u => u.Name == ToBeFollower).FirstOrDefault().Id);
                                Console.WriteLine("Added!");
                            }

                            Console.WriteLine("how many have this user blocked?");
                            forLoop = Convert.ToInt32(Console.ReadLine());

                            for (int i = 0; i < forLoop; i++)
                            {
                                Console.WriteLine("What is the name of a blocked user?");
                                var ToBeFollower = Console.ReadLine();
                                userList = service.Get();
                                userInTheMaking.BlockedUsers.Add(userList.Where(u => u.Name == ToBeFollower).FirstOrDefault().Id);
                                Console.WriteLine("Added!");
                            }

                            Console.WriteLine("how many are in this users cirkles?");
                            forLoop = Convert.ToInt32(Console.ReadLine());

                            for (int i = 0; i < forLoop; i++)
                            {
                                Console.WriteLine("What is the name of a follower?");
                                var ToBeFollower = Console.ReadLine();
                                userList = service.Get();
                                userInTheMaking.UserCircle.Add(userList.Where(u => u.Name == ToBeFollower).FirstOrDefault().Id);
                                Console.WriteLine("Added!");
                            }

                            service.Create(userInTheMaking);
                            Console.WriteLine("Done!");
                            break;

                        case "2":
                            Console.WriteLine("What is the name of the user that is going to create a post?");
                            string postAuthor = Console.ReadLine();
                            
                            User ToBePostAuthor = new User();
                            isInThere = false;
                            foreach (var user in service.Get())
                            {
                                if (user.Name == postAuthor)
                                {
                                    ToBePostAuthor = user;
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


                            bool notdone = true;
                            bool isPrivate = false;
                            while (notdone)
                            {
                                Console.WriteLine("Is the post going to be public or private?");
                                string input = Console.ReadLine();
                                if (input == "public")
                                {
                                    isPrivate = false;
                                    notdone = false;
                                }
                                else if (input == "private")
                                {
                                    isPrivate = true;
                                    notdone = false;
                                }
                                else
                                {
                                    Console.WriteLine("Try again");
                                }
                            }

                            notdone = true;
                            while (notdone)
                            {
                                Console.WriteLine("Is the post type text or picture?");
                                string input = Console.ReadLine();
                                if (input == "text")
                                {
                                    Console.WriteLine("What is the text in the post?");
                                    input = Console.ReadLine();

                                    TextPost thePost = new TextPost()
                                    {
                                        UserId = ToBePostAuthor.Id,
                                        Author = postAuthor,
                                        IsPublic = !isPrivate,
                                        CreationDate = DateTime.Now,
                                        Text = input
                                    };
                                    service.Create(ToBePostAuthor.Id, thePost);

                                    notdone = false;
                                }
                                else if (input == "picture")
                                {
                                    Console.WriteLine("What is the text in the post?");
                                    string textInput = Console.ReadLine();
                                    Console.WriteLine("What is the URL for the picture in the post?");
                                    string picInput = Console.ReadLine();
                                    PicturePost thePost = new PicturePost()
                                    {
                                        UserId = ToBePostAuthor.Id,
                                        Author = postAuthor,
                                        IsPublic = !isPrivate,
                                        CreationDate = DateTime.Now,
                                        Text = input,
                                        ImageUrl = picInput
                                    };
                                    service.Create(ToBePostAuthor.Id, thePost);
                                    notdone = false;
                                }
                                else
                                {
                                    Console.WriteLine("Try again");
                                }
                            }
                            Console.WriteLine("Done!");
                            break;

                        case "3":

                            Console.WriteLine("What is the name of the user that is going to create a comment?");
                            string commentAuthor = Console.ReadLine();
                            
                            User ToBeCommentAuthor = new User();
                            isInThere = false;
                            foreach (var user in service.Get())
                            {
                                if (user.Name == commentAuthor)
                                {
                                    ToBeCommentAuthor = user;
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

                            Console.WriteLine("What is the author of the post that is going to have a comment?");
                            string postauthor = Console.ReadLine();
                             
                            User ToBePostAuthorer = new User();
                            isInThere = false;
                            foreach (var user in service.Get())
                            {
                                if (user.Name == postauthor)
                                {
                                    ToBePostAuthorer = user;
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

                            Console.WriteLine("Is the post text or picture?");
                            string type = Console.ReadLine();

                            if (type == "text")
                            {
                                TextPost ourpost = new TextPost();
                                isInThere = false;
                                foreach (var post in service.GetPost())
                                {
                                    if (post.Author == postauthor)
                                    {
                                        ourpost = (TextPost)post;
                                        isInThere = true;
                                        break;
                                    }
                                }
                                if (!isInThere)
                                {
                                    Console.WriteLine("Post does not exist!");
                                    break;
                                }
                                isInThere = false;

                                Console.WriteLine("What is the comment text?");
                                string content = Console.ReadLine();

                                Comment ourComment = new Comment()
                                {
                                    Text = content,
                                    PostId = ourpost.Id,
                                    UserId = ToBePostAuthorer.Id
                                };

                                service.Create(ourpost.Id, ourComment);
                            }
                            else if (type == "picture")
                            {
                                PicturePost ourpost = new PicturePost();
                                isInThere = false;
                                foreach (var post in service.GetPost())
                                {
                                    if (post.Author == postauthor)
                                    {
                                        ourpost = (PicturePost)post;
                                        isInThere = true;
                                        break;
                                    }
                                }
                                if (!isInThere)
                                {
                                    Console.WriteLine("Post does not exist!");
                                    break;
                                }
                                isInThere = false;

                                Console.WriteLine("What is the comment text?");
                                string content = Console.ReadLine();

                                Comment ourComment = new Comment()
                                {
                                    Text = content,
                                    PostId = ourpost.Id,
                                    UserId = ToBePostAuthorer.Id
                                };

                                service.Create(ourpost.Id, ourComment);
                            }
                            Console.WriteLine("Done!");
                            break;

                        case "4":
                             
                            Console.WriteLine("What is the user that is getting another users wall?");
                            string wallWanter = Console.ReadLine();
                            
                            User ToBewallWanter = new User();
                            isInThere = false;
                            foreach (var user in service.Get())
                            {
                                if (user.Name == wallWanter)
                                {
                                    ToBewallWanter = user;
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

                            Console.WriteLine("What is the user whose wall we are going to get?");
                            string wallOwner = Console.ReadLine();
                            
                            User ToBewallOwner = new User();
                            isInThere = false;
                            foreach (var user in service.Get())
                            {
                                if (user.Name == wallOwner)
                                {
                                    ToBewallOwner = user;
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

                            IEnumerable<Tuple<Post, IEnumerable<Comment>>> wall = service.GetWall(ToBewallOwner.Id, ToBewallWanter.Id);
                            foreach (var instance in wall)
                            {
                                Console.WriteLine("Post created at " + instance.Item1.CreationDate + " by " + instance.Item1.Author + " can be seen");
                                Console.WriteLine("With the comment(s):");
                                foreach (var comment in instance.Item2)
                                {
                                    Console.WriteLine(comment.Text);
                                }
                            }  

                            break;

                        case "5":
                            
                            Console.WriteLine("What is the user whose feed we are going to see?");
                            string FeedOwner = Console.ReadLine();
                            
                            User ToBeFeedOwner = new User();
                            isInThere = false;
                            foreach (var user in service.Get())
                            {
                                if (user.Name == FeedOwner)
                                {
                                    ToBeFeedOwner = user;
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

                            foreach (var post in ToBeFeedOwner.Feed)
                            {
                                Console.WriteLine("Post created at " + post.CreationDate + " by " + post.Author + " can be seen");
                            }

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
        }
    }
}
