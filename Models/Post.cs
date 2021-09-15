using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC.Models
{
    // The Post class defines an object with strongly typed data that can be used
    // by the BlogController for placement in the Post view.
    public class Post
    {
        public long Id { get; set; }

        private string _key;
        
        // Define an easily readable key string that can be used to identify a
        // post. Here regex is used to modify the title so it can be used for 
        // the 'Key' property.
        public string Key
        {
            get
            {
                if (_key == null)
                {
                    _key = Regex.Replace(Title.ToLower(), "[^a-z0-9]", "-");
                }
                return _key;
            }
            set { _key = value; }
        }
        
        // The name displayed in a label for the form input can be customised
        // using the [Display(Name = "XXX")] decorator
        [Display(Name = "Post Title")]
        // Data validation can be applied using decorators above fields. 
        // They require the use of System.ComponentModel.DataAnnotations
        [Required]
        [StringLength(100, MinimumLength = 5,
            ErrorMessage = "Title must be between 5 and 100 characters long.")]
        // The datatype can also be specified using the [DataType(DataType.XXX)]
        // decorator.
        [DataType(DataType.Text)]
        public string Title { get; set; }
        
        public string Author { get; set; }

        [Required]
        [MinLength(100, ErrorMessage = "Blog posts must be at least 100 characters long.")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        
        public DateTime Posted { get; set; }
    }
}
