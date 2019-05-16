using System;
using SocialNetworkGruppe8.Models;
using SocialNetworkGruppe8.Services;
namespace SocialNetworkGruppe8
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new SvendService();
            s.Create(new User());
            foreach (var user in s.Get())
            {
                Console.WriteLine(user.Id);
            }

            Console.ReadKey();
        }
    }
}
