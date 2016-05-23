using GotoHealth10.Models;
using GotoHealth10.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GotoHealth10.ViewModels
{
    public class UserPageViewModel : ViewModelBase
    {
        bool update = false;

        UserModel userModel;

        UserRepository userRepository = new UserRepository();

        public UserPageViewModel()
        {
            Genders = Enum.GetValues(typeof(GenderType));
        }

        GenderType _SelectedItem;
        public GenderType SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                Set(ref _SelectedItem, value);

                Gender = _SelectedItem.ToString();
            }
        }

        int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        string _Email;
        public string Email
        {
            get { return _Email; }
            set { Set(ref _Email, value); }
        }

        string _NickName;
        public string NickName
        {
            get { return _NickName; }
            set { Set(ref _NickName, value); }
        }

        string _Logged;
        public string Logged
        {
            get { return _Logged; }
            set { Set(ref _Logged, value); }
        }

        string _Age;
        public string Age
        {
            get { return _Age; }
            set { Set(ref _Age, value); }
        }

        string _Gender;
        public string Gender
        {
            get { return _Gender; }
            set { Set(ref _Gender, value); }
        }

        string _InitialWeight;
        public string InitialWeight
        {
            get { return _InitialWeight; }
            set { Set(ref _InitialWeight, value); }
        }

        string _Height;
        public string Height
        {
            get { return _Height; }
            set { Set(ref _Height, value); }
        }

        string _TargetWeight;
        public string TargetWeight
        {
            get { return _TargetWeight; }
            set { Set(ref _TargetWeight, value); }
        }

        Array _Genders;
        public Array Genders
        {
            get { return _Genders; }
            set { Set(ref _Genders, value); }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var existUser = userRepository.FindUser();

            if (existUser != null)
            {
                // Flag para sinalizar o update na base
                update = true;

                Id = existUser.Id;
                Email = existUser.Email;
                NickName = existUser.NickName;
                Age = existUser.Age;
                SelectedItem = (GenderType)(existUser.Gender == "Male" ? 0 : 1);
                InitialWeight = existUser.InitialWeigth;
                Height = existUser.Height;
                TargetWeight = existUser.TargetWeight;
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

        //public async Task SaveUser()
        //{
        //    await Save();
        //}

        public async Task SaveUserAsync()
        {
            if (!update)
                await Save();
            else
                await Update();
        }

        async Task Save()
        {
            userModel = new UserModel()
            {
                Email = Email,
                NickName = NickName,
                Logged = "1",
                Age = Age,
                Gender = Gender,
                InitialWeigth = InitialWeight,
                Height = Height,
                TargetWeight = TargetWeight
            };

            await userRepository.SaveUserAsync(userModel);

            GotoAddWeightPage();
        }

        async Task Update()
        {
            userModel = new UserModel()
            {
                Id = Id,
                Email = Email,
                NickName = NickName,
                Logged = "1",
                Age = Age,
                Gender = Gender,
                InitialWeigth = InitialWeight,
                Height = Height,
                TargetWeight = TargetWeight
            };

            await userRepository.UpdateUserAsync(userModel);

            GotoMainPage();
        }


        void GotoAddWeightPage() =>
            NavigationService.Navigate(typeof(Views.AddWeightPage), userModel);

        void GotoMainPage() =>
            NavigationService.Navigate(typeof(Views.MainPage), null);

        // Create enum GenderType
        public enum GenderType
        {
            Male,
            Female
        } 
    }

}
