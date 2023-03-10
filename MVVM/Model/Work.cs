using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.MVVM.Model
{
    public class Work
    {

        public string sName, sSource, sTarget, sType;
        public List<string> Tasks { get; set; }
        public Work(string name, string source, string target, string type)
        {
            sName = name;
            sSource = source;
            sTarget = target;
            sType = type;
            Tasks = new List<string>();
        }
        public Work() { }

    }
}