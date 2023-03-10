using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using EasySave.MVVM.Model;
using EasySave.Languages;
namespace EasySave.MVVM.ViewModel
{
    internal class CreateViewModel
    {
        public void Create(string name, string source, string target,string type)
        {
            

            Work work = new(name,source, target, type);

            new Works().Add(work);   


        }
    }
}
