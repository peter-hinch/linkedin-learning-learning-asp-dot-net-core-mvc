using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC.Models
{
    public class FormattingService
    {
        // This is an "injectable service" which can be used throughout razor
        // views in this project. See _Post partial view.
        public string AsReadableDate(DateTime date)
        {
            return date.ToString("d");
        }
    }
}
