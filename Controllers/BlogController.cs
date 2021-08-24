using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC.Controllers
{
    // Route can also be defined at the controller level. All action methods
    // within this controller will need to be preceded by 'blog/' to be accessed.
    [Route("blog")]
    public class BlogController : Controller
    {
        // Action methods with no custom route will not be affected by the custom
        // route at the contoller level. To use the controller level prefix, 
        // specify a custom route with an empty string.
        [Route("")]
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

        // A custom route is defined to take a URL pattern specific to this method.
        // Additional constraints can be added to further define how the URL is entered.
        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            
            return new ContentResult
            {
                Content = string.Format("Year: {0}; Month: {1}; Key {2}", year, month, key)
            };
        }
    }
}
