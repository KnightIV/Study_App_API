using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyApp.Assets.Models {

    public class RecurringGoal : Goal {

        public TimeSpan Frequency { get; set; }
    }
}
