using GotoHealth10.Repositories;
using System;
using System.Collections.Generic;
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
                Date = _DateDt[3];
                Weight = _Weight;
            }
        }

        DailyWeighingRepository dailyRepository = new DailyWeighingRepository();

        string[] _DateDt = DateTime.Now.GetDateTimeFormats('d');

        string _Weight = "71.80";
        public string Weight { get { return _Weight; } set { Set(ref _Weight, value); } }

        public string Date { get { return _DateDt[3]; } set { Set(ref _DateDt[3], value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var lastCheck = await dailyRepository.LastCheck();
            if (lastCheck != null)
            {
                Date = lastCheck.Date;
                Weight = lastCheck.Weight;
            }
            else
            {
                Date = _DateDt[3];
                Weight = _Weight;
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