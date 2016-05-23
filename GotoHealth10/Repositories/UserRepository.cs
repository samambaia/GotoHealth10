using GotoHealth10.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GotoHealth10.Repositories
{
    public class UserRepository
    {
        #region Local Storage
        const string FILENAME = "GTH_User.txt";

        public async Task SaveAsync(UserModel userModel)
        {
            var jsonString = JsonConvert.SerializeObject(userModel);
            var file = await LocalFile();
            await FileIO.WriteTextAsync(file, jsonString);
        }

        //public async Task<UserModel> FindUser()
        //{
        //    var user = await findAsync();

        //    return user;
        //}

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

        async Task<UserModel> findAsync()
        {
            try
            {
                var file = await LocalFile();
                var jsonString = await FileIO.ReadTextAsync(file);
                if (jsonString != String.Empty)
                {
                    var user = JsonConvert.DeserializeObject<UserModel>(jsonString);
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new UserModel();
        }

        #endregion

        #region SQLite Classes
        public UserModel FindUser()
        {
            using (var db = new Context())
            {
                var user = db.User.FirstOrDefault();

                return user;
            }
        }

        public async Task<int> SaveUserAsync(UserModel userModel)
        {
            using (var db = new Context())
            {
                db.User.Add(userModel);
                var ret = db.SaveChangesAsync();
                return await ret;
            }
        }

        public async Task<int> UpdateUserAsync(UserModel userModel)
        {
            using (var db = new Context())
            {
                db.Entry(userModel).State = Microsoft.Data.Entity.EntityState.Modified;
                db.User.Update(userModel);
                var ret = db.SaveChangesAsync();
                return await ret;
            }
        }

        #endregion
    }
}
