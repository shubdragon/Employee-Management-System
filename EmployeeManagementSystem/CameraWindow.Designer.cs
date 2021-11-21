namespace EmployeeManagementSystem
{
    partial class CameraWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraWindow));
            this.FramePictureBox = new System.Windows.Forms.PictureBox();
            this.CaptureButton = new MetroFramework.Controls.MetroButton();
            this.CamOkayButton = new MetroFramework.Controls.MetroButton();
            this.CameraStartButton = new MetroFramework.Controls.MetroButton();
            this.ImageSettingsButton = new MetroFramework.Controls.MetroButton();
            this.CameraSettingsButton = new MetroFramework.Controls.MetroButton();
            this.NetCameraControl = new Camera_NET.CameraControl();
            this.CameraComboBox = new MetroFramework.Controls.MetroComboBox();
            this.ResolutionComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.ResizerPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FramePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResizerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FramePictureBox
            // 
            this.FramePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FramePictureBox.InitialImage = null;
            this.FramePictureBox.Location = new System.Drawing.Point(249, 23);
            this.FramePictureBox.Name = "FramePictureBox";
            this.FramePictureBox.Size = new System.Drawing.Size(215, 167);
            this.FramePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FramePictureBox.TabIndex = 1;
            this.FramePictureBox.TabStop = false;
            // 
            // CaptureButton
            // 
            this.CaptureButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CaptureButton.Location = new System.Drawing.Point(200, 205);
            this.CaptureButton.Name = "CaptureButton";
            this.CaptureButton.Size = new System.Drawing.Size(75, 23);
            this.CaptureButton.TabIndex = 2;
            this.CaptureButton.Text = "Capture";
            this.CaptureButton.UseSelectable = true;
            this.CaptureButton.Click += new System.EventHandler(this.CaptureButton_Click);
            // 
            // CamOkayButton
            // 
            this.CamOkayButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CamOkayButton.Location = new System.Drawing.Point(281, 205);
            this.CamOkayButton.Name = "CamOkayButton";
            this.CamOkayButton.Size = new System.Drawing.Size(75, 23);
            this.CamOkayButton.TabIndex = 3;
            this.CamOkayButton.Text = "Okay";
            this.CamOkayButton.UseSelectable = true;
            this.CamOkayButton.Click += new System.EventHandler(this.CamOkayButton_Click);
            // 
            // CameraStartButton
            // 
            this.CameraStartButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CameraStartButton.Location = new System.Drawing.Point(119, 205);
            this.CameraStartButton.Name = "CameraStartButton";
            this.CameraStartButton.Size = new System.Drawing.Size(75, 23);
            this.CameraStartButton.TabIndex = 1;
            this.CameraStartButton.Text = "Start";
            this.CameraStartButton.UseSelectable = true;
            this.CameraStartButton.Click += new System.EventHandler(this.CameraStartButton_Click);
            // 
            // ImageSettingsButton
            // 
            this.ImageSettingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImageSettingsButton.Location = new System.Drawing.Point(246, 309);
            this.ImageSettingsButton.Name = "ImageSettingsButton";
            this.ImageSettingsButton.Size = new System.Drawing.Size(218, 23);
            this.ImageSettingsButton.TabIndex = 7;
            this.ImageSettingsButton.Text = "Image Settings";
            this.ImageSettingsButton.UseSelectable = true;
            this.ImageSettingsButton.Click += new System.EventHandler(this.ImageSettingsButton_Click);
            // 
            // CameraSettingsButton
            // 
            this.CameraSettingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CameraSettingsButton.Location = new System.Drawing.Point(23, 309);
            this.CameraSettingsButton.Name = "CameraSettingsButton";
            this.CameraSettingsButton.Size = new System.Drawing.Size(218, 23);
            this.CameraSettingsButton.TabIndex = 8;
            this.CameraSettingsButton.Text = "Camera Settings";
            this.CameraSettingsButton.UseSelectable = true;
            this.CameraSettingsButton.Click += new System.EventHandler(this.CameraSettingsButton_Click);
            // 
            // NetCameraControl
            // 
            this.NetCameraControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NetCameraControl.DirectShowLogFilepath = "";
            this.NetCameraControl.Location = new System.Drawing.Point(23, 23);
            this.NetCameraControl.Name = "NetCameraControl";
            this.NetCameraControl.Size = new System.Drawing.Size(215, 167);
            this.NetCameraControl.TabIndex = 9;
            // 
            // CameraComboBox
            // 
            this.CameraComboBox.FormattingEnabled = true;
            this.CameraComboBox.IntegralHeight = false;
            this.CameraComboBox.ItemHeight = 23;
            this.CameraComboBox.Location = new System.Drawing.Point(89, 239);
            this.CameraComboBox.Name = "CameraComboBox";
            this.CameraComboBox.Size = new System.Drawing.Size(375, 29);
            this.CameraComboBox.TabIndex = 10;
            this.CameraComboBox.UseSelectable = true;
            this.CameraComboBox.SelectedIndexChanged += new System.EventHandler(this.CameraComboBox_SelectedIndexChanged);
            // 
            // ResolutionComboBox
            // 
            this.ResolutionComboBox.FormattingEnabled = true;
            this.ResolutionComboBox.ItemHeight = 23;
            this.ResolutionComboBox.Location = new System.Drawing.Point(89, 274);
            this.ResolutionComboBox.Name = "ResolutionComboBox";
            this.ResolutionComboBox.Size = new System.Drawing.Size(375, 29);
            this.ResolutionComboBox.TabIndex = 12;
            this.ResolutionComboBox.UseSelectable = true;
            this.ResolutionComboBox.SelectedIndexChanged += new System.EventHandler(this.ResolutionComboBox_SelectedIndexChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(16, 243);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(59, 19);
            this.metroLabel1.TabIndex = 14;
            this.metroLabel1.Text = "Camera:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(16, 278);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(72, 19);
            this.metroLabel2.TabIndex = 15;
            this.metroLabel2.Text = "Resolution:";
            // 
            // ResizerPictureBox
            // 
            this.ResizerPictureBox.Location = new System.Drawing.Point(344, 23);
            this.ResizerPictureBox.Name = "ResizerPictureBox";
            this.ResizerPictureBox.Size = new System.Drawing.Size(120, 140);
            this.ResizerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ResizerPictureBox.TabIndex = 16;
            this.ResizerPictureBox.TabStop = false;
            this.ResizerPictureBox.Visible = false;
            // 
            // CameraWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(487, 355);
            this.Controls.Add(this.ResizerPictureBox);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.ResolutionComboBox);
            this.Controls.Add(this.CameraComboBox);
            this.Controls.Add(this.NetCameraControl);
            this.Controls.Add(this.CameraSettingsButton);
            this.Controls.Add(this.ImageSettingsButton);
            this.Controls.Add(this.CameraStartButton);
            this.Controls.Add(this.CamOkayButton);
            this.Controls.Add(this.CaptureButton);
            this.Controls.Add(this.FramePictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CameraWindow";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraWindow_FormClosing);
            this.Load += new System.EventHandler(this.CameraWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FramePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResizerPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox FramePictureBox;
        private MetroFramework.Controls.MetroButton CaptureButton;
        private MetroFramework.Controls.MetroButton CamOkayButton;
        private MetroFramework.Controls.MetroButton CameraStartButton;
        private MetroFramework.Controls.MetroButton ImageSettingsButton;
        private MetroFramework.Controls.MetroButton CameraSettingsButton;
        private Camera_NET.CameraControl NetCameraControl;
        private MetroFramework.Controls.MetroComboBox CameraComboBox;
        private MetroFramework.Controls.MetroComboBox ResolutionComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.PictureBox ResizerPictureBox;
    }
}