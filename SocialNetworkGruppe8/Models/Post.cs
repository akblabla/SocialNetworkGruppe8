using System;
using System.Collections.Generic;
using System.Text;
using SocialNetworkGruppe8.Models;
namespace SocialNetworkGruppe8.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public bool IsPublic { get; set; }
    }
}
