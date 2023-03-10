using EasySave.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EasySave.MVVM.ViewModel;

namespace EasySave.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour HomeView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        SettingsViewModel settings;
        SettingsViewModel procset;
        public SettingsView()
        {
            InitializeComponent();
            settings = new();
            procset = new();
            UpdateExtensionsList();
            UpdateProcessList();


        }

        
        private void SettingsView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Si la vue est visible, met à jour la liste des extensions
            if (IsVisible)
            {
                UpdateExtensionsList();
                UpdateProcessList();
            }
        }


        private void UpdateExtensionsList()
        {
            // Efface la liste actuelle
            blockedProcessesListBox.Items.Clear();

            // Récupère les extensions depuis le fichier Extensions.json
            List<string> extensions = settings.Get();

            // Ajoute chaque extension à la liste
            foreach (string extension in extensions)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = extension;
                blockedProcessesListBox.Items.Add(item);
            }
        }
        private void UpdateProcessList()
        {
            // Efface la liste actuelle
            encryptedExtensionsListBox.Items.Clear();

            // Récupère les extensions depuis le fichier Extensions.json
            List<string> processors = procset.GetP();

            // Ajoute chaque extension à la liste
            foreach (string proc in processors)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = proc;
                encryptedExtensionsListBox.Items.Add(item);
            }
        }

        private void encryptedExtensionsAddButton_Click(object sender, RoutedEventArgs e)
        {
            string newProcess = encryptedExtensionsTextBox.Text;
            procset.AddProc(newProcess);

            ListBoxItem item = new ListBoxItem();
            item.Content = newProcess;
            blockedProcessesListBox.Items.Add(item);
        }
        private void encryptedExtensionsDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (encryptedExtensionsListBox.SelectedIndex != -1)
            {
                string selectedProcess = encryptedExtensionsListBox.SelectedItem.ToString();
                procset.RemoveProc(encryptedExtensionsListBox.SelectedIndex);

                encryptedExtensionsListBox.Items.RemoveAt(encryptedExtensionsListBox.SelectedIndex);
            }
        }

        

        
        private void blockedProcessesAddButton_Click(object sender, RoutedEventArgs e)
        {
            string newExtension = blockedProcessesTextBox.Text;
            settings.AddExt(newExtension);

            ListBoxItem item = new ListBoxItem();
            item.Content = newExtension;
            blockedProcessesListBox.Items.Add(item);

        }
        private void blockedProcessesDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (blockedProcessesListBox.SelectedIndex != -1)
            {
                string selectedExtension = blockedProcessesListBox.SelectedItem.ToString();
                settings.RemoveExt(blockedProcessesListBox.SelectedIndex);

                blockedProcessesListBox.Items.RemoveAt(blockedProcessesListBox.SelectedIndex);
            }

        }
    }
}
