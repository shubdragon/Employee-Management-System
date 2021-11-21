using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;
using Camera_NET;
using EmployeeManagementSystem.Windows;
using MetroFramework.Forms;

namespace EmployeeManagementSystem
{
    public partial class CameraWindow : MetroForm
    {
        private CameraChoice _CameraChoice = new CameraChoice();
        //private FilterInfoCollection captureDevice;
        //private VideoCaptureDevice finalFrame;
        // WebCam webcam;
        public CameraWindow()
        {
            InitializeComponent();
        }

        private void CameraWindow_Load(object sender, EventArgs e)
        {            
            /*
            foreach (WebCameraId camera in WebCamControl.GetVideoCaptureDevices())
            {
                VideoSourceComboBox.Items.Add(new ComboBoxItem(camera));
            }

            if (VideoSourceComboBox.Items.Count > 0)
            {
                VideoSourceComboBox.SelectedItem = VideoSourceComboBox.Items[0];
            }
            */

            /*
            try
            {
                //CameraPictureBox.ImageLocation = @"Assets\PressStart.gif";
                //webcam = new WebCam();
                //webcam.InitializeWebCam(ref CameraPictureBox);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Camera Device not found or not compatible. Check Drivers through Device Manager", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CameraPictureBox.ImageLocation = @"Assets\nosource.png";
            }
            */

            /*
            captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);            
            foreach (FilterInfo Device in captureDevice)
            {                
                VidSourceComboBox.Items.Add(Device.Name);
            }

            if(VidSourceComboBox.Items.Count > 0)
            {
                VidSourceComboBox.SelectedIndex = 0;
            }
            else
            {
                VidSourceComboBox.Items.Add("No Device Found or select proper camera from drop down or check a camera device is installed and working properly");
                VidSourceComboBox.SelectedIndex = 0;
            }
            
            finalFrame = new VideoCaptureDevice();

            try
            {
                if(VidSourceComboBox.SelectedIndex > -1)
                {
                    finalFrame = new VideoCaptureDevice(captureDevice[VidSourceComboBox.SelectedIndex].MonikerString);
                    finalFrame.NewFrame += FinalFrame_NewFrame;
                    finalFrame.Start();
                }                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "Camera Device not found or not compatible. Check Drivers through Device Manager", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CameraPictureBox.ImageLocation = @"Assets\nosource.png";
            }
            */
        }
        /*
        private void VidSourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {         
            try
            {
                if (VidSourceComboBox.SelectedIndex > -1)
                {
                    finalFrame = new VideoCaptureDevice(captureDevice[VidSourceComboBox.SelectedIndex].MonikerString);
                    finalFrame.NewFrame += FinalFrame_NewFrame;
                    finalFrame.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Camera Device not found or not compatible. Check Drivers through Device Manager", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CameraPictureBox.ImageLocation = @"Assets\nosource.png";
            }
        }*/

        private void CaptureButton_Click(object sender, EventArgs e)
        {
            if (!NetCameraControl.CameraCreated)
                return;

            Bitmap bitmap = null;
            try
            {
                bitmap = NetCameraControl.SnapshotSourceImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error while getting a snapshot");
            }

            if (bitmap == null)
                return;

            FramePictureBox.Image = bitmap;
            FramePictureBox.Update();
            ResizerPictureBox.Image = bitmap;
            ResizerPictureBox.Update();
            //FramePictureBox.Image = WebCamControl.GetCurrentImage();
            //FramePictureBox.Image = CameraPictureBox.Image;            
        }

