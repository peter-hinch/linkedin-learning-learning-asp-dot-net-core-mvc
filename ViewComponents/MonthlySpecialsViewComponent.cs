using LearningAspDotNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC.ViewComponents
{
    // To define a ViewComponent the class must satisfy at least one of the
    // following conditions:
    // - Name of the class must end with the suffix ViewComponent
    // - Class must have the ViewComponent decorator
    // - Extend from the ViewComponent base class
    [ViewComponent]
    public class MonthlySpecialsViewComponent : ViewComponent
    {
        // Inject the BlogDataContext into MonthlySpecialsViewComponent
        private readonly BlogDataContext db;

        public MonthlySpecialsViewComponent(BlogDataContext db)
        {
            this.db = db;
        }
        
        // Declare a public method named Invoke that returns an IViewComponent
        // result.
        public IViewComponentResult Invoke()
        {
            // Populate the view with the specials contained in BlogDataContext.
            var specials = db.MonthlySpecials.ToArray();
            return View(specials);
        }
    }
}
