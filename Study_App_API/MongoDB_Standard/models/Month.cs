using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_Standard.models {

    public class Month {

        public List<Day> Days { get; set; }
        public int MonthOfYear { get; set; }

        public string MonthName {
            get {
                switch (MonthOfYear) {
                    case 0:
                        return "January";
                    case 1:
                        return "February";
                    case 2:
                        return "March";
                    case 3:
                        return "April";
                    case 4:
                        return "May";
                    case 5:
                        return "June";
                    case 6:
                        return "July";
                    case 7:
                        return "August";
                    case 8:
                        return "September";
                    case 9:
                        return "October";
                    case 10:
                        return "November";
                    case 11:
                        return "December";
                    default:
                        return null;
                }
            }
        }
    }
}