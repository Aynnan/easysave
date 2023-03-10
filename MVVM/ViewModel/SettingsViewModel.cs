using EasySave.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.MVVM.ViewModel
{
    internal class SettingsViewModel
    {
        Extensions extModel;
        List<string> extensions;
        ProcessP procModel;
        List<string> processors;
        public SettingsViewModel()
        {
            extModel= new Extensions();
            extensions = extModel.Get();
            procModel = new ProcessP();
            processors = procModel.GetP();
        }
        public void AddExt(string ext)
        {
            extModel.AddExt(ext);
        }

        public void RemoveExt(int index) {
            extModel.RemoveExt(index);
            

        }
        public void AddProc(string proc)
        {
            procModel.AddProc(proc);
        }

        public void RemoveProc(int index)
        {
            procModel.RemoveProc(index);


        }

        public void Refresh()
        {
            extensions = extModel.Get();
        }
        public void RefreshP()
        {
            processors = procModel.GetP();
        }
        public List<string> Get()
        {
            Refresh();
            return extensions;
        }
        public List<string> GetP()
        {
            RefreshP();
            return processors;
        }
    }
}
