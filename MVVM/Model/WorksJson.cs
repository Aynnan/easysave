using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;

namespace EasySave.MVVM.Model
{
    class WorksJson
    {
        string file;

        public WorksJson()
        {
            file = AppContext.BaseDirectory;
            for (int i = 0; i <= 3; i++)
            {
                file = Path.GetDirectoryName(file) ?? file;
            }
            file += "/Logs/JSON/Works.json";
        }

        public List<Work> get()
        {
            List<Work> works = new List<Work>();
            string json = File.ReadAllText(file);
            works = JsonConvert.DeserializeObject<List<Work>>(json) ?? new();
            return works;
        }

        public void set(List<Work> works)
        {
            string json = JsonConvert.SerializeObject(works, Formatting.Indented);

            File.WriteAllText(file, json);
        }

    }
}
