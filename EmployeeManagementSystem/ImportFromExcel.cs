using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class ImportFromExcel : MetroForm
    {
        string _path;
        public ImportFromExcel()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog
            {
                Filter = "Excell|*.xls;*.xlsx;"
            };
            DialogResult dr = od.ShowDialog();
            if (dr == DialogResult.Abort)
                return;
            if (dr == DialogResult.Cancel)
                return;
            PathTextBox.Text = od.FileName.ToString();
        }

        BackgroundWorker bw = new BackgroundWorker
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        private void UploadButton_Click(object sender, EventArgs e)
        {
            UploadButton.Enabled = false;
            _path = PathTextBox.Text;
            if (PathTextBox.Text == "" || !PathTextBox.Text.Contains(".xls") || !PathTextBox.Text.Contains(".xlsx"))
            {
                MessageBox.Show("Please Browse Excel file to upload", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PathTextBox.Text = "";
                UploadButton.Enabled = true;
                return;
            }
            if (bw.IsBusy)
            {
                return;
            }

            System.Diagnostics.Stopwatch sWatch = new System.Diagnostics.Stopwatch();
            bw.DoWork += (bwSender, bwArg) =>
            {
                //what happens here must not touch the form  
                //as it's in a different thread          
                sWatch.Start();
                InsertExcelRecords();
            };

            bw.ProgressChanged += (bwSender, bwArg) =>
            {
                Pb1.Value = bwArg.ProgressPercentage;
            };

            bw.RunWorkerCompleted += (bwSender, bwArg) =>
            {
                //now you're back in the UI thread you can update the form  
                //remember to dispose of bw now                 

                sWatch.Stop();
                Pb1.Value = 10;
                //work is done, no need for the stop button now...  
                
                PathTextBox.Text = "";
                BrowseButton.Enabled = true;

                //label1.Visible = false;
                bw.Dispose();
            };

            //lets allow the user to click stop  
            Pb1.Visible = true;
            //label1.Visible = true;
            MessageBox.Show("Uploading has been started !.", "Upload processing..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            UploadButton.Enabled = true;

            //Starts the actual work - triggerrs the "DoWork" event  
            bw.RunWorkerAsync();

            //InsertExcelRecords();
        }

        private void InsertExcelRecords()
        {
            try
            {
                //  ExcelConn(_path);  
                string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", _path);
                OleDbConnection Econ = new OleDbConnection(constr);
                string Query = string.Format("Select [Eid],[Name],[FatherOrHusbandName],[Relation],[UAN],[ESIC no],[Mobile],[Aadhaar],[Gender],[Email],[DOJ],[DOL],[DOB],[IFSC],[Account],[Role],[Address] FROM [{0}]", "Sheet1$");
                OleDbCommand Ecom = new OleDbCommand(Query, Econ);
                Econ.Open();

                DataSet ds = new DataSet();
                OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
                Econ.Close();
                oda.Fill(ds);
                DataTable Exceldt = ds.Tables[0];

                for (int i = Exceldt.Rows.Count - 1; i >= 0; i--)
                {
                    if (Exceldt.Rows[i]["Eid"] == DBNull.Value || Exceldt.Rows[i]["Name"] == DBNull.Value || Exceldt.Rows[i]["FatherOrHusbandName"] == DBNull.Value || Exceldt.Rows[i]["Relation"] == DBNull.Value || Exceldt.Rows[i]["Gender"] == DBNull.Value || Exceldt.Rows[i]["DOJ"] == DBNull.Value || Exceldt.Rows[i]["DOB"] == DBNull.Value)
                    {
                        Exceldt.Rows[i].Delete();
                    }
                }
                Exceldt.AcceptChanges();

                SqlConnection con = new SqlConnection();
                //creating object of SqlBulkCopy      
                SqlBulkCopy objbulk = new SqlBulkCopy(con)
                {
                    //assigning Destination table name      
                    DestinationTableName = "employeeinfo"
                };
                //Mapping Table column    
                objbulk.ColumnMappings.Add("Eid", "Eid");
                objbulk.ColumnMappings.Add("Name", "Name");
                objbulk.ColumnMappings.Add("FatherOrHusbandName", "FatherOrHusbandName");
                objbulk.ColumnMappings.Add("Relation", "Relation");
                objbulk.ColumnMappings.Add("UAN", "UAN");
                objbulk.ColumnMappings.Add("ESIC no", "ESIC no");
                objbulk.ColumnMappings.Add("Mobile", "Mobile");
                objbulk.ColumnMappings.Add("Aadhaar", "Aadhaar");
                objbulk.ColumnMappings.Add("Gender", "Gender");
                objbulk.ColumnMappings.Add("Email", "Email");
                objbulk.ColumnMappings.Add("DOJ", "DOJ");
                objbulk.ColumnMappings.Add("DOL", "DOL");
                objbulk.ColumnMappings.Add("DOB", "DOB");
                objbulk.ColumnMappings.Add("IFSC", "IFSC");
                objbulk.ColumnMappings.Add("Account", "Account");
                objbulk.ColumnMappings.Add("Role", "Role");
                objbulk.ColumnMappings.Add("Address", "Address");

                //inserting Datatable Records to DataBase   

                con.ConnectionString = MainWindow.dbConnectionString;//EmployeeManagementSystem.Properties.Settings.Default.emsdatabaseConnectionString; //Connection Details    
                con.Open();
                objbulk.WriteToServer(Exceldt);
                con.Close();
                MessageBox.Show("Data has been Imported successfully.", "Imported", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Data has not been Imported due to :{0}", ex.Message), "Not Imported", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Pb1.Visible = false;
                PathTextBox.Text = "";
                BrowseButton.Enabled = true;
                UploadButton.Visible = true;
                //label1.Visible = false;

            }
        }
        
    }
}
