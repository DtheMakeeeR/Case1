using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace Case1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProjectSystem ps = new ProjectSystem("users.xml", "tasks.xml");
            ps.Start();
        }
    }
}
