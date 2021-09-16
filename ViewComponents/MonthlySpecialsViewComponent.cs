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
        // Declare a public method named Invoke that returns a string.
        public string Invoke()
        {
            return "TODO: Show monthly specials";
        }
    }
}
