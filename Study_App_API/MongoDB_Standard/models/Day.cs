using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_Standard.models {

    public class Day {

        public List<Goal> Goals { get; set; }
        public int DayOfMonth { get; set; }
        public string DayOfWeek { get; set; }
    }
}
