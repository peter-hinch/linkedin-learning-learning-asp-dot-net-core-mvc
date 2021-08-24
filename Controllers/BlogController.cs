using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return new ContentResult { Content = "Blog Posts" };
            //return View();
        }

        // Passes the string from the URL in the 'id' portion to the action method:
        // ./blog/post/XXX
        // Alternatively URL can be entered in the following manner:
        // ./blog/post?id=XXX
        // If the parameter passed in is nullable e.g. int? the result will
        // either be a value that belongs to the datatype or else it is null.
        // A default value can also be assigned to be used when the value given
        // does not belong to the specified datatype.
        public IActionResult Post(int? id = -1)
        {
            
            return new ContentResult { Content = id.ToString() };
        }
    }
}
