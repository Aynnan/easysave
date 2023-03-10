
using System;
using System.ComponentModel;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using EasySave.Languages;
using EasySave.MVVM.View;
using EasySave.MVVM.ViewModel;
using System.Threading;

namespace EasySave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private static bool IsSingleInstance()
        {
            bool createdNew;
            var mutex = new Mutex(true, "EasySave", out createdNew);
            return createdNew;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public MainWindow()
        {
            if (!IsSingleInstance())
            {
                MessageBox.Show("L'application est déjà en cours d'exécution.");
                return;
            }
            InitializeComponent();
            DataContext = new MainViewModel();
            cLanguage.SetLanguage("en-US");
            
        }
        private void Radio_Exit(object sender, RoutedEventArgs e)
        {
            // Code à exécuter lorsque le bouton est cliqué
            Application.Current.Shutdown();

        }
        private void French_Click(object sender, RoutedEventArgs e)
        {
            // Changer la langue de l'application en français
            cLanguage.SetLanguage("fr-FR");

            // Mettre à jour le contenu des boutons avec les traductions en français
            Traduction();

            MessageBox.Show("Le changement de langue a été effectué.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        private void English_Click(object sender, RoutedEventArgs e)
        {
            // Changer la langue de l'application en français
            cLanguage.SetLanguage("en-US");
            Traduction();

            MessageBox.Show("The language has been change succesfuly.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);



        }


        private void Traduction()
        {
            // ContentDisplay.Content = new MainWindow();
            HomeRadioButton.Content = EasySave.Languages.cLanguage.GetString("Home");
            CreateBackupRadioButton.Content = EasySave.Languages.cLanguage.GetString("Create a backup");
            ExecuteBackupRadioButton.Content = EasySave.Languages.cLanguage.GetString("Execute a backup");
            ModifyBackupRadioButton.Content = EasySave.Languages.cLanguage.GetString("Modify a backup");
            FrenchRadioButton.Content = EasySave.Languages.cLanguage.GetString("Français");
            EnglishRadioButton.Content = EasySave.Languages.cLanguage.GetString("Englisht");
            ExitRadioButton.Content = EasySave.Languages.cLanguage.GetString("Exit");
            HomeView homeView = new HomeView();
            homeView.Welcome.Text = cLanguage.GetString("Welcome");
            homeView.GetStarted.Text = cLanguage.GetString("Get started today");
            CreateView discoView = new CreateView();
            discoView.Nom.Text = cLanguage.GetString("Enter a name :");
            discoView.Source.Text = cLanguage.GetString("Enter a file source :");
            discoView.Target.Text = cLanguage.GetString("Enter a file target :");
            discoView.choosesave.Text = cLanguage.GetString("Choose a type of save :");
            discoView.complete.Content = cLanguage.GetString("Complete");
            discoView.differential.Content = cLanguage.GetString("Differential");
            discoView.CreateButtonEnd.Content = cLanguage.GetString("CreateButton");
            discoView.searchsource.Content = cLanguage.GetString("Search");
            discoView.searchtarget.Content = cLanguage.GetString("Search");
            discoView.PleaseComplete.Text = cLanguage.GetString("Please complete the informations below :");
            ExecuteView exeView = new ExecuteView();
            exeView.PleaseExecute.Text = cLanguage.GetString("Please choose a backup to execute :");
            ModifyView modiView = new ModifyView();
            modiView.PleaseModify.Text = cLanguage.GetString("Please choose a backup to modify :");
        }

        private void HomeRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyBackupRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}

