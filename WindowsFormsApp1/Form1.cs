using DeskBooruApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Black;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.White;
        }

        private void TabPanel1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void triggerFolderDialog(object sender, EventArgs e)
        {

            //allowed file extensions
            string[] extensions = new string[] { ".BMP", ".JPG", ".GIF", ".PNG", ",JPEG" };

            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                //make sure nothing is in the list of images.
                GlobalStatics.ImageLocations.Clear();
                //borrowed this to make sure only images are loaded!
                //https://stackoverflow.com/questions/13633687/how-to-load-and-store-a-folder-of-images
                foreach (var file in Directory.GetFiles(folderBrowserDialog1.SelectedPath).Where(f => extensions.Contains(Path.GetExtension(f).ToUpper())))
                    {
                        GlobalStatics.ImageLocations.Add(Path.GetFullPath(file));
                    }
                //fix mode of image to avoid stretching.
                AddFolderPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                //doing this only loads one image into RAM, instead of every image in the folder as done in the link above.
                AddFolderPictureBox.Image = new Bitmap(GlobalStatics.ImageLocations[0]);

                //keep track of where in the array we are.
                GlobalStatics.imageArrayindex = 0;
                //enable buttons to browse images
                forwardButtonFolder.Enabled = true;
                backButtonFolder.Enabled = true;
            }
        }

        private void triggerChooseImageDialog(object sender, EventArgs e)
        {
            //Taken some from the following link
            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=net-5.0

            //this sets the allowed file extensions!
            openFileDialog1.Filter = "Image files (*.bmp, *.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.bmp; *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //initialize imageArray with singular image and data.
                //this allows for functions like "submitButtonclick()" to work for both 
                //the single image page and the folder page
                GlobalStatics.imageArrayindex = 0;
                //need to clear the array first to make sure only one image is in the array
                GlobalStatics.ImageLocations.Clear();
                GlobalStatics.ImageLocations.Add(openFileDialog1.FileName);
                ImagePreviewSingle.Image = new Bitmap(openFileDialog1.FileName);
                ImagePreviewSingle.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void forwardButtonBrowse(object sender, EventArgs e)
        {
            //increment global index if index+1 is not the same as length of imagelocation Array.
            if (GlobalStatics.imageArrayindex + 1 == GlobalStatics.ImageLocations.Count)
            {
                forwardButtonFolder.Enabled = false;
            }
            else
            {
                GlobalStatics.imageArrayindex++;
                backButtonFolder.Enabled = true;
            }
            //dispose of old image to avoid filling up RAM;
            AddFolderPictureBox.Image.Dispose();
            //change image in box to next one in array.
            AddFolderPictureBox.Load(GlobalStatics.ImageLocations[GlobalStatics.imageArrayindex]);
        }
        private void backButtonBrowse(object sender, EventArgs e)
        {
            //increment global index if index+1 is not the same as length of imagelocation Array.
            if (GlobalStatics.imageArrayindex == 0)
            {
                backButtonFolder.Enabled = false;
            }
            else
            {
                GlobalStatics.imageArrayindex--;
                forwardButtonFolder.Enabled = true;
            }
            //dispose of old image to avoid filling up RAM;
            AddFolderPictureBox.Image.Dispose();
            //change image in box to next one in array.
            AddFolderPictureBox.Load(GlobalStatics.ImageLocations[GlobalStatics.imageArrayindex]);
        }

        private void AddImagesAddButton_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(AddImageAddTagTextBox.Text))
            {
                string Tag;
                Tag = AddImageAddTagTextBox.Text;
                AddImageAddTagTextBox.Clear();
                //add tag to List of tags if not already in, preventing duplicates
                if (!GlobalStatics.currentTagsSingle.Contains(Tag))
                {
                    AddImageCurrentTagTexBox.AppendText(Tag);
                    AddImageCurrentTagTexBox.AppendText(", ");
                    GlobalStatics.currentTagsSingle.Add(Tag);
                }

            }
        }

        private void AddFolderAddButton_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(AddFolderAddTagTextBox.Text))
            {
                string Tag;
                Tag = AddFolderAddTagTextBox.Text;
                AddFolderAddTagTextBox.Clear();
                //add tag to List of tags if not already in, preventing duplicates
                if (!GlobalStatics.currentTagsFolder.Contains(Tag))
                {
                    AddFolderCurrentTagTextBox.AppendText(Tag);
                    AddFolderCurrentTagTextBox.AppendText(", ");
                    GlobalStatics.currentTagsFolder.Add(Tag);
                }
                
            }
        }
        //this function makes it so that when a user presses enter with the text box focused
        //then the "add" button is clicked, more user intuitive.
        private void AddFolderAddTagTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                AddFolderAddButton_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        //this function makes it so that when a user presses enter with the text box focused
        //then the "add" button is clicked, more user intuitive.
        private void AddImageAddTagTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddImagesAddButton_Click(sender, e);
                //these two functions prevent windows from making a "DING" when the enter button is pressed
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void clearTagsFolder_Click(object sender, EventArgs e)
        {
            //When clicking "Clear Tags" it will delete everything in current tags if nothing was in the text
            if (String.IsNullOrEmpty(DeleteFolderTagTextBox.Text))
            {
                GlobalStatics.currentTagsFolder.Clear();
                AddFolderCurrentTagTextBox.Clear();
            }
            //Allow to delete specific tags when typed in the text box.
            else
            {
                string Tag;
                Tag = DeleteFolderTagTextBox.Text;

                //Iterate through the list and if the word is found then remove it from the list
                for (int i = 0; i < GlobalStatics.currentTagsFolder.Count; i++)
                {
                    if (Tag == GlobalStatics.currentTagsFolder[i])
                    {
                        GlobalStatics.currentTagsFolder.RemoveAt(i);
                    }
                }
                //Clears the current tags textbox and then print all the elements in the list
                // into the current tags textbox.
                AddFolderCurrentTagTextBox.Clear();
                foreach (string var in GlobalStatics.currentTagsFolder)
                {
                    AddFolderCurrentTagTextBox.AppendText(var);
                    AddFolderCurrentTagTextBox.AppendText(", ");
                }
            }
            DeleteFolderTagTextBox.Clear();
        }

        private void clearTagsSingleButton_Click(object sender, EventArgs e)
        {
            //When clicking "Clear Tags" it will delete everything in current tags if nothing was in the text
            if(String.IsNullOrEmpty(AddImageDeleteTagTextBox.Text))
            {
                GlobalStatics.currentTagsSingle.Clear();
                AddImageCurrentTagTexBox.Clear();
            }
            //Allow to delete specific tags when typed in the text box.
            else
            {
                string Tag;
                Tag = AddImageDeleteTagTextBox.Text;
                
                //Iterate through the list and if the word is found then remove it from the list
                for (int i = 0; i < GlobalStatics.currentTagsSingle.Count; i++)
                {
                    if(Tag == GlobalStatics.currentTagsSingle[i])
                    {
                        GlobalStatics.currentTagsSingle.RemoveAt(i);   
                    }
                }
                //Clears the current tags textbox and then print all the elements in the list
                // into the current tags textbox.
                AddImageCurrentTagTexBox.Clear();
                foreach (string var in GlobalStatics.currentTagsSingle)
                {
                    AddImageCurrentTagTexBox.AppendText(var);
                    AddImageCurrentTagTexBox.AppendText(", ");
                }
            }
            AddImageDeleteTagTextBox.Clear();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            //get location of file from global list
            string location = GlobalStatics.ImageLocations[GlobalStatics.imageArrayindex];
            Database db = new Database();

            //gets data for feeding into database function
            Image currImage = Image.FromFile(location);
            string now = DateTime.Now.ToString("yyyy-MM-dd.HH:mm:ss");
            string aspectRatio = $"{currImage.Width}:{currImage.Height}";

            //gets file name from source location
            //borrowed from https://stackoverflow.com/questions/4073244/how-to-parse-out-image-name-from-image-url
            string fileName = Path.GetFileName(@location);

            //ID will be the ID of the inserted image
            int ID =
            db.insertImage(now, currImage.Width, currImage.Height, aspectRatio, currImage.RawFormat.ToString(), location);
            //insert all the tags in the currenttags box.


            db.addTags(ID, GlobalStatics.currentTagsSingle);

            //now that all tags are in the database, we add our image to tag relation
            db.add_Tag_Image_relation(ID, GlobalStatics.currentTagsSingle);

            //notifies user that the image was added with a green textbox
            SingleImageStatusColor.BackColor = Color.Green;
            SingleImageStatusColor.Text = "Added!";

            //stops memory leaks!
            db.dispose();
            currImage.Dispose();
            
        }

        //TODO move this to Database.cs
        private void TagListRichTexBox_TextChanged(object sender, EventArgs e)
        {
            //Attempting to print tags into textbox
            string text = " ";
            Database db = new Database();
            string query = "Print SELECT * FROM tags";
            SQLiteCommand myCommand = new SQLiteCommand(query, db.myConnection);
            db.OpenConnection();
            var result = myCommand.ExecuteNonQuery();
            SQLiteDataReader DR1 = myCommand.ExecuteReader();
            if (DR1.Read())
            {
                text += DR1.GetValue(0).ToString();
                text += "/n";
            }
            TagListRichTexBox.Text = text;
            db.CloseConnection();
            db.dispose();
            Application.DoEvents();
        }
        // This section of code is for all the picture boxes to open the Form 2
        // So we can change what we can in one small place rather than looking for it

        // This is in the Favorite Panel
        private void Fave_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            //collection of pictureboxes
            PictureBox[] boxes = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8 };
            //turns search string into a list
            List<string> tags = searchTextBox.Text.Split(", ").ToList();
            List<string> imageLocations = db.image_search(tags);
            for (int i = 0; i < 8; i++)
            {
                if (i < imageLocations.Count)
                {
                    //sets the new image for picturebox
                    boxes[i].ImageLocation = imageLocations[i];
                    boxes[i].SizeMode = PictureBoxSizeMode.Zoom;
                    //load a default background so it doesnt ruin the aesthetic
                    boxes[i].BackgroundImage = DeskBooruApp.Properties.Resources.DarkGradient;
                    //refresh to make sure it loads everything!
                    boxes[i].Refresh();
                }
                else
                {
                    boxes[i].Image = null;
                    boxes[i].BackgroundImage = DeskBooruApp.Properties.Resources.DarkGradient;
                    //refresh to make sure it loads everything!
                    boxes[i].Refresh();
                }

            }

        }

        //when user presses enter in textbox, search button is triggered
        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SearchButton_Click(sender, e);
                //these two functions prevent windows from making a "DING" when the enter button is pressed
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
