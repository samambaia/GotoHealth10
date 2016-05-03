using GotoHealth10.Models;
using GotoHealth10.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace GotoHealth10.ViewModels
{
    public class HistoricPageViewModel : ViewModelBase
    {
        public HistoricPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                var dwm = new ObservableCollection<DailyWeighingModel>()
                {
                    new DailyWeighingModel()
                    {
                        Id = "1",
                        Date = new DateTime(2016, 4, 25),
                        Weight = "72.0"
                    },
                    new DailyWeighingModel()
                    {
                        Id = "2",
                        Date = new DateTime(2016, 4, 26),
                        Weight = "71.2"
                    },
                    new DailyWeighingModel()
                    {
                        Id = "3",
                        Date = new DateTime(2016, 4, 27),
                        Weight = "60.0"
                    }
                };

                Markings = dwm;
            }
        }

        DailyWeighingModel _selectedItem;
        public DailyWeighingModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);

                GotoMainPage();
            }
        }

        private static ObservableCollection<DailyWeighingModel> _markings;
        public ObservableCollection<DailyWeighingModel> Markings
        {
            get
            {
                return _markings;
            }
            set
            {
                Set(ref _markings, value);
            }
        }

        //public async Task Delete()
        //{
        //    await _dailyRepository.DeleteSelectedItem(_selectedItem);
        //}

        Task<DailyWeighingModel> LoadItem(string Date)
        {
            DailyWeighingRepository _dailyRepository = new DailyWeighingRepository();

            var result = _dailyRepository.FindSelectedItem(Date);
            return result;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Markings = await Load();
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        {
            if (suspending)
            {
                pageState[nameof(Markings)] = Markings;
            }

            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        Task<ObservableCollection<DailyWeighingModel>> Load()
        {
            DailyWeighingRepository _dailyRepository = new DailyWeighingRepository();

            var result = _dailyRepository.LoadAsync();
            return result;
        }

        //private void GotoAddWeightPage() =>
        //    NavigationService.Navigate(typeof(Views.AddWeightPage), null);

        private void GotoMainPage() =>
            NavigationService.Navigate(typeof(Views.MainPage), _selectedItem);

    }
}

