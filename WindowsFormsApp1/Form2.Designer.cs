
namespace DeskBooruApp
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.InfoPictureBox = new System.Windows.Forms.PictureBox();
            this.InfoTagTextBox = new System.Windows.Forms.TextBox();
            this.InfoLabel1 = new System.Windows.Forms.Label();
            this.InfoLabel2 = new System.Windows.Forms.Label();
            this.InfoCommentTextBox = new System.Windows.Forms.TextBox();
            this.InfoSaveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.InfoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // InfoPictureBox
            // 
            this.InfoPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("InfoPictureBox.BackgroundImage")));
            this.InfoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InfoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("InfoPictureBox.Image")));
            this.InfoPictureBox.Location = new System.Drawing.Point(88, 24);
            this.InfoPictureBox.Name = "InfoPictureBox";
            this.InfoPictureBox.Size = new System.Drawing.Size(327, 413);
            this.InfoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.InfoPictureBox.TabIndex = 0;
            this.InfoPictureBox.TabStop = false;
            // 
            // InfoTagTextBox
            // 
            this.InfoTagTextBox.Location = new System.Drawing.Point(88, 464);
            this.InfoTagTextBox.Multiline = true;
            this.InfoTagTextBox.Name = "InfoTagTextBox";
            this.InfoTagTextBox.PlaceholderText = "Current tags will display here";
            this.InfoTagTextBox.ReadOnly = true;
            this.InfoTagTextBox.Size = new System.Drawing.Size(327, 89);
            this.InfoTagTextBox.TabIndex = 1;
            // 
            // InfoLabel1
            // 
            this.InfoLabel1.AutoSize = true;
            this.InfoLabel1.BackColor = System.Drawing.Color.Transparent;
            this.InfoLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InfoLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.InfoLabel1.Location = new System.Drawing.Point(88, 440);
            this.InfoLabel1.Name = "InfoLabel1";
            this.InfoLabel1.Size = new System.Drawing.Size(40, 21);
            this.InfoLabel1.TabIndex = 2;
            this.InfoLabel1.Text = "Tags";
            // 
            // InfoLabel2
            // 
            this.InfoLabel2.AutoSize = true;
            this.InfoLabel2.BackColor = System.Drawing.Color.Transparent;
            this.InfoLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InfoLabel2.ForeColor = System.Drawing.SystemColors.Control;
            this.InfoLabel2.Location = new System.Drawing.Point(88, 556);
            this.InfoLabel2.Name = "InfoLabel2";
            this.InfoLabel2.Size = new System.Drawing.Size(89, 21);
            this.InfoLabel2.TabIndex = 3;
            this.InfoLabel2.Text = "Comments:";
            // 
            // InfoCommentTextBox
            // 
            this.InfoCommentTextBox.Location = new System.Drawing.Point(88, 580);
            this.InfoCommentTextBox.Multiline = true;
            this.InfoCommentTextBox.Name = "InfoCommentTextBox";
            this.InfoCommentTextBox.PlaceholderText = "Write any comments you want to attach to this image";
            this.InfoCommentTextBox.Size = new System.Drawing.Size(327, 62);
            this.InfoCommentTextBox.TabIndex = 4;
            // 
            // InfoSaveButton
            // 
            this.InfoSaveButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("InfoSaveButton.BackgroundImage")));
            this.InfoSaveButton.FlatAppearance.BorderSize = 0;
            this.InfoSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InfoSaveButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.InfoSaveButton.Location = new System.Drawing.Point(190, 654);
            this.InfoSaveButton.Name = "InfoSaveButton";
            this.InfoSaveButton.Size = new System.Drawing.Size(116, 22);
            this.InfoSaveButton.TabIndex = 5;
            this.InfoSaveButton.Text = "Save";
            this.InfoSaveButton.UseVisualStyleBackColor = true;
            this.InfoSaveButton.Click += new System.EventHandler(this.InfoSaveButton_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(506, 688);
            this.Controls.Add(this.InfoSaveButton);
            this.Controls.Add(this.InfoCommentTextBox);
            this.Controls.Add(this.InfoLabel2);
            this.Controls.Add(this.InfoLabel1);
            this.Controls.Add(this.InfoTagTextBox);
            this.Controls.Add(this.InfoPictureBox);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "DeskBooru - Image Info";
            ((System.ComponentModel.ISupportInitialize)(this.InfoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox InfoPictureBox;
        private System.Windows.Forms.TextBox InfoTagTextBox;
        private System.Windows.Forms.Label InfoLabel1;
        private System.Windows.Forms.Label InfoLabel2;
        private System.Windows.Forms.TextBox InfoCommentTextBox;
        private System.Windows.Forms.Button InfoSaveButton;
    }
}