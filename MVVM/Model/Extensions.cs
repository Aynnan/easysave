using System.Collections.Generic;

namespace EasySave.MVVM.Model
{
    internal class Extensions
    {
        public string ext;
        public Extensions() { }
        public Extensions(string ext) { this.ext = ext;} 
        public List<string> Get()
        {
            List<string> list ;
            list = new ExtensionsJSON().Get();
            return list;
        }

        public void AddExt(string ext) { 
            List<string> list =Get() ;
            list.Add(new string(ext));
            new ExtensionsJSON().Set(list);

        }

        public void RemoveExt(int index) { 
            List<string> list =Get() ;
            list.Remove(list[index]);
            new ExtensionsJSON().Set(list); 

        }
    }
}
