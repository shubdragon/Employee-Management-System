using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using MahApps.Metro.Controls;
using System.ComponentModel;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using MahApps.Metro.Controls.Dialogs;
using System.Globalization;

namespace EmployeeManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for DataMgmtWindow.xaml
    /// </summary>
    public partial class DataMgmtWindow : MetroWindow, INotifyPropertyChanged
    {
        string dbConnectionString = MainWindow.dbConnectionString;//EmployeeManagementSystem.Properties.Settings.Default.emsdatabaseConnectionString;
        string IP = MainWindow.IP;
        //List<string> genderList;
        //string imgLocation;
        bool IsClient = MainWindow.IsClient;
        bool IsAdd = false;
        string Tab2Query;
        string mySelectedDate;
        public static string EidFetch;
        public static string StartEidFetch;
        public static string EndEidFetch;
        public static string LoadReport;
        public static string YearFetch;
        public static DateTime? SalaryDate;
        public static DateTime? OrgMonthDate;


        public DataMgmtWindow()
        {
            InitializeComponent();
            DisableText();
            RatesLoad();
            EstNameLoad();            
            RefreshGrid();
            if (IsClient == true)
            {
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                ParametersUpdateButton.IsEnabled = false;
                RestoreButton.IsEnabled = false;
            }
            /*searchData("");
            //FillComboBox();
            genderList = new List<string>
            {
                "Male","male","Female","female","Other","other"
            };
            GenderTextBox.TextChanged += new TextChangedEventHandler(GenderTextBox_TextChanged);
            */
            Tab2YearComboBox.Items.Clear();
            Tab3YearComboBox.Items.Clear();
            for (int i = 2005; i <= (DateTime.Now.Year + 1); i++)
            {
                Tab2YearComboBox.Items.Add(i);
                Tab3YearComboBox.Items.Add(i);
                Tab3SSYearComboBox.Items.Add(i);
                Tab3YearOrgComboBox.Items.Add(i);
                Tab3YYearOrgComboBox.Items.Add(i);
            }
        }

        private void HeaderRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void HeaderEstNameUpdate_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                string Query = "update parameters set ESTNAME = '" + this.EstNameTextBox.Text + "'";
                SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                createCommand.ExecuteNonQuery();
                sqliteCon.Close();
                MessageBox.Show("Saved Sucessfully");
            }
            catch (Exception ex)
            {
                sqliteCon.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void EstNameLoad()
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            string Query = "select ESTNAME from parameters";
            SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
            try
            {
                sqliteCon.Open();
                using (SqlDataReader dr = createCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        EstNameTextBox.Text = dr["ESTNAME"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EidTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void GlobalTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9.]+");
        }

        // Tab1
        #region Tab1
        private void LogoutButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow win1 = new MainWindow();
            win1.Show();
            this.Hide();
        }        

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddButton.Content.Equals("Add"))
            {
                AddButton.Content = "Cancel";
                EnableText();
                SaveButton.IsEnabled = true;
                if(IsClient == false)
                {
                    EditButton.IsEnabled = false;
                    DeleteButton.IsEnabled = false;
                }                
                PrevButton.IsEnabled = false;
                NextButton.IsEnabled = false;
                IFSCTextBox.Text = "";
                AcTextBox.Text = "";
                EidTextBox.Text = "";
                NameTextBox.Text = "";
                FHTextBox.Text = "";
                UanTextBox.Text = "";
                EsicTextBox.Text = "";
                MobileTextBox.Text = "";
                AadhaarTextBox.Text = "";
                GenderComboBox.SelectedIndex = -1;
                EmailTextBox.Text = "";
                DojTextBox.Text = "";
                DolTextBox.Text = "";
                DobTextBox.Text = "";
                RelationComboBox.SelectedIndex = -1;
                RoleTextBox.Text = "";
                AddressTextBox.Text = "";
                PrintButton.IsEnabled = false;
                ExportDataButton.IsEnabled = false;
                ImportDataButton.IsEnabled = false;
                TemplateButton.IsEnabled = false;
                BrowseButton.IsEnabled = true;
                RemoveButton.IsEnabled = true;
                DownloadButton.IsEnabled = false;
                EmpPictureBox.Image = null;
                EmpPictureBox.ImageLocation = @"Assets\noimage.jpg";
            }
            else
            {
                AddButton.Content = "Add";
                DisableText();
                SaveButton.IsEnabled = false;
                if(IsClient == false)
                {
                    EditButton.IsEnabled = true;
                    DeleteButton.IsEnabled = true;
                }               
                PrevButton.IsEnabled = true;
                NextButton.IsEnabled = true;
                IFSCTextBox.Text = "";
                AcTextBox.Text = "";
                EidTextBox.Text = "";
                NameTextBox.Text = "";
                FHTextBox.Text = "";
                UanTextBox.Text = "";
                EsicTextBox.Text = "";
                MobileTextBox.Text = "";
                AadhaarTextBox.Text = "";
                GenderComboBox.SelectedIndex = -1;
                EmailTextBox.Text = "";
                DojTextBox.Text = "";
                DolTextBox.Text = "";
                DobTextBox.Text = "";
                RelationComboBox.SelectedIndex = -1;
                RoleTextBox.Text = "";
                AddressTextBox.Text = "";
                PrintButton.IsEnabled = true;
                ExportDataButton.IsEnabled = true;
                ImportDataButton.IsEnabled = true;
                TemplateButton.IsEnabled = true;
                BrowseButton.IsEnabled = false;
                RemoveButton.IsEnabled = false;
                DownloadButton.IsEnabled = true;
                TableDataGrid.SelectedIndex = 0;
            }
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (EidTextBox.Text != "" && NameTextBox.Text != "" && FHTextBox.Text != "" && RelationComboBox.SelectedIndex != -1 && GenderComboBox.SelectedIndex != -1 && (DojTextBox.Text != "__/__/____" || DojTextBox.Text != "__-__-____") && (DobTextBox.Text != "__/__/____" || DobTextBox.Text != "__-__-____") && DojTextBox.IsMaskCompleted && DobTextBox.IsMaskCompleted)
            {
                if (!AadhaarTextBox.IsMaskCompleted || !DolTextBox.IsMaskCompleted)
                {
                    AadhaarTextBox.Text = "000000000000";
                    DolTextBox.Text = "00000000";
                }
                //byte[] imageBt = null;
                //FileStream fstream = new FileStream(this.ImagePathTextBlock.Text, FileMode.Open, FileAccess.Read);
                //BinaryReader br = new BinaryReader(fstream);
                //imageBt = br.ReadBytes((int)fstream.Length);
                MemoryStream ms = new MemoryStream();
                EmpPictureBox.Image.Save(ms, EmpPictureBox.Image.RawFormat);
                byte[] img = ms.ToArray();

                SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
                try
                {
                    sqliteCon.Open();
                    string UpdateQuery = "select Eid,Name,FatherOrHusbandName,Relation,UAN,[ESIC no],Mobile,Aadhaar,Gender,Email,DOJ,DOL,DOB,IFSC,Account,Role,Address from employeeinfo ";
                    string Query = "insert into employeeinfo (Eid,Name,FatherOrHusbandName,Relation,UAN,[ESIC no],Mobile,Aadhaar,Gender,Email,DOJ,DOL,DOB,IFSC,Account,Role,Address,Image) values('" + this.EidTextBox.Text + "','" + this.NameTextBox.Text + "','" + this.FHTextBox.Text + "','" + this.RelationComboBox.Text + "','" + this.UanTextBox.Text + "','" + this.EsicTextBox.Text + "','" + this.MobileTextBox.Text + "','" + this.AadhaarTextBox.Text + "','" + this.GenderComboBox.Text + "','" + this.EmailTextBox.Text + "','" + this.DojTextBox.Text + "','" + DolTextBox.Text + "','" + this.DobTextBox.Text + "','" + this.IFSCTextBox.Text + "','" + this.AcTextBox.Text + "','" + this.RoleTextBox.Text + "','" + this.AddressTextBox.Text + "',@img)";
                    SqlCommand createCommand = new SqlCommand(Query, sqliteCon);

                    createCommand.Parameters.Add("@img", SqlDbType.Image).Value = img;
                    //createCommand.Parameters.Add(new SqlParameter("@img", imageBt));
                    //createCommand.Parameters.Add("@value", S.Value = int.Parse(AadhaarTextBox.Text);
                    SqlCommand updateCommand = new SqlCommand(UpdateQuery, sqliteCon);

                    createCommand.ExecuteNonQuery();
                   
                    SqlDataAdapter dataAdp = new SqlDataAdapter(updateCommand);
                    DataTable dt = new DataTable("employeeinfo");
                    dataAdp.Fill(dt);
                    TableDataGrid.ItemsSource = dt.DefaultView;
                    dataAdp.Update(dt);
                    sqliteCon.Close();

                    DisableText();
                    SaveButton.IsEnabled = false;
                    EditButton.IsEnabled = true;
                    DeleteButton.IsEnabled = true;
                    PrevButton.IsEnabled = true;
                    NextButton.IsEnabled = true;
                    AddButton.Content = "Add";
                    PrintButton.IsEnabled = true;
                    ExportDataButton.IsEnabled = true;
                    ImportDataButton.IsEnabled = true;
                    TemplateButton.IsEnabled = true;
                    BrowseButton.IsEnabled = false;
                    RemoveButton.IsEnabled = false;
                    DownloadButton.IsEnabled = true;
                    //SearchComboBox.Items.Clear();
                    //FillComboBox();
                    MessageBox.Show("Saved Sucessfully");
                }
                catch (Exception ex)
                {
                    sqliteCon.Close();
                    MessageBox.Show(ex.Message);
                }
            } 
            else
            {
                if(!DojTextBox.IsMaskCompleted || !DobTextBox.IsMaskCompleted)
                {
                    MessageBox.Show("Error\n" + "DOB or DOJ should be in 00-00-000 format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Error\n" + "Eid, Name, Father or Husband Name, Relation, Gender, DOJ and DOB can't be empty" + "for empty date use 00-00-0000", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }                
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditButton.Content.Equals("Edit"))
            {
                EnableText();
                EidTextBox.IsEnabled = false;
                EditButton.Content = "Cancel";
                UpdateButton.IsEnabled = true;
                AddButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                PrevButton.IsEnabled = false;
                NextButton.IsEnabled = false;
                PrintButton.IsEnabled = false;
                ExportDataButton.IsEnabled = false;
                ImportDataButton.IsEnabled = false;
                TemplateButton.IsEnabled = false;
                BrowseButton.IsEnabled = true;
                RemoveButton.IsEnabled = true;
                DownloadButton.IsEnabled = false;
            }
            else
            {
                DisableText();
                EditButton.Content = "Edit";
                UpdateButton.IsEnabled = false;
                AddButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                PrevButton.IsEnabled = true;
                NextButton.IsEnabled = true;
                PrintButton.IsEnabled = true;
                ExportDataButton.IsEnabled = true;
                ImportDataButton.IsEnabled = true;
                TemplateButton.IsEnabled = true;
                BrowseButton.IsEnabled = false;
                RemoveButton.IsEnabled = false;
                DownloadButton.IsEnabled = true;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (EidTextBox.Text != "" && NameTextBox.Text != "" && FHTextBox.Text != "" && RelationComboBox.SelectedIndex != -1 && GenderComboBox.SelectedIndex != -1 && (DojTextBox.Text != "__/__/____" || DojTextBox.Text != "__-__-____") && (DobTextBox.Text != "__/__/____" || DobTextBox.Text != "__-__-____") && DojTextBox.IsMaskCompleted && DobTextBox.IsMaskCompleted)
            {
                if (!AadhaarTextBox.IsMaskCompleted || !DolTextBox.IsMaskCompleted)
                {
                    AadhaarTextBox.Text = "000000000000";
                    DolTextBox.Text = "00000000";
                }

                MemoryStream ms = new MemoryStream();
                EmpPictureBox.Image.Save(ms, EmpPictureBox.Image.RawFormat);
                byte[] img = ms.ToArray();

                SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
                try
                {
                    sqliteCon.Open();
                    /*string UantextBoxText = AadhaarTextBox.Text;
                    UantextBoxText = UantextBoxText.Replace("-", "");
                    int number = int.Parse(UantextBoxText);
                    */
                    string UpdateQuery = "select Eid,Name,FatherOrHusbandName,Relation,UAN,[ESIC no],Mobile,Aadhaar,Gender,Email,DOJ,DOL,DOB,IFSC,Account,Role,Address from employeeinfo ";
                    string Query = "update employeeinfo set Eid='" + this.EidTextBox.Text + "',Name='" + this.NameTextBox.Text + "',FatherOrHusbandName='" + this.FHTextBox.Text + "',Relation='" + this.RelationComboBox.Text.ToString() + "',UAN='" + this.UanTextBox.Text + "',[ESIC no]='" + this.EsicTextBox.Text + "',Mobile='" + this.MobileTextBox.Text + "',Aadhaar='" + this.AadhaarTextBox.Text + "',Gender='" + this.GenderComboBox.Text + "',Email='" + this.EmailTextBox.Text + "',DOJ='" + this.DojTextBox.Text + "',DOL='" + this.DolTextBox.Text + "',DOB='" + this.DobTextBox.Text + "',IFSC='" + this.IFSCTextBox.Text + "',Account='" + this.AcTextBox.Text + "',Role='" + this.RoleTextBox.Text + "',Address='" + this.AddressTextBox.Text + "',Image=@img where Eid='" + this.EidTextBox.Text + "'";
                    SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                    createCommand.Parameters.Add("@img", SqlDbType.Image).Value = img;

                    SqlCommand updateCommand = new SqlCommand(UpdateQuery, sqliteCon);
                    createCommand.ExecuteNonQuery();

                    SqlDataAdapter dataAdp = new SqlDataAdapter(updateCommand);
                    DataTable dt = new DataTable("employeeinfo");
                    dataAdp.Fill(dt);
                    TableDataGrid.ItemsSource = dt.DefaultView;
                    dataAdp.Update(dt);

                    sqliteCon.Close();
                    DisableText();
                    EditButton.Content = "Edit";
                    UpdateButton.IsEnabled = false;
                    AddButton.IsEnabled = true;
                    DeleteButton.IsEnabled = true;
                    PrevButton.IsEnabled = true;
                    NextButton.IsEnabled = true;
                    BrowseButton.IsEnabled = false;
                    RemoveButton.IsEnabled = false;
                    DownloadButton.IsEnabled = true;
                    //SearchComboBox.Items.Clear();
                    //FillComboBox();
                    PrintButton.IsEnabled = true;
                    ExportDataButton.IsEnabled = true;
                    ImportDataButton.IsEnabled = true;
                    TemplateButton.IsEnabled = true;
                    MessageBox.Show("Updated Sucessfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if (!DojTextBox.IsMaskCompleted || !DobTextBox.IsMaskCompleted)
                {
                    MessageBox.Show("Error\n" + "DOB or DOJ should be in 00-00-000 format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Error\n" + "Eid, Name, Father or Husband Name, Relation, Gender, DOJ and DOB can't be empty" + "for empty date use 00-00-0000", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?\n" + "Deleting Employee record also delete respective employee salary data also.", "Delete Confirmation! Read First", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    sqliteCon.Open();
                    string UpdateQuery = "select Eid,Name,FatherOrHusbandName,Relation,UAN,[ESIC no],Mobile,Aadhaar,Gender,Email,DOJ,DOL,DOB,IFSC,Account,Role,Address from employeeinfo ";
                    string Query = "delete from employeeinfo where Eid='" + this.EidTextBox.Text + "'";
                    SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                    SqlCommand updateCommand = new SqlCommand(UpdateQuery, sqliteCon);
                    createCommand.ExecuteNonQuery();

                    SqlDataAdapter dataAdp = new SqlDataAdapter(updateCommand);
                    DataTable dt = new DataTable("employeeinfo");
                    dataAdp.Fill(dt);
                    TableDataGrid.ItemsSource = dt.DefaultView;
                    dataAdp.Update(dt);

                    sqliteCon.Close();
                    // SearchComboBox.Items.Clear();
                    //FillComboBox();
                    MessageBox.Show("Data Deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            /* DataRowView rowSelected = TableDataGrid.SelectedItem as DataRowView;
             if (rowSelected == null)
                 return;
             int idx = GetDataRowViewIndex(rowSelected);
             if (idx > 0)
                 TableDataGrid.SelectedItem = rowSelected.DataView[idx - 1];
                 */
            if (TableDataGrid.SelectedIndex > 0)
                TableDataGrid.SelectedIndex--;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableDataGrid.SelectedIndex < TableDataGrid.Items.Count - 1)
                TableDataGrid.SelectedIndex++;
            /*
            DataRowView rowSelected = TableDataGrid.SelectedItem as DataRowView;
            if (rowSelected == null)
                return;
            int idx = GetDataRowViewIndex(rowSelected);
            if (idx < rowSelected.DataView.Count - 1)
                TableDataGrid.SelectedItem = rowSelected.DataView[idx + 1];
                */
        }

        /* int GetDataRowViewIndex(DataRowView row)
           {
               for (int i = 0; i < row.DataView.Count; i++)
                   if (row.DataView[i] == row)
                       return i;
               return -1;
           }
           */

        /*   void FillComboBox()
           {
               SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
               try
               {
                   sqliteCon.Open();
                   string Query = "select * from employeeinfo ";
                   SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
                   //createCommand.ExecuteNonQuery();

                   SQLiteDataReader dr = createCommand.ExecuteReader();
                   while (dr.Read())
                   {
                       string name = dr.GetString(1);
                       SearchComboBox.Items.Add(name);
                   }
                   sqliteCon.Close();
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
           }
           */
        private void TableDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            DataGrid gd = (DataGrid)sender;
            if (gd.SelectedItem is DataRowView rowSelected)
            {
                string Query = "select * from employeeinfo where Eid='" + rowSelected["Eid"].ToString() + "'";
                SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                try
                {
                    sqliteCon.Open();
                    SqlDataReader dr = createCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["Image"] != DBNull.Value)
                        {
                            byte[] img = (byte[])(dr["Image"]);
                            if (img == null)
                            {
                                EmpPictureBox.Image = null;
                            }
                            else
                            {
                                MemoryStream mstream = new MemoryStream(img);
                                EmpPictureBox.Image = System.Drawing.Image.FromStream(mstream);
                            }
                        }
                        else
                        {
                            EmpPictureBox.ImageLocation = @"Assets\noimage.jpg";
                            //EmpPictureBox.Image = null;
                        }

                    }
                    EidTextBox.Text = rowSelected["Eid"].ToString();
                    NameTextBox.Text = rowSelected["Name"].ToString();
                    FHTextBox.Text = rowSelected["FatherOrHusbandName"].ToString();
                    RelationComboBox.Text = rowSelected["Relation"].ToString();
                    UanTextBox.Text = rowSelected["UAN"].ToString();
                    EsicTextBox.Text = rowSelected["ESIC no"].ToString();
                    MobileTextBox.Text = rowSelected["Mobile"].ToString();
                    AadhaarTextBox.Text = rowSelected["Aadhaar"].ToString();
                    GenderComboBox.Text = rowSelected["Gender"].ToString();
                    EmailTextBox.Text = rowSelected["Email"].ToString();
                    DojTextBox.Text = rowSelected["DOJ"].ToString();
                    DolTextBox.Text = rowSelected["DOL"].ToString();
                    DobTextBox.Text = rowSelected["DOB"].ToString(); ;
                    IFSCTextBox.Text = rowSelected["IFSC"].ToString();
                    AcTextBox.Text = rowSelected["Account"].ToString();
                    RoleTextBox.Text = rowSelected["Role"].ToString();
                    AddressTextBox.Text = rowSelected["Address"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void DisableText()
        {
            EidTextBox.IsEnabled = false;
            NameTextBox.IsEnabled = false;
            FHTextBox.IsEnabled = false;
            UanTextBox.IsEnabled = false;
            EsicTextBox.IsEnabled = false;
            MobileTextBox.IsEnabled = false;
            AadhaarTextBox.IsEnabled = false;
            GenderComboBox.IsEnabled = false;
            EmailTextBox.IsEnabled = false;
            DojTextBox.IsEnabled = false;
            DolTextBox.IsEnabled = false;
            DobTextBox.IsEnabled = false;
            RoleTextBox.IsEnabled = false;
            RelationComboBox.IsEnabled = false;
            AddressTextBox.IsEnabled = false;
            IFSCTextBox.IsEnabled = false;
            AcTextBox.IsEnabled = false;
        }

        public void EnableText()
        {
            EidTextBox.IsEnabled = true;
            NameTextBox.IsEnabled = true;
            FHTextBox.IsEnabled = true;
            UanTextBox.IsEnabled = true;
            EsicTextBox.IsEnabled = true;
            MobileTextBox.IsEnabled = true;
            AadhaarTextBox.IsEnabled = true;
            GenderComboBox.IsEnabled = true;
            EmailTextBox.IsEnabled = true;
            DojTextBox.IsEnabled = true;
            DolTextBox.IsEnabled = true;
            DobTextBox.IsEnabled = true;
            RelationComboBox.IsEnabled = true;
            RoleTextBox.IsEnabled = true;
            AddressTextBox.IsEnabled = true;
            IFSCTextBox.IsEnabled = true;
            AcTextBox.IsEnabled = true;
        }
        /*
        private void GenderTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string typedString = GenderTextBox.Text;
            List<string> autoList = new List<string>();
            autoList.Clear();

            foreach (string item in genderList)
            {
                if (!string.IsNullOrEmpty(GenderTextBox.Text))
                {
                    if (item.StartsWith(typedString))
                    {
                        autoList.Add(item);
                    }
                }
            }


            if (autoList.Count > 0 && GenderTextBox.IsEnabled)
            {
                GenderListBox.ItemsSource = autoList;
                GenderListBox.Visibility = Visibility.Visible;
            }
            else if (GenderTextBox.Text.Equals(""))
            {
                GenderListBox.Visibility = Visibility.Collapsed;
                GenderListBox.ItemsSource = null;
            }
            else
            {
                GenderListBox.Visibility = Visibility.Collapsed;
                GenderListBox.ItemsSource = null;
            }

        }

        private void GenderListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (GenderListBox.ItemsSource != null)
            {
                GenderListBox.Visibility = Visibility.Collapsed;
                GenderTextBox.TextChanged -= new TextChangedEventHandler(GenderTextBox_TextChanged);
                if (GenderListBox.SelectedIndex != -1)
                {
                    GenderTextBox.Text = GenderListBox.SelectedItem.ToString();
                }
                GenderTextBox.TextChanged += new TextChangedEventHandler(GenderTextBox_TextChanged);
            }
        }
        */
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            //Print(TableDataGrid);
            PrintSelectionWindows WPFPringDg = new PrintSelectionWindows();
            WPFPringDg.Owner = Application.Current.MainWindow;
            WPFPringDg.ShowDialog();
        } 

        private void ExportDataButton_Click(object sender, RoutedEventArgs e)
        {
            ExportDialog expForm = new ExportDialog();
            {
                expForm.ShowDialog();
            }
        }                       
        
        private void ImportDataButton_Click(object sender, RoutedEventArgs e)
        {
            ImportFromExcel impForm = new ImportFromExcel();
            {
                impForm.ShowDialog();
                //Directory.SetCurrentDirectory(@"SQLiteImporter");
                //Process.Start("DB Browser for SQLite.exe");
                /*Process[] pname = Process.GetProcessesByName("DB Browser for SQLite");
                if (pname.Length == 0)
                    MessageBox.Show("nothing");
                else
                    MessageBox.Show("run");
                    */
                /*
                System.Windows.Forms.OpenFileDialog opnfileDiag = new System.Windows.Forms.OpenFileDialog();
                if (opnfileDiag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //PathTextBox.Text = opnfileDiag.FileName;

                    string PathConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + opnfileDiag.FileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    OleDbConnection dbCon = new OleDbConnection(PathConn);

                    OleDbDataAdapter dtAdp = new OleDbDataAdapter("Select * from [" + "Sheet1" + "$]", dbCon);
                    DataTable dt = new DataTable();
                    dtAdp.Fill(dt);

                    TableDataGrid.ItemsSource = dt.DefaultView;
                    dg.DataSource = dt.DefaultView;

                    */
                /* using(SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString))
                 {
                     using (SQLiteCommand comm = new SQLiteCommand())
                     {
                         comm.Connection = sqliteCon;
                         sqliteCon.Open();
                         for(int i=0; i< TableDataGrid.Items.Count;i++)
                         {
                             //insert into employeeinfo (Eid,Name,FatherOrHusbandName,UAN,ESIC,Mobile,Aadhaar,Gender,Email,DOJ,DOL,DOB,Wage,Role,Address) values('" + this.EidTextBox.Text + "','" + this.NameTextBox.Text + "','" + this.FHTextBox.Text + "','" + this.UanTextBox.Text + "','" + this.EsicTextBox.Text + "','" + this.MobileTextBox.Text + "','" + this.AadhaarTextBox.Text + "','" + this.GenderTextBox.Text + "','" + this.EmailTextBox.Text + "','" + this.DojTextBox.Text + "','" + this.DolTextBox.Text + "','" + this.DobTextBox.Text + "','" + this.WageTextBox.Text + "','" + this.RoleTextBox.Text + "','" + this.AddressTextBox.Text + "')";
                             string Query = "Insert into employeeinfo values ("
                                 + TableDataGrid.Items;
                         }
                     }
                 }                    


           }
               catch (Exception ex)
           {
               MessageBox.Show("Error\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
           }*/
            }
        }


        /*  void ReadData()
          {
              DataSet ds = new DataSet();
              SQLiteConnection dbCon = new SQLiteConnection(dbConnectionString);
              SQLiteDataAdapter dAdapter = new SQLiteDataAdapter("select * from employeeinfo", dbCon);
              dAdapter.Fill(ds, "[employeeinfo]");
              ds.AcceptChanges();
              //set the table as the datasource for the grid in order to show that data in the grid
              this.TableDataGrid.ItemsSource = ds.DefaultViewManager;
          }// end function

          void SaveData()
          {
              DataSet ds = new DataSet();
              SQLiteConnection dbCon = new SQLiteConnection(dbConnectionString);
              SQLiteDataAdapter dAdapter = new SQLiteDataAdapter("select * from employeeinfo", dbCon);
              DataSet changes = ds.GetChanges();
              if (changes != null)
              {
                  //Data has changes. 
                  //use update method in the adapter. it should update your datasource
                  int updatedRows = dAdapter.Update(changes);
                  ds.AcceptChanges();
              }
          }// end function
          */
        /*System.Windows.Forms.OpenFileDialog ope = new System.Windows.Forms.OpenFileDialog();
        ope.Filter = "Excel Files|*.xls;*xlsx;.xlsm*";
        //if (ope.ShowDialog() == DialogResult.Cancel) return;
        FileStream stream = new FileStream(ope.FileName, FileMode.Open);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();

        System.Data.Linq.DataContext conn = new System.Data.Linq.DataContext("");
        foreach(DataTable dt in result.Tables)
        {
            foreach(DataRow dr in dt.Rows)
            {
                DataGrid addtable = new DataGrid();

            }
        }

        */

        /* private void SearchComboBox_DropDownClosed(object sender, EventArgs e)
       { 
       SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
       try
       {
           sqliteCon.Open();
           string Query = "select * from employeeinfo where Name='" + SearchComboBox.Text + "'";
           SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
           //createCommand.ExecuteNonQuery();

           SQLiteDataReader dr = createCommand.ExecuteReader();
           while (dr.Read())
           {
               string sEid = dr.GetInt32(0).ToString();
               string sName = dr.GetString(1);
               string sFatherOrHusbandName = dr.GetString(2);
               string sUAN = dr.GetInt32(3).ToString();
               string sESIC = dr.GetInt32(4).ToString();
               string sMobile = dr.GetInt32(5).ToString();
               string sAadhaar = dr.GetInt32(6).ToString();
               string sGender = dr.GetString(7);
               string sEmail = dr.GetString(8);
               string sDOJ = dr.GetInt32(9).ToString();
               string sDOL = dr.GetInt32(10).ToString();
               string sDOB = dr.GetInt32(11).ToString();
               string sWage = dr.GetInt32(12).ToString();
               string sRole = dr.GetString(13);
               string sAddress = dr.GetString(14);

               EidTextBox.Text = sEid;
               NameTextBox.Text = sName;
               FHTextBox.Text = sFatherOrHusbandName;
               UanTextBox.Text = sUAN;
               EsicTextBox.Text = sESIC;
               MobileTextBox.Text = sMobile;
               AadhaarTextBox.Text = sAadhaar;
               GenderTextBox.Text = sGender;
               EmailTextBox.Text = sEmail;
               DojTextBox.Text = sDOJ;
               DolTextBox.Text = sDOL;
               DobTextBox.Text = sDOB;
               WageTextBox.Text = sWage;
               RoleTextBox.Text = sRole;
               AddressTextBox.Text = sAddress;
           }
           sqliteCon.Close();
       }
       catch (Exception ex)
       {
           MessageBox.Show(ex.Message);
       }
   }
   */

        private void TempleteButton_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Assets\Template.xlsx";
            if (File.Exists(path))
            {
                Process.Start(new ProcessStartInfo(path));
            }
            else
            {
                MessageBox.Show("File Missing or Corrupted" + path);
            }
        }

        private void TemplateHelpButton_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Assets\HelpImport.pdf";
            if (File.Exists(path))
            {
                Process.Start(new ProcessStartInfo(path));
            }
            else
            {
                MessageBox.Show("File Missing or Corrupted" + path);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchData(SearchTextBox.Text);
        }

        public void SearchData(String valueToFind)
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                string searchQuery = "select Eid,Name,FatherOrHusbandName,Relation,UAN,[ESIC no],Mobile,Aadhaar,Gender,Email,DOJ,DOL,DOB,IFSC,Account,Role,Address from employeeinfo where Name like '%" + valueToFind + "%'";
                SqlDataAdapter dataAdp = new SqlDataAdapter(searchQuery, sqliteCon);
                DataTable dt = new DataTable("employeeinfo");
                dataAdp.Fill(dt);
                TableDataGrid.ItemsSource = dt.DefaultView;
                sqliteCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EmpImage_MouseDown(object sender, MouseButtonEventArgs e)
        {/*
            try
            {
                System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
                dlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*png)|*.png";
                dlg.Title = "Select Employee Image";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (ValidFile(dlg.FileName, 312000, 560, 720))
                    {
                        string imgLocation = dlg.FileName.ToString();
                        EmpImage.Source = new BitmapImage(new Uri(imgLocation));
                    }
                    else
                    {
                        MessageBox.Show("Maximum Image Size 300KB and Height 420 & Width 320 Maximum", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            */
        }        

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            CameraBorder.Visibility = Visibility.Visible;
            /*
            try
            {
                System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
                dlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*png)|*.png";
                dlg.Title = "Select Employee Image";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (ValidFile(dlg.FileName, 312000, 560, 720 ))
                    {
                        string imgLocation = dlg.FileName.ToString();
                        EmpImage.Source = new BitmapImage(new Uri(imgLocation));
                    }
                    else
                    {
                        MessageBox.Show("Maximum Image Size 300KB and Height 420 & Width 320 Maximum", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }                    
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            */
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            EmpPictureBox.ImageLocation = @"Assets\noimage.jpg";
            //EmpPictureBox.Image = null;
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.SaveFileDialog sfdlg = new System.Windows.Forms.SaveFileDialog())
            {
                sfdlg.Title = "Save Dialog";
                sfdlg.Filter = "Jpg Image (*.jpg)|*.jpg|GIF Image (*.gif)|*.gif|PNG Image (*png)|*.png";
                if (sfdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {/*
                    using (var bmp = EmpPictureBox.Image)
                    {                        
                        bmp.Save(sfdlg.FileName);
                        MessageBox.Show("Saved Successfully.....");
                    }
                    */
                    
                    using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(EmpPictureBox.Width, EmpPictureBox.Height))
                    {
                        EmpPictureBox.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));
                        EmpPictureBox.Image = new System.Drawing.Bitmap(EmpPictureBox.Width, EmpPictureBox.Height);
                        bmp.Save(sfdlg.FileName);
                        MessageBox.Show("Saved Successfully.....");
                    }
                    
                }
            }
        }
        

        private void CameraCloseTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CameraBorder.Visibility = Visibility.Collapsed;
        }

        private void BrowseImageButton_Click(object sender, RoutedEventArgs e)
        {
            CameraBorder.Visibility = Visibility.Collapsed;
            try
            {
                System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog
                {
                    Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*png)|*.png",
                    Title = "Select Employee Image"
                };
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (ValidFile(dlg.FileName, 312000, 640, 480))
                    {
                        string imgLocation = dlg.FileName.ToString();
                        ImagePathTextBlock.Text = imgLocation;
                        ResizerEmpPictureBox.Image = new System.Drawing.Bitmap(dlg.OpenFile());

                        using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ResizerEmpPictureBox.Width, ResizerEmpPictureBox.Height))
                        {
                            ResizerEmpPictureBox.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));
                            ResizerEmpPictureBox.Image = new System.Drawing.Bitmap(ResizerEmpPictureBox.Width, ResizerEmpPictureBox.Height);
                            bmp.Save(@"App_Data\Temp\New.jpg", ImageFormat.Jpeg);
                        }

                        foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                        {
                            EmpPictureBox.ImageLocation = @"App_Data\Temp\New.jpg";
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("Maximum Image Size 300KB and Height 420 & Width 320 Maximum", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidFile(string filename, long limitInBytes, int limitWidth, int limitHeight)
        {
            var fileSizeInBytes = new FileInfo(filename).Length;
            if (fileSizeInBytes > limitInBytes) return false;

            using (var img = new System.Drawing.Bitmap(filename))
            {
                if (img.Width > limitWidth || img.Height > limitHeight) return false;
            }

            return true;
        }

        private void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            CameraBorder.Visibility = Visibility.Collapsed;
            CameraWindow camForm = new CameraWindow();
            {
                camForm.ShowDialog();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //this.Close();
            e.Cancel = true;            
            MainWindow win1 = new MainWindow();
            win1.Show();
            this.Hide();
        }              

        private void RefreshGrid()
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                string Query = "select Eid,Name,FatherOrHusbandName,Relation,UAN,[ESIC no],Mobile,Aadhaar,Gender,Email,DOJ,DOL,DOB,IFSC,Account,Role,Address from employeeinfo";
                SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                DataTable dt = new DataTable("employeeinfo");
                dataAdp.Fill(dt);
                TableDataGrid.ItemsSource = dt.DefaultView;
                //dgv.DataSource = dt;
                dataAdp.Update(dt);

                // Define a DataGrid TableStyle and add it to DataGrid
                //System.Windows.Forms.DataGridTableStyle ts = new System.Windows.Forms.DataGridTableStyle();
                //ts.MappingName = dt.TableName;
                // dg.TableStyles.Add(ts);
                // foreach (System.Windows.Forms.DataGridTextBoxColumn txtcol in ts.GridColumnStyles)
                // {
                //     txtcol.NullText = "";
                //      txtcol.ReadOnly = true;
                // }

                sqliteCon.Close();
                TableDataGrid.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /* public DataSet ConnectCSV(string filetable)
         {
             DataSet ds = new DataSet();
             try
             {
                 //You can get connected to driver either by using
                 //DSN or connection string. Create a connection string
                 //as below, if you want to use DSN less connection.
                 //The DBQ attribute sets the path of directory which 
                 //contains CSV files


                 string sql_select;
                 SQLiteConnection conn;

                 //Create connection to CSV file
                 conn = new SQLiteConnection(dbConnectionString.Trim());

                 // For creating a connection using DSN, use following line
                 //conn = new System.Data.Odbc.OdbcConnection(DSN="MyDSN");

                 //Open the connection 
                 conn.Open();
                 //Fetch records from CSV
                 sql_select = "select * from [" + filetable + "]";

                 SQLiteDataAdapter dataAdp = new SQLiteDataAdapter(sql_select, conn);
                 //Fill dataset with the records from CSV file
                 dataAdp.Fill(ds, "Stocks");

                 //Set the datagrid properties

                 TableDataGrid.ItemsSource = ds.DefaultViewManager;
                 TableDataGrid.DataContext = "Stocks";
                 //Close Connection to CSV file
                 conn.Close();
             }
             catch (Exception e) //Error
             {
                 MessageBox.Show(e.Message);
             }
             return ds;
         }*/
        #endregion

        //Tab2
        #region Tab2
        
        private void Tab2GoButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);

            if(IsAdd == true && Tab2DatePicker.SelectedDate != null && EidSelectTextBox.Text != "")
            {
                Tab2Query = "select * from employeeinfo where Eid='" + EidSelectTextBox.Text + "' and convert(varchar, DOL) = '00-00-0000'";
                //"select * from employeeinfo where Eid='" + EidSelectTextBox.Text + "' if not exists select * from salary where Eid='" + EidSelectTextBox.Text + "'"; //"IF (NOT EXISTS(SELECT * FROM SALARY WHERE Eid = '" + EidSelectTextBox.Text + "') ELSE(SELECT * FROM EMPLOYEEINFO WHERE Eid = '" + EidSelectTextBox.Text + "')"; // "select * from salary where Eid='" + EidSelectTextBox.Text + "' if not exists select * from salary where Eid='" + EidSelectTextBox.Text + "'" +
                //"SELECT CASE WHEN EXISTS(SELECT * FROM salary WHERE Eid = '" + EidSelectTextBox.Text + "')  THEN(SELECT * FROM employeeinfo WHERE Eid = '" + EidSelectTextBox.Text + "') END";
                SqlCommand createCommand = new SqlCommand(Tab2Query, sqliteCon);
                try
                {
                    sqliteCon.Open();
                    using (SqlDataReader dr = createCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Tab2NameTextBox.Text = dr["Name"].ToString();
                            Tab2FHNameTextBox.Text = dr["FatherOrHusbandName"].ToString();
                            Tab2RelationTextBox.Text = dr["Relation"].ToString();                            
                        }
                        if (/*dr.Read() == true*/Tab2NameTextBox.Text == "")
                        {
                            Tab2TaxTextBox.Text = Tab4EPFTextBox.Text;
                            MessageBox.Show("No Data Found");
                            Tab2CancelButton_Click(sender, e);                            
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Employee leave or Data not found. " + ex.Message);
                }
            }
            else if(IsAdd == false && Tab2DatePicker.SelectedDate != null && EidSelectTextBox.Text != "")
            {
                Tab2Query = "select * from salary where Eid= '" + EidSelectTextBox.Text + "' and Date='" + Tab2DatePicker.SelectedDate + "'";
                SqlCommand createCommand = new SqlCommand(Tab2Query, sqliteCon);
                try
                {
                    sqliteCon.Open();
                    using (SqlDataReader dr = createCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Tab2NameTextBox.Text = dr["Name"].ToString();
                            Tab2FHNameTextBox.Text = dr["FatherOrHusbandName"].ToString();
                            Tab2RelationTextBox.Text = dr["Relation"].ToString();
                            Tab2BasicWageTextBox.Text = dr["Basic"].ToString();
                            Tab2DATextBox.Text = dr["Dearness Allowance"].ToString();
                            Tab2ConTextBox.Text = dr["Conveyance Allowance"].ToString();
                            Tab2MediTextBox.Text = dr["Medical Allowance"].ToString();
                            Tab2HRATextBox.Text = dr["HRA"].ToString();
                            Tab2FoodTextBox.Text = dr["Food Allowance"].ToString();
                            Tab2SpecTextBox.Text = dr["Special Allowance"].ToString();
                            Tab2OthTextBox.Text = dr["Other Amount"].ToString();
                            Tab2TaxTextBox.Text = dr["TDS/Tax"].ToString();
                            Tab2EPFTextBox.Text = dr["EPF"].ToString();
                            Tab2ESICTextBox.Text = dr["ESIC"].ToString();
                            Tab2AdvTextBox.Text = dr["Loan/Advance"].ToString();
                            Tab2OthDedTextBox.Text = dr["Other Deduction"].ToString();
                            Tab2WorkingTextBox.Text = dr["DayW"].ToString();
                            if (dr["Payment Mode"].ToString() == "Online (Net Banking)")
                            {
                                Tab2ModeComboBox.SelectedIndex = 0;
                            }
                            else if (dr["Payment Mode"].ToString() == "Cheque")
                            {
                                Tab2ModeComboBox.SelectedIndex = 1;
                            }
                            else if (dr["Payment Mode"].ToString() == "Cash")
                            {
                                Tab2ModeComboBox.SelectedIndex = 2;
                            }
                            Tab2ChequeTextBox.Text = dr["DD no"].ToString();
                        }
                        if (dr.Read() == true)
                        {
                            MessageBox.Show("No Data Found");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No Data For Given Date! " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Select Employee code and date first.", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }            
        } 

        private void Tab2AddButton_Click(object sender, RoutedEventArgs e)
        {
            IsAdd = true;
            Tab2TextEnable();
            Tab2CancelButton.Visibility = Visibility.Visible;
            Tab2SaveButton.Visibility = Visibility.Visible;
            Tab2EditViewButton.Visibility = Visibility.Collapsed;
            Tab2AddButton.Visibility = Visibility.Collapsed;
            Tab2DeleteButton.Visibility = Visibility.Collapsed;
            ResetText();
        }

        private void Tab2EditViewButton_Click(object sender, RoutedEventArgs e)
        {
            IsAdd = false;
            Tab2TextEnable();
            Tab2CancelButton.Visibility = Visibility.Visible;
            Tab2SaveButton.Visibility = Visibility.Visible;
            Tab2AddButton.Visibility = Visibility.Collapsed;
            Tab2EditViewButton.Visibility = Visibility.Collapsed;
            Tab2DeleteButton.Visibility = Visibility.Collapsed;
        }

        private void Tab2CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdd == true)
            {
                Tab2TextDisable();
                Tab2CancelButton.Visibility = Visibility.Collapsed;
                Tab2SaveButton.Visibility = Visibility.Collapsed;
                Tab2EditViewButton.Visibility = Visibility.Visible;
                Tab2AddButton.Visibility = Visibility.Visible;
                Tab2DeleteButton.Visibility = Visibility.Visible;
                IsAdd = false;
            }
            else
            {
                Tab2TextDisable();
                Tab2CancelButton.Visibility = Visibility.Collapsed;
                Tab2SaveButton.Visibility = Visibility.Collapsed;
                Tab2EditViewButton.Visibility = Visibility.Visible;
                Tab2AddButton.Visibility = Visibility.Visible;
                Tab2DeleteButton.Visibility = Visibility.Visible;
                IsAdd = false;
            }
        }

        private void Tab2SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdd == true)
            {
                if(string.IsNullOrWhiteSpace(Tab2WorkingTextBox.Text) || Tab2WorkingTextBox.Text != "0")
                {
                    SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
                    try
                    {
                        sqliteCon.Open();
                        string Query = "if not exists(select Eid from salary where Eid= '" + EidSelectTextBox.Text + "' and Date='" + Tab2DatePicker.SelectedDate + "') insert into salary (Eid,Name,FatherOrHusbandName,Relation,DayW,Basic,[Dearness Allowance],[Conveyance Allowance],[Medical Allowance],HRA,[Food Allowance],[Special Allowance],[Other Amount],[TDS/Tax],EPF,ESIC,[Loan/Advance],[Other Deduction],[Net Wages],Date,[Payment Mode],[DD no],Month,Year) values('" + this.EidSelectTextBox.Text + "','" + this.Tab2NameTextBox.Text + "','" + this.Tab2FHNameTextBox.Text + "','" + this.Tab2RelationTextBox.Text + "','" + this.Tab2WorkingTextBox.Text + "','" + this.Tab2BasicWageTextBox.Text + "','" + this.Tab2DATextBox.Text + "','" + this.Tab2ConTextBox.Text + "','" + this.Tab2MediTextBox.Text + "','" + this.Tab2HRATextBox.Text + "','" + this.Tab2FoodTextBox.Text + "','" + this.Tab2SpecTextBox.Text + "','" + this.Tab2OthTextBox.Text + "','" + this.Tab2TaxTextBox.Text + "','" + this.Tab2EPFTextBox.Text + "','" + this.Tab2ESICTextBox.Text + "','" + this.Tab2AdvTextBox.Text + "','" + this.Tab2OthDedTextBox.Text + "','" + this.Tab2NetTextBox.Text + "','" + this.Tab2DatePicker.SelectedDate + "','" + this.Tab2ModeComboBox.Text + "','" + this.Tab2ChequeTextBox.Text + "','" + this.Tab2MonthComboBox.Text + "','" + this.Tab2YearComboBox.Text + "') else RAISERROR('Data already exist, try editing data.', 16, 1)";
                        SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                        createCommand.ExecuteNonQuery();
                        sqliteCon.Close();
                        MessageBox.Show("Data Saved Sucessfully","Information",MessageBoxButton.OK,MessageBoxImage.Information);// Message error required.
                        Tab2TextDisable();
                        Tab2CancelButton.Visibility = Visibility.Collapsed;
                        Tab2SaveButton.Visibility = Visibility.Collapsed;
                        Tab2EditViewButton.Visibility = Visibility.Visible;
                        Tab2AddButton.Visibility = Visibility.Visible;
                        Tab2DeleteButton.Visibility = Visibility.Visible;
                        IsAdd = false;
                    }
                    catch (Exception ex)
                    {
                        sqliteCon.Close();
                        MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Stop);
                        Tab2TextDisable();
                        Tab2CancelButton.Visibility = Visibility.Collapsed;
                        Tab2SaveButton.Visibility = Visibility.Collapsed;
                        Tab2EditViewButton.Visibility = Visibility.Visible;
                        Tab2AddButton.Visibility = Visibility.Visible;
                        Tab2DeleteButton.Visibility = Visibility.Visible;
                        IsAdd = false;
                    }
                }
                else
                {
                    MessageBox.Show("No. Of Days Can't be Empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
                    //Tab2WorkingTextBox.Focusable = true;
                    Keyboard.Focus(Tab2WorkingTextBox);
                }
            }
            else if(IsAdd == false)
            {
                if (string.IsNullOrWhiteSpace(Tab2WorkingTextBox.Text) || Tab2WorkingTextBox.Text != "0")
                {
                    SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
                    try
                    {
                        sqliteCon.Open();
                        string Query = "update salary set Basic='" + this.Tab2BasicWageTextBox.Text + "',[Dearness Allowance]='" + this.Tab2DATextBox.Text + "',[Conveyance Allowance]='" + this.Tab2ConTextBox.Text + "',[Medical Allowance]='" + this.Tab2MediTextBox.Text + "',HRA='" + this.Tab2HRATextBox.Text + "',[Food Allowance]='" + this.Tab2FoodTextBox.Text + "',[Special Allowance]='" + this.Tab2SpecTextBox.Text + "',[Other Amount]='" + this.Tab2OthTextBox.Text + "',[TDS/Tax]='" + this.Tab2TaxTextBox.Text + "',EPF='" + this.Tab2EPFTextBox.Text + "',ESIC='" + this.Tab2ESICTextBox.Text + "',[Loan/Advance]='" + this.Tab2AdvTextBox.Text + "',[Other Deduction]='" + this.Tab2OthDedTextBox.Text + "',[Net Wages]='" + this.Tab2NetTextBox.Text + "',DayW='" + this.Tab2WorkingTextBox.Text + "',[Payment Mode]='" + this.Tab2ModeComboBox.Text + "',[DD no]='" + this.Tab2ChequeTextBox.Text + "',Month='" + this.Tab2MonthComboBox.Text + "',Year='" + this.Tab2YearComboBox.Text + "' where Eid='" + this.EidSelectTextBox.Text + "' and Date='" + Tab2DatePicker.SelectedDate + "'";
                        SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                        createCommand.ExecuteNonQuery();
                        sqliteCon.Close();
                        MessageBox.Show("Data Updated Sucessfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);// Message error required.
                        Tab2TextDisable();
                        Tab2CancelButton.Visibility = Visibility.Collapsed;
                        Tab2SaveButton.Visibility = Visibility.Collapsed;
                        Tab2EditViewButton.Visibility = Visibility.Visible;
                        Tab2AddButton.Visibility = Visibility.Visible;
                        Tab2DeleteButton.Visibility = Visibility.Visible;
                        IsAdd = false;
                    }
                    catch (Exception ex)
                    {
                        sqliteCon.Close();
                        MessageBox.Show(ex.Message);
                        Tab2TextDisable();
                        Tab2CancelButton.Visibility = Visibility.Collapsed;
                        Tab2SaveButton.Visibility = Visibility.Collapsed;
                        Tab2EditViewButton.Visibility = Visibility.Visible;
                        Tab2AddButton.Visibility = Visibility.Visible;
                        Tab2DeleteButton.Visibility = Visibility.Visible;
                        IsAdd = false;
                    }
                }
                else
                {
                    MessageBox.Show("No. Of Days Can't be Empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
                    Keyboard.Focus(Tab2WorkingTextBox);
                }
            }
        }

        private void Tab2DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?\n" + "Once Delete can't be undo.", "Delete Confirmation!", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    sqliteCon.Open();                    
                    string Query = "delete from salary where Eid='" + this.EidSelectTextBox.Text + "' and Date='" + Tab2DatePicker.SelectedDate + "'";
                    SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                    createCommand.ExecuteNonQuery();
                    sqliteCon.Close();
                    MessageBox.Show("Data Deleted");
                    ResetText();
                    Tab2NameTextBox.Text = "";
                    Tab2FHNameTextBox.Text = "";
                    Tab2RelationTextBox.Text = "";
                    Tab2MonthComboBox.SelectedIndex = -1;
                    Tab2YearComboBox.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Tab2MonthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tab2YearComboBox.IsEnabled = true;
        }

        private void Tab2YearComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (Tab2YearComboBox.SelectedIndex > -1 && Tab2MonthComboBox.SelectedIndex > -1)
            {
                mySelectedDate = "01-" + Tab2MonthComboBox.Text + "-" + Tab2YearComboBox.Text + "";
                x.Content = mySelectedDate;
                Tab2DatePicker.SelectedDate = DateTime.Parse(mySelectedDate);
            }
        }

        private void Tab2MonthComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (Tab2YearComboBox.SelectedIndex > -1)
            {
                mySelectedDate = "01-" + Tab2MonthComboBox.Text + "-" + Tab2YearComboBox.Text + "";
                x.Content = mySelectedDate;
                Tab2DatePicker.SelectedDate = DateTime.Parse(mySelectedDate);
            }
        }

        private void EPFInsideButton_Click(object sender, RoutedEventArgs e)
        {
            decimal EPF;
            decimal epPercent;
            decimal EPFAmount;
            var Basic = decimal.TryParse(Tab2BasicWageTextBox.Text, out EPF);
            var Rate = decimal.TryParse(Tab4EPFTextBox.Text, out epPercent);
            if (Basic && Rate)
            {
                EPFAmount = EPF * epPercent / 100;
                EPFAmount = Math.Round(EPFAmount);
                Tab2EPFTextBox.Text = EPFAmount.ToString();
            }
        }

        private void ESICInsideButton_Click(object sender, RoutedEventArgs e)
        {
            if(Tab2WorkingTextBox.Text != null || Tab2WorkingTextBox.Text != "0")
            {
                decimal ESIC;
                decimal esPercent;
                decimal esDays;
                decimal ESICAmount;
                decimal InsertedWage;
                var Basic = decimal.TryParse(Tab2BasicWageTextBox.Text, out ESIC);
                var Rate = decimal.TryParse(Tab4ESICTextBox.Text, out esPercent);
                var WDays = decimal.TryParse(Tab2WorkingTextBox.Text, out esDays);
                var IWage = decimal.TryParse(Tab4ESICMinWageTextBox.Text, out InsertedWage);
                var MinWage = ESIC / esDays;
                if (Basic && Rate && (MinWage > InsertedWage) && (ESIC <= 21000))
                {
                    ESICAmount = ESIC * esPercent / 100;
                    ESICAmount = Math.Round(ESICAmount);
                    Tab2ESICTextBox.Text = ESICAmount.ToString();
                }
                else
                {
                    Tab2ESICTextBox.Text = "0";
                }
            }            
        }

        private void Tab2ModeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (Tab2ModeComboBox.SelectedIndex == 0)
            {
                Tab2IFSCTextBox.Visibility = Visibility.Visible;
                Tab2AccountTextBox.Visibility = Visibility.Visible;
                Tab2ChequeTextBox.Visibility = Visibility.Collapsed;
                SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
                string AccountQuery = "select IFSC,Account from employeeinfo where Eid= '" + EidSelectTextBox.Text + "'";
                SqlCommand createCommand = new SqlCommand(Tab2Query, sqliteCon);
                try
                {
                    sqliteCon.Open();
                    using (SqlDataReader dr = createCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Tab2IFSCTextBox.Text = dr["IFSC"].ToString();
                            Tab2AccountTextBox.Text = dr["Account"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No IFSC and Account " + ex.Message);
                }
            }
            else if (Tab2ModeComboBox.SelectedIndex == 1)
            {
                Tab2IFSCTextBox.Visibility = Visibility.Collapsed;
                Tab2AccountTextBox.Visibility = Visibility.Collapsed;
                Tab2ChequeTextBox.Visibility = Visibility.Visible;
            }
            else if (Tab2ModeComboBox.SelectedIndex == 2)
            {
                Tab2IFSCTextBox.Visibility = Visibility.Collapsed;
                Tab2AccountTextBox.Visibility = Visibility.Collapsed;
                Tab2ChequeTextBox.Visibility = Visibility.Collapsed;
            }
            else if (Tab2ModeComboBox.SelectedIndex == -1)
            {
                Tab2IFSCTextBox.Visibility = Visibility.Collapsed;
                Tab2AccountTextBox.Visibility = Visibility.Collapsed;
                Tab2ChequeTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void Tab2ModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tab2ModeComboBox.SelectedIndex == 0)
            {
                Tab2IFSCTextBox.Visibility = Visibility.Visible;
                Tab2AccountTextBox.Visibility = Visibility.Visible;
                Tab2ChequeTextBox.Visibility = Visibility.Collapsed;
                SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
                Tab2Query += "select IFSC,Account from employeeinfo where Eid= '" + EidSelectTextBox.Text + "'";
                SqlCommand acCommand = new SqlCommand(Tab2Query, sqliteCon);
                try
                {
                    sqliteCon.Open();
                    using (SqlDataReader dr = acCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Tab2IFSCTextBox.Text = dr["IFSC"].ToString();
                            Tab2AccountTextBox.Text = dr["Account"].ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("No IFSC and Account " + ex.Message);
                }
            }
            else if (Tab2ModeComboBox.SelectedIndex == 1)
            {
                Tab2IFSCTextBox.Visibility = Visibility.Collapsed;
                Tab2AccountTextBox.Visibility = Visibility.Collapsed;
                Tab2ChequeTextBox.Visibility = Visibility.Visible;
            }
            else if (Tab2ModeComboBox.SelectedIndex == 2)
            {
                Tab2IFSCTextBox.Visibility = Visibility.Collapsed;
                Tab2AccountTextBox.Visibility = Visibility.Collapsed;
                Tab2ChequeTextBox.Visibility = Visibility.Collapsed;
            }
            else if (Tab2ModeComboBox.SelectedIndex == -1)
            {
                Tab2IFSCTextBox.Visibility = Visibility.Collapsed;
                Tab2AccountTextBox.Visibility = Visibility.Collapsed;
                Tab2ChequeTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void Tab2TextEnable()
        {
            Tab2BasicWageTextBox.IsEnabled = true;
            Tab2DATextBox.IsEnabled = true;
            Tab2ConTextBox.IsEnabled = true;
            Tab2MediTextBox.IsEnabled = true;
            Tab2HRATextBox.IsEnabled = true;
            Tab2FoodTextBox.IsEnabled = true;
            Tab2SpecTextBox.IsEnabled = true;
            Tab2OthTextBox.IsEnabled = true;
            Tab2TaxTextBox.IsEnabled = true;
            Tab2EPFTextBoxGrid.IsEnabled = true;
            Tab2ESICTextBoxGrid.IsEnabled = true;
            Tab2AdvTextBox.IsEnabled = true;
            Tab2OthDedTextBox.IsEnabled = true;
            Tab2WorkingTextBox.IsEnabled = true;
            Tab2ModeComboBox.IsEnabled = true;
            Tab2ChequeTextBox.IsEnabled = true;
        }

        private void Tab2TextDisable()
        {
            Tab2BasicWageTextBox.IsEnabled = false;
            Tab2DATextBox.IsEnabled = false;
            Tab2ConTextBox.IsEnabled = false;
            Tab2MediTextBox.IsEnabled = false;
            Tab2HRATextBox.IsEnabled = false;
            Tab2FoodTextBox.IsEnabled = false;
            Tab2SpecTextBox.IsEnabled = false;
            Tab2OthTextBox.IsEnabled = false;
            Tab2TaxTextBox.IsEnabled = false;
            Tab2EPFTextBoxGrid.IsEnabled = false;
            Tab2ESICTextBoxGrid.IsEnabled = false;
            Tab2AdvTextBox.IsEnabled = false;
            Tab2OthDedTextBox.IsEnabled = false;
            Tab2WorkingTextBox.IsEnabled = false;
            Tab2ModeComboBox.IsEnabled = false;
            Tab2ChequeTextBox.IsEnabled = false;
        }

        private void ResetText()
        {
            Tab2BasicWageTextBox.Text = "0";
            Tab2DATextBox.Text = "0";
            Tab2ConTextBox.Text = "0";
            Tab2MediTextBox.Text = "0";
            Tab2HRATextBox.Text = "0";
            Tab2FoodTextBox.Text = "0";
            Tab2SpecTextBox.Text = "0";
            Tab2OthTextBox.Text = "0";
            Tab2TaxTextBox.Text = "0";
            Tab2EPFTextBox.Text = "0";
            Tab2ESICTextBox.Text = "0";
            Tab2AdvTextBox.Text = "0";
            Tab2OthDedTextBox.Text = "0";
            Tab2WorkingTextBox.Text = "0";
            Tab2ModeComboBox.SelectedIndex = -1;
        }


        //Class
        private decimal bearnings;
        public decimal BEarnings
        {
            get { return bearnings; }
            set
            {
                bearnings = value;
                OnPropertyChanged("BEarnings");
            }
        }

        //Class
        private decimal daearnings;
        public decimal DAEarnings
        {
            get { return daearnings; }
            set
            {
                daearnings = value;
                OnPropertyChanged("DAEarnings");
            }
        }

        //Class
        private decimal caearnings;
        public decimal CAEarnings
        {
            get { return caearnings; }
            set
            {
                caearnings = value;
                OnPropertyChanged("CAEarnings");
            }
        }

        //Class
        private decimal maearnings;
        public decimal MAEarnings
        {
            get { return maearnings; }
            set
            {
                maearnings = value;
                OnPropertyChanged("MAEarnings");
            }
        }

        //Class
        private decimal hraearnings;
        public decimal HRAEarnings
        {
            get { return hraearnings; }
            set
            {
                hraearnings = value;
                OnPropertyChanged("HRAEarnings");
            }
        }

        //Class
        private decimal faearnings;
        public decimal FAEarnings
        {
            get { return faearnings; }
            set
            {
                faearnings = value;
                OnPropertyChanged("FAEarnings");
            }
        }

        //Class
        private decimal saearnings;
        public decimal SAEarnings
        {
            get { return saearnings; }
            set
            {
                saearnings = value;
                OnPropertyChanged("SAEarnings");
            }
        }

        //Class
        private decimal oearnings;
        public decimal OEarnings
        {
            get { return oearnings; }
            set
            {
                oearnings = value;
                OnPropertyChanged("OEarnings");
            }
        }

        //Class
        private decimal taxdeductions;
        public decimal TaxDeductions
        {
            get { return taxdeductions; }
            set
            {
                taxdeductions = value;
                OnPropertyChanged("TaxDeductions");
            }
        }

        //Class
        private decimal epfdeductions;
        public decimal EPFDeductions
        {
            get { return epfdeductions; }
            set
            {
                epfdeductions = value;
                OnPropertyChanged("EPFDeductions");
            }
        }

        //Class
        private decimal esicdeductions;
        public decimal ESICDeductions
        {
            get { return esicdeductions; }
            set
            {
                esicdeductions = value;
                OnPropertyChanged("ESICDeductions");
            }
        }

        //Class
        private decimal ladeductions;
        public decimal LADeductions
        {
            get { return ladeductions; }
            set
            {
                ladeductions = value;
                OnPropertyChanged("LADeductions");
            }
        }

        //Class
        private decimal odeductions;
        public decimal ODeductions
        {
            get { return odeductions; }
            set
            {
                odeductions = value;
                OnPropertyChanged("ODeductions");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string txt)
        {

            PropertyChangedEventHandler handle = PropertyChanged;
            if (handle != null)
            {
                handle(this, new PropertyChangedEventArgs(txt));
            }
        }


        #endregion

        //Tab3
        #region Tab3

        private void ReportTab_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                string Query1 = "select count(Eid) from employeeinfo ";
                string Query2 = "select count(Eid) from employeeinfo where convert(varchar, DOL) = '00-00-0000'";

                using (SqlCommand command1 = new SqlCommand(Query1, sqliteCon))
                {
                    object empCount = command1.ExecuteScalar();

                    if (empCount != null)
                    {
                        Tab3TotalEmpTextBox.Text = empCount.ToString();
                    }
                    else
                    {
                        Tab3TotalEmpTextBox.Text = "0";
                    }
                }
                using (SqlCommand command2 = new SqlCommand(Query2, sqliteCon))
                {
                    object emplCount = command2.ExecuteScalar();

                    if (emplCount != null)
                    {
                        Tab3LeaveEmpTextBox.Text = emplCount.ToString();
                    }
                    else
                    {
                        Tab3LeaveEmpTextBox.Text = "0";
                    }
                }
                sqliteCon.Close();
            }
            catch (Exception ex)
            {
                sqliteCon.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void Tab3EmpDetailsGoButton_Click(object sender, RoutedEventArgs e)
        {
            EidFetch = Tab3EidTextBox.Text;
            LoadReport = "EmployeeDetails";
            ReportViewerDialog rptForm = new ReportViewerDialog();
            {
                rptForm.Show();
            }
        }

        private void Tab3SalaryGoButton_Click(object sender, RoutedEventArgs e)
        {
            EidFetch = Tab3SalaryEidTextBox.Text;
            LoadReport = "PaySlip";
            SalaryDate = Tab3SalaryDatePicker.SelectedDate;
            ReportViewerDialog rptForm = new ReportViewerDialog();
            {
                rptForm.Show();
            }
        }

        private void Tab3MonthComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (Tab3YearComboBox.SelectedIndex > -1)
            {
                mySelectedDate = "01-" + Tab3MonthComboBox.Text + "-" + Tab3YearComboBox.Text + "";
                Tab3SalaryDatePicker.SelectedDate = DateTime.Parse(mySelectedDate);
            }
        }

        private void Tab3YearComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (Tab3YearComboBox.SelectedIndex > -1 && Tab3MonthComboBox.SelectedIndex > -1)
            {
                mySelectedDate = "01-" + Tab3MonthComboBox.Text + "-" + Tab3YearComboBox.Text + "";
                Tab3SalaryDatePicker.SelectedDate = DateTime.Parse(mySelectedDate);
            }
        }

        private void Tab3SSGoButton_Click(object sender, RoutedEventArgs e)
        {
            if (Tab3ASingleRadioButton.IsChecked == true && Tab3SSMonthComboBox.SelectedIndex > -1 && Tab3SSYearComboBox.SelectedIndex > -1 && Tab3EidSTextBox.Text != null)
            {
                EidFetch = Tab3EidSTextBox.Text;
                LoadReport = "SummarySinglePaySlip";
                SalaryDate = Tab3SSDatePicker.SelectedDate;
                ReportViewerDialog rptForm = new ReportViewerDialog();
                {
                    rptForm.Show();
                }
            }
            else if (Tab3ABulkRadioButton.IsChecked == true && Tab3SSMonthComboBox.SelectedIndex > -1 && Tab3SSYearComboBox.SelectedIndex > -1 && Tab3EidSRangeTextBox.Text != null && Tab3EidERangeTextBox.Text != null)
            {
                StartEidFetch = Tab3EidSRangeTextBox.Text;
                EndEidFetch = Tab3EidERangeTextBox.Text;
                LoadReport = "SummaryBulkPaySlip";
                SalaryDate = Tab3SSDatePicker.SelectedDate;
                ReportViewerDialog rptForm = new ReportViewerDialog();
                {
                    rptForm.Show();
                }
            }
            else if (Tab3ASingleRadioButton.IsChecked == true && (Tab3SSMonthComboBox.SelectedIndex == -1 || Tab3SSYearComboBox.SelectedIndex == -1 || string.IsNullOrWhiteSpace(Tab3EidSTextBox.Text)))
            {
                MessageBox.Show("Month, Year, and Eid Can't be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else if (Tab3ABulkRadioButton.IsChecked == true && (Tab3SSMonthComboBox.SelectedIndex == -1 || Tab3SSYearComboBox.SelectedIndex == -1 || string.IsNullOrWhiteSpace(Tab3EidSRangeTextBox.Text) || string.IsNullOrWhiteSpace(Tab3EidERangeTextBox.Text)))
            {
                MessageBox.Show("Month, Year, and Eid Range Can't be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void Tab3SSMonthComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (Tab3YearComboBox.SelectedIndex > -1)
            {
                mySelectedDate = "01-" + Tab3SSMonthComboBox.Text + "-" + Tab3SSYearComboBox.Text + "";
                Tab3SSDatePicker.SelectedDate = DateTime.Parse(mySelectedDate);
            }
        }

        private void Tab3SSYearComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (Tab3SSYearComboBox.SelectedIndex > -1 && Tab3SSMonthComboBox.SelectedIndex > -1)
            {
                mySelectedDate = "01-" + Tab3SSMonthComboBox.Text + "-" + Tab3SSYearComboBox.Text + "";
                Tab3SSDatePicker.SelectedDate = DateTime.Parse(mySelectedDate);
            }
        }

        private void Tab3MonthOrgComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (Tab3YearOrgComboBox.SelectedIndex > -1)
            {
                mySelectedDate = "01-" + Tab3MonthOrgComboBox.Text + "-" + Tab3YearOrgComboBox.Text + "";
                Tab3MonthOrgDatePicker.SelectedDate = DateTime.Parse(mySelectedDate);
            }
        }

        private void Tab3YearOrgComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (Tab3YearOrgComboBox.SelectedIndex > -1 && Tab3MonthOrgComboBox.SelectedIndex > -1)
            {
                mySelectedDate = "01-" + Tab3MonthOrgComboBox.Text + "-" + Tab3YearOrgComboBox.Text + "";
                Tab3MonthOrgDatePicker.SelectedDate = DateTime.Parse(mySelectedDate);
            }
        }

        private void Tab3MonthOrgGoButton_Click(object sender, RoutedEventArgs e)
        {
            LoadReport = "OrgMonthReport";
            OrgMonthDate = Tab3MonthOrgDatePicker.SelectedDate;
            ReportViewerDialog rptForm = new ReportViewerDialog();
            {
                rptForm.Show();
            }
        }

        private void Tab3YearOrgGoButton_Click(object sender, RoutedEventArgs e)
        {
            if (Tab3YearOnlyRadioButton.IsChecked == true && Tab3YYearOrgComboBox.SelectedIndex > -1)
            {
                LoadReport = "OrgYearAllReport";
                YearFetch = Tab3YYearOrgComboBox.Text;
                ReportViewerDialog rptForm = new ReportViewerDialog();
                {
                    rptForm.Show();
                }
            }
            else if (Tab3YearWithEidRadioButton.IsChecked == true && Tab3YYearOrgComboBox.SelectedIndex > -1 && Tab3YearEidTextBox.Text != null)
            {
                LoadReport = "OrgYearOnlyReport";
                YearFetch = Tab3YYearOrgComboBox.Text;
                EidFetch = Tab3YearEidTextBox.Text;
                ReportViewerDialog rptForm = new ReportViewerDialog();
                {
                    rptForm.Show();
                }
            }
        }

        #endregion

        // Tab4
        #region Tab4

        // Backup
        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {            
            var dir = @"C:\EMSBackup";  // folder location
            
            //bmpScreenShot.Save(filename, ImageFormat.Jpeg);

            if (!Directory.Exists(dir))  // if it doesn't exist, create
                Directory.CreateDirectory(dir);

            string filename = System.IO.Path.Combine(String.Format("\\Backup " + "{0}.bak", DateTime.Now.ToString("dd-MM-yyyy hh.mm.ss.tt")));

            try
            {
                Server dbServer = new Server(new ServerConnection(IP, "admin", "1011535"));
                Backup dbBackup = new Backup()
                {
                    Action = BackupActionType.Database,
                    Database = "emsdatabase"
                };
                dbBackup.Devices.AddDevice(dir + filename, DeviceType.File);
                dbBackup.Initialize = true;
                dbBackup.PercentComplete += DbBackup_PercentComplete;
                dbBackup.Complete += DbBackup_Complete;
                dbBackup.SqlBackupAsync(dbServer);
                //BackupStatusTextBlock.Text = dir + filename;
            }
            catch (Exception ex)
            {
                BackupStatusTextBlock.Text = ex.Message;
                BackupStatusTextBlock.Foreground = new SolidColorBrush(Colors.IndianRed);
            }
        }

        private void DbBackup_Complete(object sender, ServerMessageEventArgs e)
        {
            if (e.Error != null)
            {
                /*Dispatcher.BeginInvoke(new Action(() =>
                {
                    
                }));*/

                Dispatcher.BeginInvoke(new Action(delegate
                {
                    BackupStatusTextBlock.Text = @"Backup has been saved successfully in C:\EMSBackup";
                    BackupStatusTextBlock.Foreground = new SolidColorBrush(Colors.CornflowerBlue);
                }));
            }
            else
            {
                BackupStatusTextBlock.Text = @"Unable to Backup, retry after a system reboot.";
                BackupStatusTextBlock.Foreground = new SolidColorBrush(Colors.IndianRed);
            }
        }

        private void DbBackup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                BackupProgressBar.Value = e.Percent;
            }));
        }

        // Restore
        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if(IsClient == false)
            {
                System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog
                {
                    Filter = "Backup Files (*.bak)|*.bak",
                    Title = "Select backup file"
                };
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileLocation = dlg.FileName.ToString();
                    //ImagePathTextBlock.Text = imgLocation;
                    //ResizerEmpPictureBox.Image = new System.Drawing.Bitmap(dlg.OpenFile());
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure? You can not restore data again to old state, It is recommended to take backup first and store it in safe place", "Replace Confirmation! Read First", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Server dbServer = new Server(new ServerConnection(IP, "admin", "1011535"));
                            Restore dbRestore = new Restore()
                            {
                                Database = "emsdatabase",
                                Action = RestoreActionType.Database,
                                ReplaceDatabase = true,
                                NoRecovery = false
                            };
                            dbServer.KillAllProcesses(dbRestore.Database);                            
                            dbRestore.Devices.AddDevice(fileLocation, DeviceType.File);
                            dbRestore.PercentComplete += DbRestore_PercentComplete;
                            dbRestore.Complete += DbRestore_Complete;
                            dbRestore.SqlRestoreAsync(dbServer);
                            MessageBoxResult exitConfirmation = MessageBox.Show("It is recommended to logout from current session after data restore, Press OK to logout.", "SignOut Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            if (exitConfirmation == MessageBoxResult.OK)
                            {
                                MainWindow win1 = new MainWindow();
                                win1.Show();
                                this.Hide();
                            }
                        }
                        catch (Exception ex)
                        {
                            RestoreStatusTextBlock.Text = ex.Message;
                            RestoreStatusTextBlock.Foreground = new SolidColorBrush(Colors.IndianRed);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation Terminated");
                    }
                }
            }
            else
            {
                MessageBox.Show("Client user can take backup but can't restore database it is administrative task", "Unauthorised Task", MessageBoxButton.OK, MessageBoxImage.Stop);
            }            
        }

        private void DbRestore_Complete(object sender, ServerMessageEventArgs e)
        {
            if (e.Error != null)
            {
                /*Dispatcher.BeginInvoke(new Action(() =>
                {
                    
                }));*/

                Dispatcher.BeginInvoke(new Action(delegate
                {
                    RestoreStatusTextBlock.Text = "Restore Database Successful";
                    BackupStatusTextBlock.Foreground = new SolidColorBrush(Colors.CornflowerBlue);
                }));
            }
            else
            {
                BackupStatusTextBlock.Text = @"Unable to Backup, retry after a system reboot.";
                BackupStatusTextBlock.Foreground = new SolidColorBrush(Colors.IndianRed);
            }
        }

        private void DbRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                RestoreProgressBar.Value = e.Percent;
            }));
        }        

        private void RatesExpander_Expanded(object sender, RoutedEventArgs e)
        {
            RatesLoad();
        }

        private void RatesLoad()
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            Tab2Query = "select * from parameters";
            SqlCommand createCommand = new SqlCommand(Tab2Query, sqliteCon);
            try
            {
                sqliteCon.Open();
                using (SqlDataReader dr = createCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Tab4EPFTextBox.Text = dr["EPF"].ToString();
                        Tab4ESICTextBox.Text = dr["ESIC"].ToString();
                        Tab4ESICMinWageTextBox.Text = dr["ESDAYS"].ToString();
                        Tab4ESICWageCeilingTextBox.Text = dr["ESICWAGE"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ParametersUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                string Query = "update parameters set EPF='" + this.Tab4EPFTextBox.Text + "',ESIC='" + this.Tab4ESICTextBox.Text + "',ESDAYS='" + this.Tab4ESICMinWageTextBox.Text + "',ESICWAGE='" + this.Tab4ESICWageCeilingTextBox.Text + "'";
                SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                createCommand.ExecuteNonQuery();
                sqliteCon.Close();
                MessageBox.Show("Saved Sucessfully");
            }
            catch (Exception ex)
            {
                sqliteCon.Close();
                MessageBox.Show(ex.Message);                
            }
        }

        private void Tab4KMHyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.esic.nic.in/contribution");
        }

        #endregion
        
    }
}