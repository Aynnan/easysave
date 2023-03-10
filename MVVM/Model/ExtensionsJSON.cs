using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.MVVM.Model
{
    internal class ExtensionsJSON
    {
        string file = "";
        public ExtensionsJSON() {
            file = AppContext.BaseDirectory;
            for (int i = 0; i <= 3; i++)
            {
                file = Path.GetDirectoryName(file) ?? file;
            }
            file += "/Logs/JSON/Extensions.json";
        }
        public List<string> Get()
        {
            string json = File.ReadAllText(file);
            List<string> exts = JsonConvert.DeserializeObject<List<string>>(json) ?? new();
            return exts;
        }

        public void Set(List<string> exts) {
            string json = JsonConvert.SerializeObject(exts, Formatting.Indented);
            File.WriteAllText(file, json);

        }

    }
}
