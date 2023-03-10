using EasySave.Core;
namespace EasySave.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }        
        public RelayCommand ModifyViewCommand { get; set; }
        public RelayCommand ExecuteViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }

        public HomeViewModel HomeVm { get; set; }
        public CreateViewModel CreateVM { get; set; }
        public ModifyViewModel ModifyVM { get; set; }
        public ExecuteViewModel ExecuteVM { get; set; }

        public SettingsViewModel SettingsVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            CreateVM = new CreateViewModel();
            ModifyVM = new ModifyViewModel();
            ExecuteVM = new ExecuteViewModel();
            SettingsVM = new SettingsViewModel();

            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView= HomeVm;
            });

            DiscoveryViewCommand = new RelayCommand(o =>
            {
                CurrentView = CreateVM;
            });

            ModifyViewCommand = new RelayCommand(o =>
            {
                CurrentView = ModifyVM;
            });

            ExecuteViewCommand = new RelayCommand(o =>
            {
                CurrentView = ExecuteVM;
            });
            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });
        }

    }
}
