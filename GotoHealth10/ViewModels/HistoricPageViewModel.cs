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
                var dwm = new ObservableCollection<WeighingModel>()
                {
                    new WeighingModel()
                    {
                        Id = 1,
                        Date = new DateTime(2016, 4, 25),
                        Weight = "72.0"
                    },
                    new WeighingModel()
                    {
                        Id = 2,
                        Date = new DateTime(2016, 4, 26),
                        Weight = "71.2"
                    },
                    new WeighingModel()
                    {
                        Id = 3,
                        Date = new DateTime(2016, 4, 27),
                        Weight = "60.0"
                    }
                };

                Markings = dwm;
            }
        }

        WeighingModel _selectedItem;
        public WeighingModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);

                GotoMainPage();
            }
        }

        ObservableCollection<WeighingModel> _markings;
        public ObservableCollection<WeighingModel> Markings
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

        Task<WeighingModel> LoadItem(int Id)
        {
            WeighingRepository _dailyRepository = new WeighingRepository();

            var result = _dailyRepository.FindSelectedItemAsync(Id);
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

        Task<ObservableCollection<WeighingModel>> Load()
        {
            WeighingRepository _dailyRepository = new WeighingRepository();

            var result = _dailyRepository.LoadWeighingAsync(10);
            return result;
        }

        void GotoMainPage() =>
            NavigationService.Navigate(typeof(Views.MainPage), _selectedItem);

    }
}

