using System;
using System.IO;
using System.Xml.Serialization;

namespace EasySave.MVVM.Model
{
    class DailyXML
    {
        string file = "";
        public DailyXML() {
            file = AppContext.BaseDirectory;
            for(int i=0;i<=3;i++) {
                file = Path.GetDirectoryName(file) ?? file;
            }
            file += "/Logs/XML/Routinelogs.xml";
        }  

        public void AddDaily(Timestamp timestamp)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Timestamp));

            StringWriter stringWriter = new StringWriter();
            serializer.Serialize(stringWriter , timestamp);
            string xml = stringWriter.ToString();

            using (StreamWriter writer = new StreamWriter(file, true))
            {
                writer.WriteLine(xml);
                serializer.Serialize(stringWriter , timestamp);
                writer.Flush();
                writer.Close();

            }
        }
    }
}
