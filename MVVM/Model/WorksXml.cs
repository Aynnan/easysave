using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace EasySave.MVVM.Model
{
    class WorksXml
    {
        string file;

        public WorksXml()
        {
            file = AppContext.BaseDirectory;
            for (int i = 0; i <= 3; i++)
            {
                file = Path.GetDirectoryName(file) ?? file;
            }
            file += "/Logs/XML/Works.xml";
        }

        public List<Work> get()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Work>));
            List<Work> works;
            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                works = serializer.Deserialize(fs) as List<Work> ?? new List<Work>();
            }

            return works;
        }

        public void set(List<Work> works)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Work>));

            using (FileStream fs = new FileStream(file, FileMode.Truncate))
            {
                serializer.Serialize(fs, works);
            }
        }

    }
}
