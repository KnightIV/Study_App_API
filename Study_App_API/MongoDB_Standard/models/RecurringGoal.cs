﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_Standard.models {

    public class RecurringGoal : Goal {

        public TimeSpan Frequency { get; set; }
    }
}