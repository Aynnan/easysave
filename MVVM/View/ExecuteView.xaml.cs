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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

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

            foreach (var work in works)
            {
                var stackPanel = new StackPanel();

                var textBlock = new TextBlock()
                {
                    Text = $"{work.sName} :\nSource :{work.sSource}\nTarget : {work.sTarget}\nType : {work.sType}",
                    Margin = new Thickness(0, 0, 10, 0)
                };

                var playButton = new Button()
                {
                    Content = "Play",
                    Width = 75,
                    Margin = new Thickness(0, 0, 10, 0),
                    Tag = $"{listWorks.Children.Count}"
                };
                playButton.Click += PlayButton_Click;

                var pauseButton = new Button()
                {
                    Content = "Pause",
                    Width = 75,
                    Margin = new Thickness(0, 0, 10, 0),
                    Visibility = Visibility.Collapsed,
                    Tag = $"{listWorks.Children.Count}"
                };
                pauseButton.Click += PauseButton_Click;

                var stopButton = new Button()
                {
                    Content = "Stop",
                    Width = 75,
                    Margin = new Thickness(0, 0, 10, 0),
                    Visibility = Visibility.Collapsed,
                    Tag = $"{listWorks.Children.Count}"
                };
                stopButton.Click += StopButton_Click;

                stackPanel.Children.Add(textBlock);
                stackPanel.Children.Add(playButton);
                stackPanel.Children.Add(pauseButton);
                stackPanel.Children.Add(stopButton);

                listWorks.Children.Add(stackPanel);
            }
        }


        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string tag = button.Tag?.ToString() ?? "";
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


        private void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int numberOfTasks = exec.Works.Count;

            foreach (var work in exec.Works)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                work.IsRunning = true;
                int taskIndex = exec.Works.IndexOf(work); // Stocke l'index de la tâche en cours d'exécution

                // Mettre à jour l'affichage du bouton Pause et Stop pour la tâche en cours
                Dispatcher.Invoke(() =>
                {
                    var stackPanel = (StackPanel)listWorks.Children[taskIndex];
                    var pauseButton = (Button)stackPanel.Children[2];
                    var stopButton = (Button)stackPanel.Children[3];
                    pauseButton.Visibility = Visibility.Visible;
                    stopButton.Visibility = Visibility.Visible;
                });

                exec.Execute(taskIndex); // Utilise l'index stocké pour exécuter la tâche
                work.IsRunning = false;

                // Mettre à jour la barre de progression dans le thread principal
                int progress = (exec.Works.IndexOf(work) + 1) * 100 / numberOfTasks;
                worker.ReportProgress(progress);

                // Mettre à jour l'affichage du bouton Pause et Stop pour la tâche terminée
                Dispatcher.Invoke(() =>
                {
                    var stackPanel = (StackPanel)listWorks.Children[taskIndex];
                    var pauseButton = (Button)stackPanel.Children[2];
                    var stopButton = (Button)stackPanel.Children[3];
                    pauseButton.Visibility = Visibility.Collapsed;
                    stopButton.Visibility = Visibility.Collapsed;
                });
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


        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var taskIndex = int.Parse(button.Tag.ToString());
            if (isPaused)
            {
                exec.Resume(taskIndex);
                worker.RunWorkerAsync();
                isPaused = false;
            }
            else
            {
                exec.Pause(taskIndex);
                worker.CancelAsync();
                isPaused = true;
            }
        }


        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var taskIndex = int.Parse(button.Tag.ToString());
            exec.Stop(taskIndex);
            worker.CancelAsync();
        }



    }
}