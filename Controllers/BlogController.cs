using LearningAspDotNetCoreMVC.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly BlogDataContext _db;

        // Insert an instance of the database context into the BlogController
        // constructor.
        public BlogController(BlogDataContext db)
        {
            _db = db;
        }
        
        // Action methods with no custom route will not be affected by the custom
        // route at the contoller level. To use the controller level prefix, 
        // specify a custom route with an empty string.
        [Route("")]
        public IActionResult Index(int page = 0)
        {
            // Declaration of variables to facilitate pagination of Index.
            var pageSize = 2;
            var totalPosts = _db.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            // Properties to pass to the ViewBag to define pagination behaviour.
            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            // Obtain posts from the database for display.
            var posts =
                _db.Posts
                    .OrderByDescending(x => x.Posted)
                    .Skip(pageSize * page)
                    .Take(pageSize)
                    .ToArray();

            // Check whether the page was called via an AJAX request. If so, 
            // render only the partial view (new content). This prevents the
            // shared layout from being rendered again inside the page.
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView(posts);
            }

            return View(posts);
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
            var post = _db.Posts.FirstOrDefault(x => x.Key == key);

            // Creating a new instance of the Post class allows us to add the same data
            // as previously added using ViewBag, but it is now strongly typed.
            /*
            var post = new Post
            {
                Title = "My blog post",
                Posted = DateTime.Now,
                Author = "Jess Chadwick",
                Body = "This is a great blog post, don't you think?"
            };
            */

            // ViewBag allows content to be passed into views as key value pairs within
            // a ViewBag dynamic object. This is convenient, but is not strongly typed. 
            /*
            ViewBag.Title = "My Blog Post";
            ViewBag.Posted = DateTime.Now;
            ViewBag.Author = "Jess Chadwick";
            ViewBag.Body = "This is a great blog post, don't you think?";
            */

            /*
            return new ContentResult
            {
                Content = string.Format("Year: {0}; Month: {1}; Key {2}", year, month, key)
            };
            */

            return View(post);
        }

        // Adding the decorator keyword HttpGet signifies this action method will
        // handle GET requests to this method. The parameters will still need to 
        // be different for each action method for the application to compile.
        // The Authorize decorator ensures only authorized users can access the method.
        [Authorize]
        [HttpGet, Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        // Adding the decorator keyword HttpPost signifies this action method will
        // handle POST requests to this method. The parameters will still need to 
        // be different for each action method for the application to compile.
        /*
        [HttpPost, Route("create")]
        public IActionResult Create(CreatePostRequest post )
        {
            return View();
        }

        // The CreatePostRequest class defines the Post fields to be used when 
        // creating a new post.
        public class CreatePostRequest
        {
            public string Title { get; set; }
            public string Body { get; set; }
        }
        */

        // Alternatively you can whitelist properties of an object to be bound 
        // to that instance when it is created.
        /*
        [HttpPost, Route("create")]
        public IActionResult Create([Bind("Title", "Body")] Post post)
        {
            return View();
        }
        */

        // Another alternative is to explicitly set the fields you no not want
        // passed in from the user.
        // The Authorize decorator ensures only authorized users can access the method.
        [Authorize]
        [HttpPost, Route("create")]
        public IActionResult Create(Post post)
        {
            // Conditional logic can be added to determine whether the model
            // state is valid, and how to proceed.
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Overwrite the fields that we do not wish the user to set themselves.
            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            _db.Posts.Add(post);
            _db.SaveChanges();

            return RedirectToAction("Post", "Blog", new
            {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key
            });
        }
    }
}
