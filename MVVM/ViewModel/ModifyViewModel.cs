using EasySave.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EasySave.Languages;

namespace EasySave.MVVM.ViewModel
{
    internal class ModifyViewModel
    {
        public List<Work> _works;
        public ModifyViewModel ()
        {
            _works = new Works().getList();
        }

        public void Modify(int index,string name, string source,string target,string type)
        {
            Work work = new(name, source, target, type);
            
            new Works().Modify(work, index);

        }
    }
}
