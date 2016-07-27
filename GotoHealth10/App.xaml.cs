using GotoHealth10.Models;
using GotoHealth10.Repositories;
using GotoHealth10.Services.SettingsServices;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Template10.Controls;
using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.UI.Xaml;

namespace GotoHealth10
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            using (var db = new Context())
            {
                //db.Database.EnsureDeleted();
                //var addCol = db.User.FromSql("ALTER TABLE UserModel ADD COLUMN teste Text;");
                db.Database.EnsureCreated();
                //db.Database.Migrate();
            }

            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion

            Microsoft.HockeyApp.HockeyClient.Current.Configure("4bc0f0ec461f4cd8b2cbda32a42002aa");

            // Define current language to the App
            //ApplicationLanguages.PrimaryLanguageOverride = "pt-BR";
            //ApplicationLanguages.PrimaryLanguageOverride = "en-US";
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            if (Window.Current.Content as ModalDialog == null)
            {
                // create a new frame 
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                // create modal root
                Window.Current.Content = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    Content = new Views.Shell(nav),
                    ModalContent = new Views.Busy()
                };
            }
            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // long-running startup tasks go here

            WeighingRepository dailyRepository = new WeighingRepository();
            UserRepository userRepository = new UserRepository();

            var existUser = userRepository.FindUser();

            if (existUser == null)
            {
                NavigationService.Navigate(typeof(Views.UserPage));
            }
            else
            {
                // Ckeck if a Weight exists
                var lastCheck = await dailyRepository.LastCheckAsync();
                if (lastCheck == null)
                {
                    NavigationService.Navigate(typeof(Views.AddWeightPage));
                }
                else
                {
                    NavigationService.Navigate(typeof(Views.MainPage));
                }
            }

            await Task.CompletedTask;
        }
    }
}

