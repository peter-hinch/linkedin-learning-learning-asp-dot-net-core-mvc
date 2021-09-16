using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC.Models
{
    // Define the MonthlySpecial object to be used in the MonthlySpecialsViewComponent.
    public class MonthlySpecial
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
    }
}
