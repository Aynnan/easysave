
using EasySave.Languages;
using EasySave.MVVM.Model;
using EasySave.MVVM.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Navigation;
using System;
using System.Windows.Data;

namespace EasySave.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour ModifyView.xaml
    /// </summary>
    public partial class ModifyView : UserControl
    {

        ModifyViewModel modif;
        public ModifyView()
        {
            InitializeComponent();
            modif = new();
            FillList();
            PleaseModify.Text = cLanguage.GetString("Please choose a backup to modify :");
        }
        public void FillList()
        {
            List<Work> works = modif._works;
            if (works.Count == 0)
            {
                System.Windows.Controls.TextBox textBox = new();
                textBox.FontSize = 30;
                textBox.Text = cLanguage.GetString("NoWorkCreated");
                textBox.Background = Brushes.Transparent;
                textBox.Foreground = Brushes.White;
                textBox.BorderThickness = new Thickness(0);
                listWorks.Children.Add(textBox);
                return;
            }
            int compteur = 0;
            ListBox listBox = new ListBox();

            foreach (Work work in works)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = $"{work.sName} :\nSource :{work.sSource}\nTarget : {work.sTarget}\nType : {work.sType}";
                newItem.Tag = $"{compteur}";
                newItem.PreviewMouseDoubleClick += ListBoxItem_MouseDoubleClick;
                listBox.Items.Add(newItem);
                compteur++;
            }

            listWorks.Children.Add(listBox);
        }
        public void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem item = sender as ListBoxItem;
            if (item != null)
            {
                string tag = item.Tag.ToString() ?? "";
                DisplayWork(int.Parse(tag));
            }
        }
        private void DisplayWork(int index)
        {
            Work work = modif._works[index];
            while (listWorks.Children.Count > 0) //Vider le stackPanel
            {
                listWorks.Children.RemoveAt(listWorks.Children.Count - 1);
            }
            //Création du Text input name
            //StackPanel
            StackPanel nameStack = new();
            //Label
            Label nameLabel = new();
            nameLabel.Foreground = Brushes.White;
            nameLabel.Content = cLanguage.GetString("TypeNewName");
            //Input
            System.Windows.Controls.TextBox name = new();
            name.Name = "ModifyNameTest";
            name.Text = work.sName;
            //Ajout au panel
            nameStack.Children.Add(nameLabel);
            nameStack.Children.Add(name);

            //Création du Text input source 
            //StackPanel
            StackPanel sourceStack = new();
            //Label 
            Label sourceLabel = new();
            sourceLabel.Foreground = Brushes.White;
            sourceLabel.Content = cLanguage.GetString("TypeNewSource");
            //Input
            System.Windows.Controls.TextBox source = new();
            source.Name = "ModifySourceTest";
            source.Text = work.sSource;
            //Ajout au panel 
            sourceStack.Children.Add(sourceLabel);
            sourceStack.Children.Add(source);


            //Création du text input target
            //StackPanel
            StackPanel targetStack = new();
            //Label
            Label targetLabel = new();
            targetLabel.Foreground = Brushes.White;
            targetLabel.Content = cLanguage.GetString("TypeNewTarget");
            //Input
            System.Windows.Controls.TextBox target = new();
            target.Name = "ModifyTargetTest";
            target.Text = work.sTarget;
            //Ajout au panel
            targetStack.Children.Add(targetLabel);
            targetStack.Children.Add(target);


            // Création des RadioButton
            //Stack
            StackPanel typeStack = new();
            
            //Label 
            Label typeLabel = new();
            typeLabel.Foreground = Brushes.White;
            typeLabel.Content = cLanguage.GetString("TypeNewType");
            // Création de RadioButton "Complete"
            RadioButton completeRadioButton = new RadioButton();
            completeRadioButton.Margin = new Thickness(0, 10, 0, 0);
            completeRadioButton.Content = "Complete";
            completeRadioButton.Name = "complete";
            completeRadioButton.Tag = "complete";
            // Création de RadioButton "Differential"
            RadioButton differentialRadioButton = new RadioButton();
            differentialRadioButton.Content = "Differential";
            differentialRadioButton.Name = "differential";
            differentialRadioButton.Tag = "differential";

            typeStack.Children.Add(typeLabel);
            typeStack.Children.Add(completeRadioButton);
            typeStack.Children.Add(differentialRadioButton);

            listWorks.Children.Add(nameStack);
            listWorks.Children.Add(sourceStack);
            listWorks.Children.Add(targetStack);
            listWorks.Children.Add(typeStack);

            StackPanel stackButton = new();
            //Création du bouton de validation
            System.Windows.Controls.Button create = new();
            create.Content = "Modify";
            completeRadioButton.Foreground = Brushes.White;
            differentialRadioButton.Foreground = Brushes.White;
            create.Click += new RoutedEventHandler((sender, e) => {
                string type;

                if ((bool)completeRadioButton.IsChecked)
                    type = "complete";
                else if ((bool)differentialRadioButton.IsChecked)
                    type = "differential";
                else
                {
                    MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
             
                modif.Modify(index, name.Text, source.Text, target.Text, type);
                MessageBox.Show("Travail modifié", "Info", MessageBoxButton.OK);
               
            });
            //Création du bouton de reset
            ModifyView modifyView = new ModifyView();

           
            stackButton.Children.Add(create);


            listWorks.Children.Add(stackButton);

        }
    }
}
