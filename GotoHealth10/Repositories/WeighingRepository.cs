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
    public class WeighingRepository
    {
        #region Local Storage
        /*
        const string FILENAME = "GotoHealth.txt";

        public async Task SaveAsync(WeighingModel dailyWeighing)
        {
            var checks = (await findAllAsync()).ToList();
            checks.Add(dailyWeighing);
            var jsonString = JsonConvert.SerializeObject(checks);
            var file = await LocalFile();
            await FileIO.WriteTextAsync(file, jsonString);
        }
        
        public async Task<ObservableCollection<WeighingModel>> LoadAsync()
        {
            try
            {
                var file = await LocalFile();
                var jsonString = await FileIO.ReadTextAsync(file);
                if (jsonString != string.Empty)
                {
                    var items = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ObservableCollection<WeighingModel>>(jsonString));

                    var order = items.OrderByDescending(a => a.Date).Take(5);

                    ObservableCollection<WeighingModel> itemsDesc = new ObservableCollection<WeighingModel>(order);
                    return itemsDesc;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new ObservableCollection<WeighingModel>();
        }

        //public async Task<IEnumerable<DailyWeighingModel>> FindAllByUserNameAsync(string Id)
        //{
        //    var checks = await findAllAsync();
        //    var checksByUserName = checks.Where(s => s.Id == Id);

        //    return checksByUserName.ToArray();
        //}

        public async Task<WeighingModel> FindSelectedItem(string Date)
        {
            var checks = await findAllAsync();
            var selectedItem = checks.Where(a => a.Date == DateTime.Parse(Date));

            return (WeighingModel)selectedItem;
        }

        public async Task DeleteSelectedItem(WeighingModel item)
        {
            var items = (await findAllAsync()).ToList();

            WeighingModel _item = items.Single(c => c.Date == item.Date);

            items.Remove(_item);

            var jsonString = JsonConvert.SerializeObject(items);
            var file = await LocalFile();
            await FileIO.WriteTextAsync(file, jsonString);
        }

        public async Task<WeighingModel> LastCheck()
        {
            var checks = await findAllAsync();
            var lastCheck = checks.OrderByDescending(s => s.Date).FirstOrDefault();

            return lastCheck;
        }

        async Task<StorageFile> LocalFile()
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

        async Task<IEnumerable<WeighingModel>> findAllAsync()
        {
            try
            {
                var file = await LocalFile();
                var jsonString = await FileIO.ReadTextAsync(file);
                if (jsonString != String.Empty)
                {
                    var checks = JsonConvert.DeserializeObject<List<WeighingModel>>(jsonString);
                    return checks.OrderByDescending(a => a.Date);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Enumerable.Empty<WeighingModel>();
        }
        */
        #endregion

        #region SQLite
        public async Task<int> SaveWeighingAsync(WeighingModel weighingModel)
        {
            try
            {
                using (var db = new Context())
                {
                    db.Weighing.Add(weighingModel);
                    var ret = db.SaveChangesAsync();
                    return await ret;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ObservableCollection<WeighingModel>> LoadWeighingAsync(int takeItems)
        {
            try
            {
                using (var db = new Context())
                {
                    var items = await Task.Factory.StartNew(() => db.Weighing.OrderByDescending(a => a.Date).Take(takeItems));
                    ObservableCollection<WeighingModel> itemsDesc = new ObservableCollection<WeighingModel>(items);
                    return itemsDesc;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ObservableCollection<WeighingModel>> LoadWeighingDescAsync()
        {
            try
            {
                using (var db = new Context())
                {
                    var items = await Task.Factory.StartNew(() => db.Weighing.OrderByDescending(a => a.Date));
                    ObservableCollection<WeighingModel> itemsDesc = new ObservableCollection<WeighingModel>(items);
                    return itemsDesc;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<WeighingModel> FindSelectedItemAsync(int Id)
        {
            using (var db = new Context())
            {
                var selectedItem = await Task.Factory.StartNew(() => db.Weighing.Where(a => a.Id == Id));
                return (WeighingModel)selectedItem;
            }
        }

        public async Task<WeighingModel> LastCheckAsync()
        {
            using (var db = new Context())
            {
                var lastCheck = await Task.Factory.StartNew(() => db.Weighing.OrderByDescending(s => s.Date).FirstOrDefault());
                return lastCheck;
            }
        }

        #endregion
    }
}
