using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeckleRobotClient.UI
{
    // getting MissingMethodException when upgrading SpeckleCore, so just pasting this in here for now
    public class SpeckleError
    {
        public string Message { get; set; }
        public string Details { get; set; }
    }
    public class SpeckleConversionError : SpeckleError
    {
        public object SourceObject { get; set; }
    }
}
