using System.Collections.ObjectModel;
using System.Windows;
using TraderForPoe.Properties;
using TraderForPoe.ViewModel.Base;

namespace TraderForPoe.ViewModel
{
    public class UserSettingsViewModel : ViewModelBase
    {
        #region Constructors

        public UserSettingsViewModel()
        {
            CmdQuit = new RelayCommand(() => Application.Current.Shutdown());
            CmdRestart = new RelayCommand(() => RestartApp());
            CmdDeleteQuadStash = new RelayCommand(() => QuadStashList.Remove(SelectedQuadStashListItem));
            CmdAddToQuadStashList = new RelayCommand(() => AddToQuadStashList());
        }

        #endregion Constructors

        #region Properties

        public bool CheckForUpdatesOnStart
        {
            get => Settings.Default.CheckForUpdatesOnStart;
            set
            {
                if (Settings.Default.CheckForUpdatesOnStart != value)
                {
                    Settings.Default.CheckForUpdatesOnStart = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public bool CloseItemAfterCustomWhisper1
        {
            get => Settings.Default.CloseItemAfterCustomWhisper1;
            set
            {
                if (Settings.Default.CloseItemAfterCustomWhisper1 != value)
                {
                    Settings.Default.CloseItemAfterCustomWhisper1 = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public bool CloseItemAfterCustomWhisper2
        {
            get => Settings.Default.CloseItemAfterCustomWhisper2;
            set
            {
                if (Settings.Default.CloseItemAfterCustomWhisper2 != value)
                {
                    Settings.Default.CloseItemAfterCustomWhisper2 = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public bool CloseItemAfterCustomWhisper3
        {
            get => Settings.Default.CloseItemAfterCustomWhisper3;
            set
            {
                if (Settings.Default.CloseItemAfterCustomWhisper3 != value)
                {
                    Settings.Default.CloseItemAfterCustomWhisper3 = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public bool CloseItemAfterCustomWhisper4
        {
            get => Settings.Default.CloseItemAfterCustomWhisper4;
            set
            {
                if (Settings.Default.CloseItemAfterCustomWhisper4 != value)
                {
                    Settings.Default.CloseItemAfterCustomWhisper4 = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public bool CloseItemAfterImBusyWhisper
        {
            get => Settings.Default.CloseItemAfterImBusyWhisper;
            set
            {
                if (Settings.Default.CloseItemAfterImBusyWhisper != value)
                {
                    Settings.Default.CloseItemAfterImBusyWhisper = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public bool CloseItemAfterThankYouWhisper
        {
            get => Settings.Default.CloseItemAfterThankYouWhisper;
            set
            {
                if (Settings.Default.CloseItemAfterThankYouWhisper != value)
                {
                    Settings.Default.CloseItemAfterThankYouWhisper = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public bool CloseItemAfterTrade
        {
            get => Settings.Default.CloseItemAfterTrade;
            set
            {
                if (Settings.Default.CloseItemAfterTrade != value)
                {
                    Settings.Default.CloseItemAfterTrade = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand CmdAddToQuadStashList { get; }

        public RelayCommand CmdDeleteQuadStash { get; }

        public RelayCommand CmdQuit { get; }

        public RelayCommand CmdRestart { get; }

        public bool CollapsedItems
        {
            get => Settings.Default.CollapsedItems;
            set
            {
                if (Settings.Default.CollapsedItems != value)
                {
                    Settings.Default.CollapsedItems = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public float ControlOpacity
        {
            get => Settings.Default.ControlOpacity;
            set { Settings.Default.ControlOpacity = value; OnPropertyChanged(); }
        }

        public string CustomWhisper1
        {
            get => Settings.Default.CustomWhisper1;
            set
            {
                if (Settings.Default.CustomWhisper1 != value)
                {
                    Settings.Default.CustomWhisper1 = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public string CustomWhisper2
        {
            get => Settings.Default.CustomWhisper2;
            set
            {
                if (Settings.Default.CustomWhisper2 != value)
                {
                    Settings.Default.CustomWhisper2 = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public string CustomWhisper3
        {
            get => Settings.Default.CustomWhisper3;
            set
            {
                if (Settings.Default.CustomWhisper3 != value)
                {
                    Settings.Default.CustomWhisper3 = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public string CustomWhisper4
        {
            get => Settings.Default.CustomWhisper4;
            set
            {
                if (Settings.Default.CustomWhisper4 != value)
                {
                    Settings.Default.CustomWhisper4 = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public bool HideIfPoeNotForeGround
        {
            get => Settings.Default.HideIfPoeNotForeGround;
            set
            {
                if (Settings.Default.HideIfPoeNotForeGround != value)
                {
                    Settings.Default.HideIfPoeNotForeGround = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public string ImBusyWhisper
        {
            get => Settings.Default.ImBusyWhisper;
            set
            {
                if (Settings.Default.ImBusyWhisper != value)
                {
                    Settings.Default.ImBusyWhisper = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public string Path
        {
            get => Settings.Default.PathToClientTxt;
            set
            {
                if (Settings.Default.PathToClientTxt != value)
                {
                    Settings.Default.PathToClientTxt = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public string PlayerName
        {
            get => Settings.Default.PlayerName;
            set
            {
                if (Settings.Default.PlayerName != value)
                {
                    Settings.Default.PlayerName = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public bool PlayNotificationSound
        {
            get => Settings.Default.PlayNotificationSound;
            set
            {
                if (Settings.Default.PlayNotificationSound != value)
                {
                    Settings.Default.PlayNotificationSound = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> QuadStashList
        {
            get => Settings.Default.QuadStash;
            set { Settings.Default.QuadStash = value; OnPropertyChanged(); }
        }

        public string QuadStashText { get; set; }

        public string SelectedQuadStashListItem { get; set; }

        public string ThankYouWhisper
        {
            get => Settings.Default.ThankYouWhisper;
            set
            {
                if (Settings.Default.ThankYouWhisper != value)
                {
                    Settings.Default.ThankYouWhisper = value;
                    Settings.Default.Save();
                    OnPropertyChanged();
                }
            }
        }

        #endregion Properties

        #region Methods

        private void AddToQuadStashList()
        {
            if (!string.IsNullOrEmpty(QuadStashText) && !string.IsNullOrWhiteSpace(QuadStashText) && !QuadStashList.Contains(QuadStashText))
            {
                QuadStashList.Add(QuadStashText);
            }
        }

        private void RestartApp()
        {
            //TODO is this mvvm?
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        #endregion Methods
    }
}