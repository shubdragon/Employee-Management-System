using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Windows.Forms;
using System.Data.SqlClient;
using EmployeeManagementSystem.Reports;
using MetroFramework.Forms;
using EmployeeManagementSystem.Windows;
using System.IO;

namespace EmployeeManagementSystem
{
    public partial class ReportViewerDialog : MetroForm
    {        
        string dbConnectionString = MainWindow.dbConnectionString;
        string EidFetch = DataMgmtWindow.EidFetch;
        string StartEidFetch = DataMgmtWindow.StartEidFetch;
        string EndEidFetch = DataMgmtWindow.EndEidFetch;
        string LoadReport = DataMgmtWindow.LoadReport;
        string YearFetch = DataMgmtWindow.YearFetch;
        DateTime? SalaryDate = DataMgmtWindow.SalaryDate;
        DateTime? OrgMonthDate = DataMgmtWindow.OrgMonthDate;

        public ReportViewerDialog()
        {
            InitializeComponent();
        }


        /*
        public string processSql()
        {
            string sql = null;
            string inSql = null;
            string firstPart = null;
            string lastPart = null;
            int selectStart = 0;
            int fromStart = 0;
            string[] fields = null;
            string[] sep = { "," };
            int i = 0;
            TextObject myText;

            inSql = metroTextBox1.Text;
            inSql = inSql.ToUpper();

            selectStart = inSql.IndexOf("SELECT");
            fromStart = inSql.IndexOf("FROM");
            selectStart = selectStart + 6;
            firstPart = inSql.Substring(selectStart, (fromStart - selectStart));
            lastPart = inSql.Substring(fromStart, inSql.Length - fromStart);

            fields = firstPart.Split(',');
            firstPart = "";
            for (i = 0; i <= fields.Length - 1; i++)
            {
                if (i > 0)
                {
                    firstPart = firstPart + "," + fields[i].ToString() + "AS COLUMN" + (i + 1);
                    firstPart.Trim();

                    myText = (TextObject)objRpt.ReportDefinition.ReportObjects[i + 1];
                    myText.Text = fields[i].ToString();
                }
                else
                {
                    firstPart = firstPart + fields[i].ToString() + "AS COLUMN" + (i + 1);
                    firstPart.Trim();

                    myText = (TextObject)objRpt.ReportDefinition.ReportObjects[i + 1];
                    myText.Text = fields[i].ToString();
                }
            }
            sql = "SELECT" + firstPart + " " + lastPart;
            return sql;
        }
        */

