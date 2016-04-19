using GotoHealth10.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace GotoHealth10.Repositories
{
    public class DailyWeighingRepository
    {
        private const string FILENAME = "GotoHealth.txt";

        public async Task SaveAsync(DailyWeighingModel dailyWeighing)
        {
            var checks = (await findAllAsync()).ToList();
            checks.Add(dailyWeighing);
            var jsonString = JsonConvert.SerializeObject(checks);
            var file = await LocalFile();
            await FileIO.WriteTextAsync(file, jsonString);
        }
        
        public async Task<ObservableCollection<DailyWeighingModel>> LoadAsync()
        {
            try
            {
                var file = await LocalFile();
                var jsonString = await FileIO.ReadTextAsync(file);
                if (jsonString != string.Empty)
                {
                    var items = await Task.Factory.StartNew( () =>  JsonConvert.DeserializeObject<ObservableCollection<DailyWeighingModel>>(jsonString));
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new ObservableCollection<DailyWeighingModel>();
        }

        public async Task<IEnumerable<DailyWeighingModel>> FindAllByUserNameAsync(int Id)
        {
            var checks = await findAllAsync();
            var checksByUserName = checks.Where(s => s.Id == Id);

            return checksByUserName.ToArray();
        }

        public async Task<DailyWeighingModel> FindSelectedItem(string Date)
        {
            var checks = await findAllAsync();
            var selectedItem = checks.Where(a => a.Date == Date);

            return (DailyWeighingModel)selectedItem;
        }

        public async Task DeleteSelectedItem(DailyWeighingModel item)
        {
            var items = (await findAllAsync()).ToList();

            DailyWeighingModel _item = items.Single(c => c.Date == item.Date);

            items.Remove(_item);

            var jsonString = JsonConvert.SerializeObject(items);
            var file = await LocalFile();
            await FileIO.WriteTextAsync(file, jsonString);
        }

        public async Task<DailyWeighingModel> LastCheck()
        {
            var checks = await findAllAsync();
            var lastCheck = checks.OrderByDescending(s => s.Date).FirstOrDefault();

            return lastCheck;
        }

        private async Task<StorageFile> LocalFile()
        {
            StorageFile file;

            try
            {
                file = await ApplicationData.Current.LocalFolder.GetFileAsync(FILENAME);
            }
            catch
            {
                file = ApplicationData.Current.LocalFolder.CreateFileAsync(FILENAME, CreationCollisionOption.OpenIfExists).GetResults();
            }

            return file;
        }

        private async Task<IEnumerable<DailyWeighingModel>> findAllAsync()
        {
            try
            {
                var file = await LocalFile();
                var jsonString = await FileIO.ReadTextAsync(file);
                if (jsonString != String.Empty)
                {
                    var checks = JsonConvert.DeserializeObject<List<DailyWeighingModel>>(jsonString);
                    return checks.OrderByDescending(a => a.Date);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Enumerable.Empty<DailyWeighingModel>();
        }
    }
}
