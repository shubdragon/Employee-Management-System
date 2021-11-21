using EmployeeManagementSystem.Windows;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string connectionString;
        public static bool IsClient;
        public static string IP;
        public static string dbConnectionString;
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();            
            //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ip"]))
            //{
            //    EnterIPTextBlock.Text = ConfigurationManager.AppSettings["ip"];
            //}
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                PublicIPTextBlock.Cursor = Cursors.Wait;
                string externalip = new WebClient().DownloadString("http://icanhazip.com");
                PublicIPTextBlock.Text = externalip;
                if(PublicIPTextBlock != null)
                {
                    PublicIPTextBlock.Cursor = Cursors.Arrow;
                }
                
            }
            catch
            {
                MessageBox.Show("Unable to detect your public ip, check your internet connection or goto www.whatismyip.com to manually check ip", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                PublicIPTextBlock.Text = "Goto www.whatismyip.com or click me";
                PublicIPTextBlock.FontSize = 15;
                if (PublicIPTextBlock != null)
                {
                    PublicIPTextBlock.Cursor = Cursors.Hand;
                }
            }
        }

        /*
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            
            
            //string[] pubIp = GetContent("http://icanhazip.com").Split('.');
            //pubIp[0] = "11";
            //pubIp[3] = "22";
            //PublicIPTextBlock.Text = pubIp[0] + "." + pubIp[1] + "." + pubIp[2] + "." + pubIp[3];
        }
    */
        /*
        public static string GetContent(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }
        */

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            connectionString = string.Format("Data Source={0}; Database={1}; Integrated Security={2}; User Instance={3}; user={4}; password={5};", EnterIPTextBox.Text + PortStringTextBlock.Text, DataBaseStringTextBlock.Text, IntegratedSecurityStringTextBlock.Text, InstanceStringTextBlock.Text, UserStringTextBlock.Text, PasswordStringTextBlock.Text);
            IP = EnterIPTextBox.Text;
            // AppSetting setting = new AppSetting();
            // setting.SaveConnectionString("EmployeeManagementSystem.Properties.Settings.emsdatabaseConnectionString", connectionString);
            dbConnectionString = connectionString;
            EmployeeManagementSystem.Properties.Settings.Default.IP = EnterIPTextBox.Text;
            EmployeeManagementSystem.Properties.Settings.Default.Save();
            // Integrated Security=False;User Instance=False;user='admin';password='1011535'"


            string Query = "select * from  login where convert(VARCHAR, passwordAdmin) ='" + this.LoginPasswordBox.Password + "'";
            if (UserNameComboBox.Text == "Admin")
            {
                Query = "select * from  login where convert(VARCHAR, passwordAdmin) ='" + this.LoginPasswordBox.Password + "'";
            }
            else if (UserNameComboBox.Text == "Client")
            {
                Query = "select * from  login where convert(VARCHAR, passwordClient) ='" + this.LoginPasswordBox.Password + "'";
                //Query = "select * from  login where convert(VARCHAR, passwordClient) ='" + this.LoginPasswordBox.Password + "'";
            }
            else
            {
                MessageBox.Show("Select a username first", "Stop", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            WaitTextBlock.Visibility = Visibility.Visible;

            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();

                //string QueryClient = "select * from  login where convert(VARCHAR, passwordClient) ='" + this.LoginPasswordBox.Password + "'";
                SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                createCommand.ExecuteNonQuery();
                SqlDataReader dr = createCommand.ExecuteReader();

                int count = 0;
                while (dr.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    if (UserNameComboBox.Text == "Admin")
                    {
                        IsClient = false;
                        DataMgmtWindow win2 = new DataMgmtWindow();
                        win2.Show();
                        this.Hide();
                    }
                    else if (UserNameComboBox.Text == "Client")
                    {
                        IsClient = true;
                        DataMgmtWindow win2 = new DataMgmtWindow();
                        win2.Show();
                        this.Hide();
                    }

                }
                if (count < 1)
                {
                    MessageBox.Show("Username and password is not correct", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    WaitTextBlock.Visibility = Visibility.Collapsed;
                }
                dr.Close();
                sqliteCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                WaitTextBlock.Visibility = Visibility.Collapsed;
            }

            //NavigationService.Navigate(new Uri("Pages/DataMgmt.xaml", UriKind.Relative));
        }

        private void AboutDevButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://twitter.com/shubdragon");
        }

        private void ChangePwTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            connectionString = string.Format("Data Source={0}; Database={1}; Integrated Security={2}; User Instance={3}; user={4}; password={5};", EnterIPTextBox.Text + PortStringTextBlock.Text, DataBaseStringTextBlock.Text, IntegratedSecurityStringTextBlock.Text, InstanceStringTextBlock.Text, UserStringTextBlock.Text, PasswordStringTextBlock.Text);
            AppSetting setting = new AppSetting();
            setting.SaveConnectionString("EmployeeManagementSystem.Properties.Settings.emsdatabaseConnectionString", connectionString);
            dbConnectionString = connectionString;
            LoginBorder.Visibility = Visibility.Collapsed;
            ChangePasswordBorder.Visibility = Visibility.Visible;
            OldPasswordBox.Password = "";
            NewPasswordBox.Password = "";
            RetypePasswordBox.Password = "";            
        }

        private void CancelPwTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            ChangePasswordBorder.Visibility = Visibility.Collapsed;
            LoginBorder.Visibility = Visibility.Visible;
        }

        private void OkayButton_Click(object sender, RoutedEventArgs e)
        {            
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                string OldQuery = "select * from  login where convert(VARCHAR, passwordAdmin)='" + this.OldPasswordBox.Password + "'";
                SqlCommand createCommand = new SqlCommand(OldQuery, sqliteCon);
                createCommand.ExecuteNonQuery();
                SqlDataReader dr = createCommand.ExecuteReader();

                int count = 0;
                while (dr.Read())
                {
                    count = count + 1;
                }
                dr.Close();
                if (count == 1)
                {
                    if(NewPasswordBox.Password == RetypePasswordBox.Password && NewPasswordBox.Password != "" && RetypePasswordBox.Password != "")
                    {
                        try
                        {
                            string Query = "update login set passwordAdmin='" + this.NewPasswordBox.Password + "'";
                            SqlCommand changeCommand = new SqlCommand(Query, sqliteCon);
                            changeCommand.ExecuteNonQuery();
                            sqliteCon.Close();

                            MessageBox.Show("Password Change Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            ChangePasswordBorder.Visibility = Visibility.Collapsed;
                            LoginBorder.Visibility = Visibility.Visible;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("New Password and Confirm Password is not match or cannot be empty","Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    
                }
                if (count < 1)
                {
                    MessageBox.Show("Username and password is not correct", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                dr.Close();
                sqliteCon.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /*
        private void EnterIPTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //UpdateKey("ip", EnterIPTextBlock.Text);

        }
        */
        private void PublicIPTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.whatismyip.com");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            try
            {
                Array.ForEach(Directory.GetFiles(@"App_Data\Temp"), File.Delete);
            }
            catch { }            
            App.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
        }


        /*
        public void UpdateKey(string strKey, string newValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\App.config");

            if (!KeyExists(strKey))
            {
                throw new ArgumentNullException("Key", "<" + strKey + "> does not exist in the configuration. Update failed.");
            }
            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            foreach (XmlNode childNode in appSettingsNode)
            {
                if (childNode.Attributes["key"].Value == strKey)
                    childNode.Attributes["value"].Value = newValue;
            }
            xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\App.config");
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }

        public bool KeyExists(string strKey)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\App.config");

            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            foreach (XmlNode childNode in appSettingsNode)
            {
                if (childNode.Attributes["key"].Value == strKey)
                    return true;
            }
            return false;
        }
        */
    }
}
