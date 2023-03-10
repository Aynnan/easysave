using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EasySave.Core
{// La classe implémente l'interface INotifyPropertyChanged qui permet de notifier la vue (dans le cas du pattern MVVM)
    // lorsqu'une propriété a été modifiée.
    class ObservableObject : INotifyPropertyChanged
    {        // L'événement PropertyChanged sera déclenché lorsqu'une propriété sera modifiée.

        public event PropertyChangedEventHandler? PropertyChanged;
        // Cette méthode permet de notifier la vue qu'une propriété a été modifiée.
        // Le paramètre [CallerMemberName] permet d'obtenir automatiquement le nom de la propriété modifiée.

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
