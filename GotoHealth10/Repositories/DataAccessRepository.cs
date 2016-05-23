using GotoHealth10.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GotoHealth10.Repositories
{
    public static class DataAccessRepository
    {
        static string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        static SQLite.Net.SQLiteConnection _conn;
        public static SQLite.Net.SQLiteConnection conn
        {
            get
            {
                _conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
                return _conn;
            }
        }

    }
}

