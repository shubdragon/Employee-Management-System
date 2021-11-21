using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MetroFramework.Forms;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class ExportDialog : MetroForm
    {
        string dbConnectionString = MainWindow.dbConnectionString;
        public ExportDialog()
        {
            InitializeComponent();
        }

        private void ExportDialog_Load(object sender, EventArgs e)
        {
            if (ExportBackgroundWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                ExportBackgroundWorker.RunWorkerAsync();
            }
            
        }

        private void ExportBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ExportRecordToExcel();                
            }            
        }

        private void ExportBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void ExportBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            ExportBackgroundWorker.Dispose();
            this.Close();
        }

        private void ExportRecordToExcel()
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                string Query = "select Eid,Name,FatherOrHusbandName,Relation,UAN,[ESIC no],Mobile,Aadhaar,Gender,Email,DOJ,DOL,DOB,IFSC,Account,Role,Address from employeeinfo ";

                SqlDataAdapter dataAdp = new SqlDataAdapter(Query, sqliteCon);
                DataTable dt = new DataTable("employeeinfo");
                dataAdp.Fill(dt);
                DataColumnCollection dcCollection = dt.Columns;

                //This code produces the correct Excel File with the expected content
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dt.Rows.Count + 2; i++)
                {
                    for (int j = 1; j < dt.Columns.Count + 1; j++)
                    {
                        if (i == 1)
                        {
                            ExcelApp.Cells[i, j] = dcCollection[j - 1].ToString();
                        }
                        else
                            ExcelApp.Cells[i, j] = dt.Rows[i - 2][j - 1].ToString();
                    }
                }
                Microsoft.Office.Interop.Excel.Worksheet ws = ExcelApp.ActiveWorkbook.Worksheets[1];
                ws.Columns["E"].NumberFormat = "0";
                ws.Columns["F"].NumberFormat = "0";
                ws.Columns["O"].NumberFormat = "0";
                ws.Range["E1"].NumberFormat = "";
                ws.Range["F1"].NumberFormat = "";
                ws.Range["O1"].NumberFormat = "";
                ws.Columns.AutoFit();
                ws.Rows.AutoFit();
                ExcelApp.ActiveWorkbook.SaveAs(@"C:\Users\Public\imported.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                 false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
                MessageBox.Show("Data Exported Successfully into C:\\Users\\Public\\imported.xlsx", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                sqliteCon.Close();

                try
                {
                    Process.Start("explorer.exe", @"C:\Users\Public");
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
