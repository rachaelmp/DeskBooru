using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

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

        public void dispose()
        {
            myConnection.Dispose();
        }

        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }

        //inserts data and returns the ID of the newly inserted row!
        public int insertImage(string createdAt, int width, int height, string aspectRatio, string format, string path)
        {
            int ID = 0;
            //using an upsert so if function tries to write row that already has a certain image_path
            //do nothing!
            string query = "INSERT INTO images(created_at, image_width, image_height, aspect_ratio, image_format, image_path) " +
                "VALUES(@createdAt, @width, @height, @Aratio, @format, @path) ON CONFLICT(image_path) DO NOTHING";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            myCommand.Parameters.AddWithValue("@createdAt", createdAt);
            myCommand.Parameters.AddWithValue("@width", width);
            myCommand.Parameters.AddWithValue("@height", height);
            myCommand.Parameters.AddWithValue("@Aratio", aspectRatio);
            myCommand.Parameters.AddWithValue("@format", format);
            myCommand.Parameters.AddWithValue("@path", path);
            var result = myCommand.ExecuteNonQuery();

            //here we SELECT for the row with that path, should only be one image
            using var commd = new SQLiteCommand("SELECT ID FROM images WHERE image_path = '@path'", this.myConnection);
            commd.Parameters.AddWithValue("@path", path);
            using SQLiteDataReader rdr = commd.ExecuteReader();
            while(rdr.Read())
            {
                ID = rdr.GetInt32(0);
            }
            this.CloseConnection();
            return ID;
        }

        //inserts all tags currently in the tag box.
        public void addTags(int imageID,List<string> tags)
        {
            //using an upsert so if function tries to write row that already has a certain tag_name
            //do nothing
            this.OpenConnection();
            using (var transaction = this.myConnection.BeginTransaction())
            {
                var command = this.myConnection.CreateCommand();
                command.CommandText =
                @"INSERT INTO tags(tag_name) VALUES (@name) ON CONFLICT(tag_name) DO NOTHING;";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@name";
                command.Parameters.Add(parameter);

                // Insert a lot of data
                var random = new Random();
                for (var i = 0; i < tags.Count; i++)
                {
                    parameter.Value = tags[i];
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            this.CloseConnection();
        }

        /// Attempt at Implementing the SQLite Commands into Functions for actual use:
        /// #1:
        /// 
        public void image_Path(string userInput)
        {
            string query = "SELECT image_path FROM images WHERE ID = @input";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            myCommand.Parameters.AddWithValue("@input", userInput);
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }
        /// #2:
        public void check_If_Exists_Tag(string userInputT_Name, string userInput)        //I probably need to look at this one again for how to implement it
        {
            string query = "INSERT INTO tags (tag_name, image_count, description)";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            myCommand.Parameters.AddWithValue("@input", userInput);
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }

        // Changed void to int so it can return tag_Count - KC
        public int tag_Count()
        {
            string query = "SELECT tag_name, COUNT * FROM tags";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
            return result;
        }

        public void all_Tags()
        {
            string query = "SELECT * FROM tags";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }

        public void tag_Specific_Count(string userInputTag_Name)
        {
            string query = "SELECT * FROM image_tags WHERE tag_id = (SELECT tag_id FROM tags WHERE tag_name = @input)";
            SQLiteCommand myCommand = new SQLiteCommand(query, this.myConnection);
            this.OpenConnection();
            myCommand.Parameters.AddWithValue("@input", userInputTag_Name);
            var result = myCommand.ExecuteNonQuery();
            this.CloseConnection();
        }

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
        //this function takes in an image ID and a list of strings, that ID is associated with each of the tags
        public void add_Tag_Image_relation(int Img_ID, List<string> tagNameList)
        {
            this.OpenConnection();
            //we use a transaction because it could be a large amount of INSERTS being made and this is faster
            using (var transaction = this.myConnection.BeginTransaction())
            {
                var command = this.myConnection.CreateCommand();
                //using a subquery, we switch the tag name for an ID by referencing the DB
                command.CommandText =
                "INSERT INTO image_tags (image_id, tag_id) VALUES (@ImgID, (SELECT tag_id FROM tags WHERE tag_name = @tagName));";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@tagName";
                command.Parameters.AddWithValue("@ImgID", Img_ID);
                command.Parameters.Add(parameter);

                foreach (string item in tagNameList)
                {
                    parameter.Value = item;
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
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

        // Changed string userInputImg_ID to int userInputImg_ID - KC
        public void add_Img_To_Gallery(int userInputImg_ID, string userInputG_Name)
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

        // Changed string userInputG_ID to int userInputG_ID - KC
        public void get_All_Img_In_Gal(int userInputG_ID)
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

        // Changed string userInputG_ID to int userInputG_ID - KC
        public void gallery_Info(int userInputG_ID)
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
