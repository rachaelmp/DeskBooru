using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace DeskBooruApp
{
    class Database
    {
        public SQLiteConnection myConnection;

        public Database()
        {
            myConnection = new SQLiteConnection("Data Source = DeskbooruDB.db;Version=3");

        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }
        
        public void CloseConnection()
        {
            if(myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }


        /// Attempt at Implementing the SQLite Commands into Functions for actual use:
        /// #3:

        public void favorite_Image(string userInput)
        {
            string query = "INSERT INTO favorites (image_id, created_at) VALUES (@input, @date)";
             SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            myCommand.Parameters.AddWithValue("@input", userInput);
            myCommand.Parameters.AddWithValue("@date", System.DateTime.Now.ToShortDateString());
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }

        public void favorite_By_Date(string userInput)
        {
            string query = "SELECT * FROM favorites ORDER BY created_at ASC";
             SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }

        /// #4:
        /// 

        public void add_Tag_Image(string userInputImg_ID, string userInputTag_ID)
        {
            string query = "INSERT INTO image_tags (image_id, tag_id) VALUES (@image, @tag)";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            myCommand.Parameters.AddWithValue("@image", userInputImg_ID);
            myCommand.Parameters.AddWithValue("@tag", userInputTag_ID);
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }

        /// #5:
        /// 

        public void create_Gallery(string userInputName, string userInputDesc)
        {
            string query = "INSERT INTO gallery (created_at, title, description) VALUES (@date, @name, @desc";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            myCommand.Parameters.AddWithValue("@date", System.DateTime.Now.ToShortDateString());
            myCommand.Parameters.AddWithValue("@image", userInputName); 
            myCommand.Parameters.AddWithValue("@tag", userInputDesc);
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }

        public void add_Img_To_Gallery(string userInputImg_ID, string userInputG_Name)
        {
            string query = "INSERT INTO image_gallery (image_id, gallery_id, date_added) VALUES(@img_id, (SELECT id FROM gallery WHERE title = @g_name), @date)";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            myCommand.Parameters.AddWithValue("@date", System.DateTime.Now.ToShortDateString());
            myCommand.Parameters.AddWithValue("@img_id", userInputImg_ID);
            myCommand.Parameters.AddWithValue("@g_name", userInputG_Name);
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }

        public void get_All_Img_In_Gal(string  userInputG_ID)
        {
            string query = "SELECT * FROM image_gallery WHERE gallery_id = @g_id";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            myCommand.Parameters.AddWithValue("@g_id", userInputG_ID);
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }

        public void sort_Gal_By_Date()
        {
            string query = "SELECT * FROM gallery ORDER BY created_at ASC";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }

        public void gallery_Info(string userInputG_ID)
        {
            string query = "SELECT * FROM gallery WHERE id = @g_id";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            myCommand.Parameters.AddWithValue("@g_id", userInputG_ID);
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }
    }
}
