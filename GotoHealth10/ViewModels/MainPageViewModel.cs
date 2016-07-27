using GotoHealth10.Models;
using GotoHealth10.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace GotoHealth10.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            //if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            //{
            //    Date = _DateDt;
            //    Weight = _Weight;
            //    UpDown = _UpDown;
            //    Difference = _Difference;
            //}
        }

        WeighingRepository dailyRepository = new WeighingRepository();
        UserRepository userRepository = new UserRepository();

        #region Main Properties

        double _BestWeight;
        public double BestWeight
        {
            get { return _BestWeight; }
            set { Set(ref _BestWeight, value); }
        }

        private double _WorstWeight;

        public double WorstWeight
        {
            get { return _WorstWeight; }
            set { Set(ref _WorstWeight, value); }
        }

        #endregion

        #region Weight Properties

        double _Weight = 69.9;
        public double Weight { get { return _Weight; } set { Set(ref _Weight, value); } }

        string _DateDt = DateTime.Today.Date.ToString("dd/MM/yyyy");
        public string Date { get { return _DateDt; } set { Set(ref _DateDt, value); } }

        string _Difference = "0.7";
        public string Difference
        {
            get { return _Difference; }
            set { Set(ref _Difference, value); }
        }

        string _UpDown = "0";
        public string UpDown
        {
            get { return _UpDown; }
            set { Set(ref _UpDown, value); }
        }

        ObservableCollection<WeighingModel> _Chart;
        public ObservableCollection<WeighingModel> Chart
        {
            get
            {
                return _Chart;
            }
            set {Set( ref _Chart, value); }
        }

        double _Imc;
        public double Imc
        {
            get { return _Imc; }
            set { Set(ref _Imc, value); }
        }

        #endregion

        #region User Properties

        private string _NickName;
        public string NickName
        {
            get { return _NickName; }
            set { Set(ref _NickName, value); }
        }

        private string _Age;
        public string Age
        {
            get { return _Age; }
            set { Set(ref _Age, value); }
        }

        double _TargetWeight;
        public double TargetWeight
        {
            get { return _TargetWeight; }
            set { Set(ref _TargetWeight, value); }
        }

        double _Height;
        public double Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        #endregion

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            // Ckeck if a Weight exists
            var lastCheck = await dailyRepository.LastCheckAsync();

            // Check the best Weight
            BestWeight = await dailyRepository.BestWeightAsync();

            // Check the worst Weight
            WorstWeight = await dailyRepository.WorstWeightAsync();

            // Check if a user exists
            var existUser = userRepository.FindUser();
            if (existUser == null)
            {
                GotoUserPage();
            }
            else
            {
                if (lastCheck == null)
                {
                    GotoAddWeightPage();
                }
            }

            if (existUser != null)
            {
                NickName = existUser.NickName;
                Age = existUser.Age.ToString();
                TargetWeight = existUser.TargetWeight;
                Height = existUser.Height;
            }

            if (lastCheck != null)
            {
                Date = lastCheck.Date.ToString();
                Weight = lastCheck.Weight;
                UpDown = lastCheck.UpDown;
                Difference = lastCheck.Difference;
                Imc = lastCheck.IMC;

                Chart = await dailyRepository.LoadWeighingAsync(10);
            }

            var par = ((WeighingModel)parameter);
            if (par != null)
            {
                Date = par.Date.ToString();
                Weight = par.Weight;
                UpDown = par.UpDown;
                Difference = par.Difference;
                Imc = par.IMC;
            }

            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        {
            if (suspending)
            {
                pageState[nameof(Weight)] = Weight;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        private void GotoMainPage() =>
            NavigationService.Navigate(typeof(Views.MainPage), null);

        private void GotoAddWeightPage() =>
            NavigationService.Navigate(typeof(Views.AddWeightPage), null);

        private void GotoUserPage() =>
            NavigationService.Navigate(typeof(Views.UserPage), null);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        //public void GotoPrivacy() =>
        //    NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

    }
}