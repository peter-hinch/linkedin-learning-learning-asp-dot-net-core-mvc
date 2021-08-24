using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC
{
    // This class allows the creation of an object to access variables in the
    // appsettings.Development.json and appsetttings.json configuration files.
    public class FeatureToggles
    {
        public bool DeveloperExceptions { get; set; }
    }
}
