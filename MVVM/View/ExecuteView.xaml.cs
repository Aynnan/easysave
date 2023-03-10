using EasySave.Languages;
using EasySave.MVVM.Model;
using EasySave.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EasySave.MVVM.View
{
    public partial class ExecuteView : UserControl
    {
        private readonly ExecuteViewModel exec;
        private readonly BackgroundWorker worker;
        private bool isPaused = false;

        public int SelectedTask { get; set; }

        public ExecuteView()
        {
            InitializeComponent();
            PleaseExecute.Text = cLanguage.GetString("Please choose a backup to execute :");
            exec = new();
            FillList();
            worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            worker.DoWork += DoWork;
            worker.ProgressChanged += ProgressChanged;
            worker.RunWorkerCompleted += RunWorkerCompleted;
        }

        private void FillList()
        {
            List<Work> works = exec.Works;

            if (works.Count == 0)
            {
                var textBox = new TextBox()
                {
                    FontSize = 30,
                    Text = cLanguage.GetString("NoWorkCreated"),
                    Background = Brushes.Transparent,
                    Foreground = Brushes.White,
                    BorderThickness = new Thickness(0)
                };
                listWorks.Children.Add(textBox);
                return;
            }

            var listBox = new ListBox();
            foreach (var work in works)
            {
                var newItem = new ListBoxItem()
                {
                    Content = $"{work.sName} :\nSource :{work.sSource}\nTarget : {work.sTarget}\nType : {work.sType}",
                    Tag = $"{listBox.Items.Count}"
                };
                newItem.PreviewMouseDoubleClick += ListBoxItem_MouseDoubleClick;
                listBox.Items.Add(newItem);
            }
            listWorks.Children.Add(listBox);
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null)
            {
                string tag = item.Tag?.ToString() ?? "";
                int.TryParse(tag, out int selectedTask);
                SelectedTask = selectedTask;

                StartTasks(sender, e);
            }
        }

        private async void StartTasks(object sender, RoutedEventArgs e)
        {
            if (SelectedTask < 0 || SelectedTask >= exec.Works.Count)
            {
                MessageBox.Show("Veuillez sélectionner une tâche valide.");
                return;
            }

            int numberOfTasks = exec.Works.Count;

            ProgressBarrStatus.Value = 0;
            ProgressBarrStatus.Maximum = numberOfTasks;

            // On stocke la valeur de selectedTask dans une variable locale pour éviter les problèmes de race condition
            int taskIndex = SelectedTask;

            worker.DoWork -= DoWork;
            worker.DoWork += DoWork;

            try
            {
                await Task.Run(() => worker.RunWorkerAsync());
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Les tâches ont été annulées.");
            }
            finally
            {
                if (!worker.IsBusy)
                {
                    if (worker.CancellationPending)
                    {
                        MessageBox.Show("Les tâches ont été annulées.");
                    }
                    else
                    {
                        MessageBox.Show("Toutes les tâches sont terminées.");
                    }
                }
            }
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int numberOfTasks = exec.Works.Count;

            for (int i = 0; i < numberOfTasks; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                int taskIndex = i; // Stocke l'index de la tâche en cours d'exécution

                exec.Execute(taskIndex); // Utilise l'index stocké pour exécuter la tâche
                int progress = (i + 1) * 100 / numberOfTasks;
                worker.ReportProgress(progress);
            }
        }


        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Mettre à jour la barre de progression dans le thread principal
            Dispatcher.Invoke(() =>
            {
                ProgressBarrStatus.Value = e.ProgressPercentage;
                lb_etat_prog_server.Content = $"{e.ProgressPercentage} / {100}";
            });
        }


        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Les tâches ont été annulées.");
            }
            else if (e.Error != null)
            {
                MessageBox.Show($"Une erreur s'est produite : {e.Error.Message}");
            }
            else
            {
                MessageBox.Show("Toutes les tâches sont terminées.");
            }
        }

        private void PauseTasksButton_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }

        private void StopTasksButton_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }

        private void ResumeTasksButton_Click(object sender, RoutedEventArgs e)
        {
            worker.RunWorkerAsync();
        }
    }
}