        /*
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            CameraPictureBox.Image = (System.Drawing.Bitmap)eventArgs.Frame.Clone();
        }        
        */
        private void CamOkayButton_Click(object sender, EventArgs e)
        {
            if (FramePictureBox.Image != null)
            {
                using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ResizerPictureBox.Width, ResizerPictureBox.Height))
                {
                    ResizerPictureBox.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));
                    ResizerPictureBox.Image = new System.Drawing.Bitmap(ResizerPictureBox.Width, ResizerPictureBox.Height);
                    bmp.Save(@"App_Data\Temp\New.jpg", ImageFormat.Jpeg);
                }
                /* using (var image = FramePictureBox.Image)
                 using (var newImage = ResizeImage(image, new Size(140, 180)))
                 {
                     newImage.Save(@"App_Data\Temp\New.jpg", ImageFormat.Jpeg);
                 }*/

                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.GetType() == typeof(DataMgmtWindow))
                    {
                        (window as DataMgmtWindow).EmpPictureBox.ImageLocation = @"App_Data\Temp\New.jpg";
                    }
                }
            }
            else
            {
                MessageBox.Show("No Image Captured", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            /*
            if (finalFrame.IsRunning == true)
            {
                finalFrame.SignalToStop();
                finalFrame.Stop();
                finalFrame.NewFrame -= FinalFrame_NewFrame;
                finalFrame = null;
            }
            */
            
            this.Close();

            //System.Drawing.Bitmap bmp = ((System.Drawing.Bitmap)(ImagePictureBox.Image));
            //System.Drawing.Bitmap bmpt = new System.Drawing.Bitmap(100, 120);            
            //System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmpt);
            //  draw the original image to the smaller thumb
            //g.DrawImage(bmp, 0, 0, (bmpt.Width + 1), (bmpt.Height + 1));            
            //bmpt.Save(@"App_Data\New.jpg", ImageFormat.Jpeg);
        }

        private void CameraWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                NetCameraControl.CloseCamera();
                //webcam.Stop();
            }
            catch
            {

            }
        }

        /* // Background Task
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
        }
        */

        private void CameraStartButton_Click(object sender, EventArgs e)
        {
            FillCameraList();

            if (CameraComboBox.Items.Count > 0)
            {
                CameraComboBox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No Camera Found", "Camera Missing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            FillResolutionList();


            /*
            ComboBoxItem i = (ComboBoxItem)VideoSourceComboBox.SelectedItem;

            try
            {
                WebCamControl.StartCapture(i.Id);
            }
            finally
            {
                //UpdateButtons();
            }
            */
        }


        private void CameraSettingsButton_Click(object sender, EventArgs e)
        {
            //webcam.AdvanceSetting();
            if (NetCameraControl.CameraCreated)
            {
                Camera.DisplayPropertyPage_Device(NetCameraControl.Moniker, this.Handle);
            }
        }

        private void ImageSettingsButton_Click(object sender, EventArgs e)
        {
            //webcam.ResolutionSetting();
            if (NetCameraControl.CameraCreated)
            {
                NetCameraControl.DisplayPropertyPage_SourcePinOutput(this.Handle);
            }
        }
      

        /*
        public static System.Drawing.Bitmap ResizedImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new System.Drawing.Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = System.Drawing.Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }
        */

        /*
        private class ComboBoxItem
        {
            public ComboBoxItem(WebCameraId id)
            {
                _id = id;
            }

            private readonly WebCameraId _id;
            public WebCameraId Id
            {
                get { return _id; }
            }

            public override string ToString()
            {
                // Generates the text shown in the combo box.
                return _id.Name;
            }
        }
        */

        private void CameraComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CameraComboBox.SelectedIndex < 0)
            {
                NetCameraControl.CloseCamera();
            }
            else
            {
                // Set camera
                SetCamera(_CameraChoice.Devices[CameraComboBox.SelectedIndex].Mon, null);
            }

            FillResolutionList();
        }

        private void ResolutionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!NetCameraControl.CameraCreated)
                return;

            int comboBoxResolutionIndex = ResolutionComboBox.SelectedIndex;
            if (comboBoxResolutionIndex < 0)
            {
                return;
            }
            ResolutionList resolutions = Camera.GetResolutionList(NetCameraControl.Moniker);

            if (resolutions == null)
                return;

            if (comboBoxResolutionIndex >= resolutions.Count)
                return; // throw

            if (0 == resolutions[comboBoxResolutionIndex].CompareTo(NetCameraControl.Resolution))
            {
                // this resolution is already selected
                return;
            }

            // Recreate camera
            SetCamera(NetCameraControl.Moniker, resolutions[comboBoxResolutionIndex]);
        }

        private void FillCameraList()
        {
            CameraComboBox.Items.Clear();

            _CameraChoice.UpdateDeviceList();

            foreach (var camera_device in _CameraChoice.Devices)
            {
                CameraComboBox.Items.Add(camera_device.Name);
            }
        }

        private void FillResolutionList()
        {
            ResolutionComboBox.Items.Clear();

            if (!NetCameraControl.CameraCreated)
                return;

            ResolutionList resolutions = Camera.GetResolutionList(NetCameraControl.Moniker);

            if (resolutions == null)
                return;


            int index_to_select = -1;

            for (int index = 0; index < resolutions.Count; index++)
            {
                ResolutionComboBox.Items.Add(resolutions[index].ToString());

                if (resolutions[index].CompareTo(NetCameraControl.Resolution) == 0)
                {
                    index_to_select = index;
                }
            }

            if (index_to_select >= 0)
            {
                ResolutionComboBox.SelectedIndex = index_to_select;
            }
        }

        private void SetCamera(IMoniker camera_moniker, Resolution resolution)
        {
            try
            {
                NetCameraControl.SetCamera(camera_moniker, resolution);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, @"Error while running camera");
            }

            if (!NetCameraControl.CameraCreated)
                return;

            NetCameraControl.MixerEnabled = true;
        }

        public static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

    }
}