using System.Collections.Generic;

namespace EasySave.MVVM.Model
{
    internal class ProcessP
    {
        public string proc;
        public ProcessP() { }
        public ProcessP(string proc) { this.proc = proc;} 
        public List<string> GetP()
        {
            List<string> list ;
            list = new ProcessJSON().GetP();
            return list;
        }

        public void AddProc(string proc) { 
            List<string> list =GetP() ;
            list.Add(new string(proc));
            new ProcessJSON().SetP(list);

        }

        public void RemoveProc(int index) { 
            List<string> list =GetP() ;
            list.Remove(list[index]);
            new ProcessJSON().SetP(list); 

        }
    }
}
