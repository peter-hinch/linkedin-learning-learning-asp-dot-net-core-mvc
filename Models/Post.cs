using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC.Models
{
    // The Post class defines an object with strongly typed data that can be used
    // by the BlogController for placement in the Post view.
    public class Post
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime Posted { get; set; }
    }
}
