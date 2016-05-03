using GotoHealth10.Models;
using GotoHealth10.Repositories;
using System;
using System.Collections.Generic;
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
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Date = _DateDt;
                Weight = _Weight;
                UpDown = _UpDown;
                Difference = _Difference;
            }
        }

        DailyWeighingRepository dailyRepository = new DailyWeighingRepository();


        string _Weight = "69.90";
        public string Weight { get { return _Weight; } set { Set(ref _Weight, value); } }

        string _DateDt = DateTime.Today.Date.ToString("dd/MM/yyyy");
        public string Date { get { return _DateDt; } set { Set(ref _DateDt, value); } }

        string _Difference = "0.70";
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

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var par = ((DailyWeighingModel)parameter);
            if (par != null)
            {
                Date = par.Date.ToString();
                Weight = par.Weight;
                UpDown = par.UpDown;
                Difference = par.Difference;
            }
            else
            {
                var lastCheck = await dailyRepository.LastCheck();
                if (lastCheck != null)
                {
                    Date = lastCheck.Date.ToString();
                    Weight = lastCheck.Weight;
                    UpDown = lastCheck.UpDown;
                    Difference = lastCheck.Difference;
                }
                else if (parameter != null)
                {
                    Date = _DateDt;
                    Weight = _Weight;
                    UpDown = _UpDown;
                    Difference = _Difference;
                }
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

        //private void GotoDetailsPage() =>
        //    NavigationService.Navigate(typeof(Views.HistoricPage), null);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}