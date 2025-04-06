using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace Case1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string uPath = "users.xml", tPath = "tasks.xml";
            if (args.Length >= 2) 
            {
                uPath = args[0];
                tPath = args[1];
            }
            ProjectSystem ps = new ProjectSystem(uPath, tPath);
            ps.Start();
        }
    }
}
