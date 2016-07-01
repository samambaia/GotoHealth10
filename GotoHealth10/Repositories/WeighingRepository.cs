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

        public async Task<double> BestWeightAsync()
        {
            using (var db = new Context())
            {
                var bestCheck = await Task.Factory.StartNew(() => db.Weighing.Min(s => s.Weight));
                return bestCheck;
            }
        }

        /// <summary>
        /// Usado para dar uma carga inicial no APP com os último 10 dias de marcação.
        /// </summary>
        public void ListAddWeighing()
        {
            var listWeighing = new List<WeighingModel>();

            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 16), Weight = 72.7, Difference = "0", IMC = 0, UpDown = "0" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 17), Weight = 72.6, Difference = "-0.1", IMC = 27.36, UpDown = "0" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 18), Weight = 72.8, Difference = "0.2", IMC = 27.33, UpDown = "1" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 19), Weight = 72.4, Difference = "-0.2", IMC = 27.25, UpDown = "0" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 20), Weight = 72.8, Difference = "0.4", IMC = 27.40, UpDown = "1" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 21), Weight = 72.9, Difference = "0.1", IMC = 27.44, UpDown = "1" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 22), Weight = 72.4, Difference = "-0.5", IMC = 27.25, UpDown = "0" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 25), Weight = 72.4, Difference = "0", IMC = 27.25, UpDown = "0" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 27), Weight = 73.5, Difference = "1.1", IMC = 27.66, UpDown = "1" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 28), Weight = 72.8, Difference = "-0.7", IMC = 27.40, UpDown = "0" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 6, 30), Weight = 72.8, Difference = "-0.7", IMC = 27.40, UpDown = "0" });
            listWeighing.Add(new WeighingModel() { Date = new System.DateTime(2016, 7, 1), Weight = 73.5, Difference = "0.7", IMC = 27.66, UpDown = "1" });

            foreach (var item in listWeighing)
            {
                try
                {
                    using (var db = new Context())
                    {
                        db.Weighing.Add(item);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        #endregion
    }
}
