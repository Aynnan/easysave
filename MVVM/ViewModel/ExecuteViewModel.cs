using EasySave.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasySave.MVVM.ViewModel
{
    internal class ExecuteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Work> _works;

        public List<Work> Works
        {
            get { return _works; }
            set
            {
                _works = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Work)));
            }
        }

        public ExecuteViewModel()
        {
            _works = new Works().getList();
        }

        public void Execute(int index)
        {
            Work work = _works[index];
            try
            {
                new Transfer(work);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public void Stop(int index)
        {
            Work work = _works[index];
            try
            {
                new Transfer(work).Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Resume(int index)
        {
            Work work = _works[index];
            try
            {
                new Transfer(work).Resume();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Pause(int index)
        {
            Work work = _works[index];
            try
            {
                new Transfer(work).Pause();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
