using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazeItUp.Pre.Shared
{
    public class TimelineElement
    {
        public String Name { get; set; }
        public String Symbol { get; set; }
        public Boolean IsCompleted { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
