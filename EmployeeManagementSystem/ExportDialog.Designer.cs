namespace EmployeeManagementSystem
{
    partial class ExportDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportDialog));
            this.LoadingPictureBox = new System.Windows.Forms.PictureBox();
            this.ExportBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ExportingLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadingPictureBox
            // 
            this.LoadingPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("LoadingPictureBox.Image")));
            this.LoadingPictureBox.InitialImage = null;
            this.LoadingPictureBox.Location = new System.Drawing.Point(12, 15);
            this.LoadingPictureBox.Name = "LoadingPictureBox";
            this.LoadingPictureBox.Size = new System.Drawing.Size(106, 91);
            this.LoadingPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LoadingPictureBox.TabIndex = 0;
            this.LoadingPictureBox.TabStop = false;
            // 
            // ExportBackgroundWorker
            // 
            this.ExportBackgroundWorker.WorkerReportsProgress = true;
            this.ExportBackgroundWorker.WorkerSupportsCancellation = true;
            this.ExportBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ExportBackgroundWorker_DoWork);
            this.ExportBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ExportBackgroundWorker_ProgressChanged);
            this.ExportBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ExportBackgroundWorker_RunWorkerCompleted);
            // 
            // ExportingLabel
            // 
            this.ExportingLabel.AutoSize = true;
            this.ExportingLabel.Font = new System.Drawing.Font("Microsoft JhengHei", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportingLabel.Location = new System.Drawing.Point(124, 39);
            this.ExportingLabel.Name = "ExportingLabel";
            this.ExportingLabel.Size = new System.Drawing.Size(202, 44);
            this.ExportingLabel.TabIndex = 1;
            this.ExportingLabel.Text = "Exporting...";
            // 
            // ExportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 124);
            this.ControlBox = false;
            this.Controls.Add(this.ExportingLabel);
            this.Controls.Add(this.LoadingPictureBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "ExportDialog";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.ExportDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LoadingPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox LoadingPictureBox;
        private System.ComponentModel.BackgroundWorker ExportBackgroundWorker;
        private System.Windows.Forms.Label ExportingLabel;
    }
}