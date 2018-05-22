using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_Standard.models {

    public class Year {

        public List<Month> Months { get; set; }
        public int YearNum { get; set; }

    }
}
