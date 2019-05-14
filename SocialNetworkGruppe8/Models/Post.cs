using System;
using System.Collections.Generic;
using System.Text;
using SocialNetworkGruppe8.Models;
namespace SocialNetworkGruppe8.Models
{
    class Post
    {
        Queue<Comment> comments { get; set; }
    }
}
