using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seting_sqlite
{
    class DataAccess
    {
        public async static void InitializeDatabase()
        {
            //await ApplicationData.Current.LocalFolder.CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
            //string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    "Text_Entry NVARCHAR(2048) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddData(string inputText)
        {
            using(SqliteConnection db =
                 new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @Entry)"; //ใส่ให้ตครบทุกตัวที่ส่งเข้าไป
                insertCommand.Parameters.AddWithValue("@Entry",inputText); //parameter ที่ต้องการส่งไป DB ต้องมี @ เพื่อป้องกันการใส่ SQL ถ้าเป็น SQL จะไม่ทำงาน

                insertCommand.ExecuteReader(); //Insert แล้วจบการทำงาน ไม่มี Database Return
                db.Close();
            }
        }

        public static List<String> GetData()
        {
            List<String> entries = new List<string>();

            using (SqliteConnection db =
                new SqliteConnection("Filename = sqliteSample.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Text_Entry from MyTable", db);
                SqliteDataReader query = selectCommand.ExecuteReader(); //รอ Database Return เลยต้องมี SqliteDataReader

                while (query.Read()) //กระโดดไปข้อมูลถัดไป ถ้ามี return true, ก้าวต่อไม่ได้หรือไม่มีข้อมูล return false
                {
                    entries.Add(query.GetString(0));
                }

                db.Close();
            }

            return entries;
        }

    }
}
