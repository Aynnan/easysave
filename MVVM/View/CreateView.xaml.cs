
using EasySave.Languages;
using EasySave.MVVM.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace EasySave.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour CreateView.xaml
    /// </summary>
    public partial class CreateView : System.Windows.Controls.UserControl
    {

        public string mName;
        
        public CreateView()
        {
            InitializeComponent();
            Nom.Text = cLanguage.GetString("Enter a name :");
            Source.Text = cLanguage.GetString("Enter a file source :");
            Target.Text = cLanguage.GetString("Enter a file target :");
            choosesave.Text = cLanguage.GetString("Choose a type of save :");
            complete.Content = cLanguage.GetString("Complete");
            differential.Content = cLanguage.GetString("Differential");
            CreateButtonEnd.Content = cLanguage.GetString("CreateButton");
            searchsource.Content = cLanguage.GetString("Search");
            searchtarget.Content = cLanguage.GetString("Search");
            PleaseComplete.Text = cLanguage.GetString("Please complete the informations below :");
        }
        public void SearchSourceClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFolder = dialog.SelectedPath;
                SourceFolder.Text = selectedFolder;
            }

        }

        public void SearchTargetClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFolder = dialog.SelectedPath;
                TargetFolder.Text = selectedFolder;
            }
        }

        public void Button_Create(object sender, RoutedEventArgs e)
        {
            mName = inputName.Text;
            string source = SourceFolder.Text;
            string target = TargetFolder.Text;
            string selectedTag = string.Empty;

            foreach (System.Windows.Controls.RadioButton radioButton in radioButton.Children.OfType<System.Windows.Controls.RadioButton>())
            {
                if (radioButton.IsChecked == true)
                {
                    selectedTag = radioButton.Tag.ToString() ?? "";
                    break;
                }
            }
            
            if(mName == "" || source == "" || target == "" || selectedTag == "")
            {
                System.Windows.MessageBox.Show("Veuillez remplir tous les champs","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            new CreateViewModel().Create(mName, source, target,selectedTag);
            System.Windows.MessageBox.Show("Travail créé avec succès", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
       
}
