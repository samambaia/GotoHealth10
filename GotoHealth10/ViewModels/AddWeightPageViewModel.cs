using GotoHealth10.Models;
using GotoHealth10.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace GotoHealth10.ViewModels
{
    public class AddWeightPageViewModel : ViewModelBase
    {
        public AddWeightPageViewModel()
        {
            Date = _DateDt[0];
            Weight = "70.00";
        }

        string[] _DateDt = DateTime.Now.GetDateTimeFormats('d', CultureInfo.CurrentCulture);

        DailyWeighingRepository dailyRepository = new DailyWeighingRepository();

        string _Weight;
        public string Weight
        {
            get
            {
                return _Weight;
            }
            set
            {
                Set(ref _Weight, value);
            }
        }

        string _Date;
        public string Date
        {
            get
            {
                return _Date;
            }
            set
            {
                Set(ref _Date, value);
            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var itemEdit = (DailyWeighingModel)parameter;
            if (itemEdit != null)
            {
                Date = itemEdit.Date;
                Weight = itemEdit.Weight;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        {
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async Task AddWeight()
        {
            await SaveWeight();
        }
        async Task SaveWeight()
        {
            var checkModel = new DailyWeighingModel()
            {
                Id = 1,
                Date = Date,
                Weight = Weight
            };

            await dailyRepository.SaveAsync(checkModel);

            GotoDetailsPage();
        }

        private void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.HistoricPage), null);

    }
}

