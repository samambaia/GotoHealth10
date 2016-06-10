using GotoHealth10.Models;
using GotoHealth10.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace GotoHealth10.ViewModels
{
    public class AddWeightPageViewModel : ViewModelBase
    {

        //string[] _DateDt = DateTime.Now.GetDateTimeFormats('d', CultureInfo.CurrentCulture);

        WeighingRepository dailyRepository = new WeighingRepository();
        UserRepository userRepository = new UserRepository();
        public AddWeightPageViewModel()
        {
            Weight = "35";
        }

        string _Weight;
        public string Weight
        {
            get{ return _Weight; }
            set{ Set(ref _Weight, value); }
        }

        //string _Date;
        //public string Date
        //{
        //    get{ return _Date; }
        //    set{ Set(ref _Date, value); }
        //}

        string _Difference;
        public string Difference
        {
            get { return _Difference; }
            set { Set(ref _Difference, value); }
        }

        string _UpDown;
        public string UpDown
        {
            get { return _UpDown; }
            set { Set(ref _UpDown, value); }
        }

        #region IMC Calc

        double _Height;
        public double Height
        {
            get { return _Height; }
            set { Set(ref _Height, value); }
        }

        double _IMC;
        public double IMC
        {
            get { return _IMC; }
            set { Set(ref _IMC, value); }
        }

        #endregion

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var paramUser = (UserModel)parameter;

            if (paramUser != null)
            {
                Height = paramUser.Height;
                Weight = paramUser.InitialWeigth.ToString();
            }
            else
            {
                var existUser = userRepository.FindUser();
                if (existUser != null)
                {
                    Height = existUser.Height;
                }
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
            var _imc = await CalcIMCAsync(Height, double.Parse(Weight));

            IMC = double.Parse(_imc);

            int id = 0;

            var lastCheck = await dailyRepository.LastCheckAsync();

            if (lastCheck != null)
            {
                id = Convert.ToInt32(lastCheck.Id);

                double last = lastCheck.Weight;
                double current = double.Parse(Weight);
                double dif = current - last;

                dif = Math.Round(dif, 2);

                if (dif.ToString().Length >= 3)
                {
                    if (dif >= 0)
                    {
                        Difference = dif.ToString().Substring(0, 3);
                    }
                    else
                    {
                        Difference = dif.ToString().Substring(0, 4);
                    }
                }
                else
                {
                    Difference = dif.ToString();
                }

                UpDown = dif < 0 ? "0" : "1";
            }
            else
            {
                UpDown = "0";
                Difference = "0";
            }

            var checkModel = new WeighingModel
            {
                Date = DateTime.Now,
                Weight = double.Parse(Weight),
                UpDown = UpDown,
                Difference = Difference,
                IMC = IMC
            };
            
            await dailyRepository.SaveWeighingAsync(checkModel);

            GotoDetailsPage();
        }
        
        async Task<string> CalcIMCAsync(double height, double weight)
        {
            return await Task.Run(() => CalcIMC(height, weight));
        }

        string CalcIMC(double height, double weight)
        {
            ////height = height.Replace(',', '.');
            //var _height = double.Parse(height, CultureInfo.CurrentUICulture);
            ////weight = weight.Replace(',', '.');
            //var _weight = double.Parse(weight, CultureInfo.CurrentUICulture);
            //var _heightPow = Math.Pow(_height, 2);
            //var imc = _weight / _heightPow;

            var _heightPow = Math.Pow(height, 2);
            var imc = weight / _heightPow;

            return imc.ToString("N2");
        }

        void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.MainPage), null);
    }
}

