namespace EmployeeManagementSystem
{
    partial class ImportFromExcel
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
            this.BrowseButton = new MetroFramework.Controls.MetroButton();
            this.UploadButton = new MetroFramework.Controls.MetroButton();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.Pb1 = new MetroFramework.Controls.MetroProgressBar();
            this.SuspendLayout();
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(428, 26);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(98, 27);
            this.BrowseButton.TabIndex = 0;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseSelectable = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.Location = new System.Drawing.Point(428, 75);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(98, 27);
            this.UploadButton.TabIndex = 1;
            this.UploadButton.Text = "Upload";
            this.UploadButton.UseSelectable = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(12, 28);
            this.PathTextBox.MinimumSize = new System.Drawing.Size(392, 25);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.ReadOnly = true;
            this.PathTextBox.Size = new System.Drawing.Size(392, 20);
            this.PathTextBox.TabIndex = 2;
            // 
            // Pb1
            // 
            this.Pb1.Location = new System.Drawing.Point(12, 75);
            this.Pb1.MarqueeAnimationSpeed = 99;
            this.Pb1.Maximum = 10;
            this.Pb1.Name = "Pb1";
            this.Pb1.Size = new System.Drawing.Size(392, 23);
            this.Pb1.Step = 5;
            this.Pb1.Style = MetroFramework.MetroColorStyle.Green;
            this.Pb1.TabIndex = 3;
            // 
            // ImportFromExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 128);
            this.Controls.Add(this.Pb1);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.BrowseButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportFromExcel";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Import From Excel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton BrowseButton;
        private MetroFramework.Controls.MetroButton UploadButton;
        private System.Windows.Forms.TextBox PathTextBox;
        private MetroFramework.Controls.MetroProgressBar Pb1;
    }
}