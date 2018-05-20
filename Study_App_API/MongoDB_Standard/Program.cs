using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB_Standard;
using Study_App_API.MongoDB_Commands;

namespace MongoDB_Standard
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseCommand c = new DatabaseCommand("Study_App");
        }
    }
}
