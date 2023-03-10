using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.MVVM.Model
{
    public class Timestamp
    {
        public DateTime Time { get; set; }
        public string Name { get; set; }

        public string fileSource { get; set; }

        public string fileTarget { get; set; }    

        public long FileSize { get; set; }

        public float SaveTime { get; set; }

        public Timestamp(string saveName, string target,string source, float time, long fileLength)
        {
            Time = DateTime.Now;
            Name = saveName;
            fileSource = source;
            fileTarget = target;
            FileSize = fileLength;
            SaveTime = time;

        }

        public Timestamp() { }
    }
}
