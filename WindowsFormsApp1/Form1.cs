using DeskBooruApp;
using System;
using System.Collections.Generic;
using System.Data;
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
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
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
            GlobalStatics.currentTagsFolder.Clear();
            AddFolderCurrentTagTextBox.Clear();
        }

        private void clearTagsSingleButton_Click(object sender, EventArgs e)
        {
            GlobalStatics.currentTagsSingle.Clear();
            AddImageCurrentTagTexBox.Clear();
        }
    }
}
