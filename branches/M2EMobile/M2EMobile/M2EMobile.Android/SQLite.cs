using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using M2EMobile.Droid;
using Xamarin.Forms;

[assembly: Dependency (typeof (SQLite_Android))]

namespace M2EMobile.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {
        }

        #region ISQLite implementation

        public global::SQLite.Net.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "M2ESQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.TodoSQLite); // RESOURCE NAME ###

                // create a write stream
                FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                // write to the stream
                ReadWriteStream(s, writeStream);
            }

            var plat = new global::SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var conn = new global::SQLite.Net.SQLiteConnection(plat, path);

            // Return the database connection 
            return conn;
            //return null;
        }

        #endregion

        /// <summary>
        /// helper method to get the database out of /raw/ and into the user filesystem
        /// </summary>
        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}