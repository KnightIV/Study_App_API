using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_Standard.models {

    public class UserAccount {

        public List<Goal> ListOfGoals { get; set; }
        public List<Note> ListOfNotes { get; set; }
        public List<File> ListOfFiles { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