        private void ReportViewerDialog_Load(object sender, EventArgs e)
        {
            if (LoadReport == "EmployeeDetails")
            {
                DataSet EmployeeMaster = new DataSet();
                EmployeeReport objRpt = new EmployeeReport();
                SqlConnection sqlCon = new SqlConnection(dbConnectionString);
                try
                {
                    string sql = null;
                    sqlCon.Open();
                    sql = "select * from employeeinfo where Eid = '" + EidFetch + "'";
                    /* SqlCommand createCommand = new SqlCommand(sql, sqlCon);

                     SqlDataAdapter dataAdp = new SqlDataAdapter();
                     dataAdp.SelectCommand = createCommand;

                     EmployeeMaster ds = new EmployeeMaster();
                     dataAdp.Fill(ds.Tables["EmpDataTable"]);
                     objRpt.SetDataSource(ds);
                     */
                    SqlDataAdapter dataAdp = new SqlDataAdapter(sql, sqlCon);
                    EmployeeMaster ds = new EmployeeMaster();
                    dataAdp.Fill(ds, "EmpDataTable");
                    objRpt.SetDataSource(ds.Tables[0]);

                    CrystalRptViewer.ReportSource = null;
                    CrystalRptViewer.ReportSource = objRpt;
                    CrystalRptViewer.Refresh();
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (LoadReport == "PaySlip")
            {
                //DataSet EmployeeMaster = new DataSet();
                //DataTable empTable = new DataTable("EmpDataTable");
                //DataTable salTable = new DataTable("SalaryDataTable");

                SqlConnection sqlCon = new SqlConnection(dbConnectionString);
                try
                {
                    sqlCon.Open();
                    string sql1 = "select * from employeeinfo where Eid= '" + EidFetch + "'";
                    string sql2 = "select * from salary where Eid= '" + EidFetch + "' and Date='" + SalaryDate + "'";
                    string sql3 = "select * from parameters";

                    SqlDataAdapter da1 = new SqlDataAdapter(sql1, sqlCon);
                    EmployeeMaster ds1 = new EmployeeMaster();
                    da1.Fill(ds1, "EmpDataTable");

                    SqlDataAdapter da2 = new SqlDataAdapter(sql2, sqlCon);
                    EmployeeMaster ds2 = new EmployeeMaster();
                    da2.Fill(ds2, "SalaryDataTable");

                    SqlDataAdapter da3 = new SqlDataAdapter(sql3, sqlCon);
                    EmployeeMaster ds3 = new EmployeeMaster();
                    da3.Fill(ds3, "ParametersDataTable");
                    /*SqlCommand cmd1 = new SqlCommand(sql1, sqlCon);
                    SqlDataAdapter dataAdp1 = new SqlDataAdapter();
                    dataAdp1.SelectCommand = cmd1;                    
                    dataAdp1.Fill(EmployeeMaster, "EmpDataTable");*/
                    //EmployeeMaster.Tables.Add(empTable);

                    /*SqlCommand cmd2 = new SqlCommand(sql2, sqlCon);
                    SqlDataAdapter dataAdp2 = new SqlDataAdapter();
                    dataAdp2.SelectCommand = cmd2;
                    dataAdp2.Fill(EmployeeMaster, "SalaryDataTable");*/
                    //EmployeeMaster.Tables.Add(salTable);

                    SalaryReport objRpt = new SalaryReport();
                    objRpt.Database.Tables["EmpDataTable"].SetDataSource(ds1.Tables[0]);
                    objRpt.Database.Tables["SalaryDataTable"].SetDataSource(ds2.Tables[1]);
                    objRpt.Database.Tables["ParametersDataTable"].SetDataSource(ds3.Tables[2]);
                    //objRpt.Database.Tables["EmpDataTable"].SetDataSource(EmployeeMaster.Tables[0]);
                    //objRpt.Database.Tables["SalaryDataTable"].SetDataSource(EmployeeMaster.Tables[1]);

                    CrystalRptViewer.ReportSource = null;
                    CrystalRptViewer.ReportSource = objRpt;
                    CrystalRptViewer.Refresh();
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (LoadReport == "SummarySinglePaySlip")
            {
                SqlConnection sqlCon = new SqlConnection(dbConnectionString);
                try
                {
                    sqlCon.Open();
                    string sql1 = "select * from employeeinfo where Eid= '" + EidFetch + "'";
                    string sql2 = "select * from salary where Eid= '" + EidFetch + "' and Date='" + SalaryDate + "'";
                    string sql3 = "select * from parameters";

                    SqlDataAdapter da1 = new SqlDataAdapter(sql1, sqlCon);
                    EmployeeMaster ds1 = new EmployeeMaster();
                    da1.Fill(ds1, "EmpDataTable");

                    SqlDataAdapter da2 = new SqlDataAdapter(sql2, sqlCon);
                    EmployeeMaster ds2 = new EmployeeMaster();
                    da2.Fill(ds2, "SalaryDataTable");

                    SqlDataAdapter da3 = new SqlDataAdapter(sql3, sqlCon);
                    EmployeeMaster ds3 = new EmployeeMaster();
                    da3.Fill(ds3, "ParametersDataTable");

                    SummarySalaryReport objRpt = new SummarySalaryReport();
                    objRpt.Database.Tables["EmpDataTable"].SetDataSource(ds1.Tables[0]);
                    objRpt.Database.Tables["SalaryDataTable"].SetDataSource(ds2.Tables[1]);
                    objRpt.Database.Tables["ParametersDataTable"].SetDataSource(ds3.Tables[2]);

                    CrystalRptViewer.ReportSource = null;
                    CrystalRptViewer.ReportSource = objRpt;
                    CrystalRptViewer.Refresh();
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (LoadReport == "SummaryBulkPaySlip")
            {
                SqlConnection sqlCon = new SqlConnection(dbConnectionString);
                try
                {
                    sqlCon.Open();
                    string sql1 = "select * from employeeinfo where Eid between '" + StartEidFetch + "' and '" + EndEidFetch + "'";
                    string sql2 = "select * from salary where Eid between '" + StartEidFetch + "' and '" + EndEidFetch + "' and Date='" + SalaryDate + "'";
                    string sql3 = "select * from parameters";

                    SqlDataAdapter da1 = new SqlDataAdapter(sql1, sqlCon);
                    EmployeeMaster ds1 = new EmployeeMaster();
                    da1.Fill(ds1, "EmpDataTable");

                    SqlDataAdapter da2 = new SqlDataAdapter(sql2, sqlCon);
                    EmployeeMaster ds2 = new EmployeeMaster();
                    da2.Fill(ds2, "SalaryDataTable");

                    SqlDataAdapter da3 = new SqlDataAdapter(sql3, sqlCon);
                    EmployeeMaster ds3 = new EmployeeMaster();
                    da3.Fill(ds3, "ParametersDataTable");

                    SummarySalaryReport objRpt = new SummarySalaryReport();
                    objRpt.Database.Tables["EmpDataTable"].SetDataSource(ds1.Tables[0]);
                    objRpt.Database.Tables["SalaryDataTable"].SetDataSource(ds2.Tables[1]);
                    objRpt.Database.Tables["ParametersDataTable"].SetDataSource(ds3.Tables[2]);

                    CrystalRptViewer.ReportSource = null;
                    CrystalRptViewer.ReportSource = objRpt;
                    CrystalRptViewer.Refresh();
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (LoadReport == "OrgMonthReport")
            {                
                SqlConnection sqlCon = new SqlConnection(dbConnectionString);
                try
                {
                    sqlCon.Open();                    
                    string sql2 = "select * from salary where Date='" + OrgMonthDate + "'";


                    SqlDataAdapter da2 = new SqlDataAdapter(sql2, sqlCon);
                    SalaryMaster ds2 = new SalaryMaster();
                    da2.Fill(ds2, "SalaryDataTable");

                    OrgMonthWageReport objRpt = new OrgMonthWageReport();
                    objRpt.Database.Tables["SalaryDataTable"].SetDataSource(ds2.Tables[0]);

                    CrystalRptViewer.ReportSource = null;
                    CrystalRptViewer.ReportSource = objRpt;
                    CrystalRptViewer.Refresh();
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }
            else if (LoadReport == "OrgYearAllReport")
            {
                SqlConnection sqlCon = new SqlConnection(dbConnectionString);
                try
                {
                    sqlCon.Open();
                    string sql1 = "select * from salary where year='" + YearFetch + "'";
                    string sql2 = "select * from parameters";

                    SqlDataAdapter da1 = new SqlDataAdapter(sql1, sqlCon);
                    SalaryMaster ds1 = new SalaryMaster();
                    da1.Fill(ds1, "SalaryDataTable");

                    SqlDataAdapter da2 = new SqlDataAdapter(sql2, sqlCon);
                    EmployeeMaster ds2 = new EmployeeMaster();
                    da2.Fill(ds2, "ParametersDataTable");

                    OrgYearWageReport objRpt = new OrgYearWageReport();
                    objRpt.Database.Tables["SalaryDataTable"].SetDataSource(ds1.Tables[0]);
                    objRpt.Database.Tables["ParametersDataTable"].SetDataSource(ds2.Tables[2]);

                    CrystalRptViewer.ReportSource = null;
                    CrystalRptViewer.ReportSource = objRpt;
                    CrystalRptViewer.Refresh();
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (LoadReport == "OrgYearOnlyReport")
            {
                SqlConnection sqlCon = new SqlConnection(dbConnectionString);
                try
                {
                    sqlCon.Open();
                    string sql2 = "select * from salary where Eid='"+ EidFetch +"' and Date='" + YearFetch + "'";
                    
                    SqlDataAdapter da2 = new SqlDataAdapter(sql2, sqlCon);
                    SalaryMaster ds2 = new SalaryMaster();
                    da2.Fill(ds2, "SalaryDataTable");

                    OrgYearWageReport objRpt = new OrgYearWageReport();
                    objRpt.Database.Tables["SalaryDataTable"].SetDataSource(ds2.Tables[0]);

                    CrystalRptViewer.ReportSource = null;
                    CrystalRptViewer.ReportSource = objRpt;
                    CrystalRptViewer.Refresh();
                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /*
        public EmployeeMaster Report_GetReportData()
        {
            SqlConnection sqlConn = new SqlConnection(dbConnectionString);            
            EmployeeMaster EmpDS = new EmployeeMaster();
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM employeeinfo WHERE Eid ='" + EidFetch + "'";
                cmd.Connection = sqlConn;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(EmpDS, EmpDS.EmpDataTable.TableName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed) sqlConn.Close();
                sqlConn.Dispose();
            }
            return EmpDS;
        }

        private void DisplayReport()
        {
            try
            {
                EmployeeMaster EmpDS = new EmployeeMaster();
                ReportDocument obj = new ReportDocument();
                EmpDS = Report_GetReportData();
                obj.FileName = "EmployeeReport.rpt";
                obj.SetDataSource((DataTable)EmpDS.EmpDataTable);
                CrystalRptViewer.ReportSource = obj;
                CrystalRptViewer.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //Logging.CustomizedException(ref ex, false);
            }
        }
        */
    }
}
