using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.MVVM.Model
{
    class DailyJSON
    {
        string file = "";
        public DailyJSON() 
        {
            file = AppContext.BaseDirectory;
            for(int i = 0; i<= 3;i++)
            {
                file = Path.GetDirectoryName(file) ?? file;
            }
            file += "/Logs/JSON/RoutineLogs.json";
        }

        public void AddDaily(Timestamp timestamp)
        {
            string json = JsonConvert.SerializeObject(timestamp, Formatting.Indented);

            using (FileStream stream = new FileStream(file, FileMode.Append, FileAccess.Write, FileShare.None))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(json);
                writer.Close();
            }
        }

    }
}
