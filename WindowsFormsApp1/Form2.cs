using DeskBooruApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DeskBooruApp
{

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void InfoSaveButton_Click(object sender, EventArgs e)
        {

        }

        public void UpdatePictureBox(System.Drawing.Image picture)
        {
            if (this.InfoPictureBox.Image != null)
            {
                this.InfoPictureBox.Image.Dispose();
            }
            this.InfoPictureBox.Image = picture;
        }
    }
}
