using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.MVVM.Model
{
    internal class ProcessJSON
    {
        string file = "";
        public ProcessJSON() {
            file = AppContext.BaseDirectory;
            for (int i = 0; i <= 3; i++)
            {
                file = Path.GetDirectoryName(file) ?? file;
            }
            file += "/Logs/JSON/Process.json";
        }
        public List<string> GetP()
        {
            string json = File.ReadAllText(file);
            List<string> proc = JsonConvert.DeserializeObject<List<string>>(json) ?? new();
            return proc;
        }

        public void SetP(List<string> proc) {
            string json = JsonConvert.SerializeObject(proc, Formatting.Indented);
            File.WriteAllText(file, json);

        }

    }
}
