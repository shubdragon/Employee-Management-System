using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DGVPrinterHelper;
using MahApps.Metro.Controls;

namespace EmployeeManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for PrintSelectionWindows.xaml
    /// </summary>
    public partial class PrintSelectionWindows : MetroWindow
    {
        string dbConnectionString = MainWindow.dbConnectionString;

        public PrintSelectionWindows()
        {
            InitializeComponent();
            EidCheckBox.IsChecked = true;
            EidCheckBox.IsEnabled = false;
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                string Query = "select Eid,Name,FatherOrHusbandName,Relation,UAN,[ESIC no],Mobile,Aadhaar,Gender,Email,DOJ,DOL,DOB,IFSC,Account,Role,Address,Image from employeeinfo";
                SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                DataTable dt = new DataTable("employeeinfo");
                dataAdp.Fill(dt);
                Printdgv.DataSource = dt.DefaultView;                
                dataAdp.Update(dt);

                sqliteCon.Close();
                //PrintTableDataGrid.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HeaderPrintButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqliteCon = new SqlConnection(dbConnectionString);
            string Query = "select " + CPEidTextBlock.Text + CPNameTextBlock.Text + CPFHTextBlock.Text + CPRelationTextBlock.Text + CPUANTextBlock.Text + CPESICTextBlock.Text + CPMobileTextBlock.Text + CPAadhaarTextBlock.Text + CPGenderTextBlock.Text + CPEmailTextBlock.Text + CPDOJTextBlock.Text + CPDOLTextBlock.Text + CPDOBTextBlock.Text + CPIFSCTextBlock.Text + CPAccountTextBlock.Text + CPRoleTextBlock.Text + CPAddressTextBlock.Text + CPImageTextBlock.Text + " from employeeinfo";
            try
            {
                sqliteCon.Open(); // "select Eid,Name,FatherOrHusbandName,Relation,UAN,ESIC,Mobile,Aadhaar,Gender,Email,DOJ,DOL,DOB,IFSC,Account,Role,Address from employeeinfo";
                
                SqlCommand createCommand = new SqlCommand(Query, sqliteCon);
                createCommand.ExecuteNonQuery();

                SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                DataTable dt = new DataTable("employeeinfo");
                dataAdp.Fill(dt);
                Printdgv.DataSource = dt.DefaultView;
                dataAdp.Update(dt);

                sqliteCon.Close();
                //PrintTableDataGrid.SelectedIndex = 0;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            
            //Print(PrintTableDataGrid);
            DGVPrinter printer = new DGVPrinter();
            if(PrintModeComboBox.SelectedIndex == 0)
            {
                printer.printDocument.DefaultPageSettings.Landscape = true;
            }
            else if(PrintModeComboBox.SelectedIndex == 1)
            {
                printer.printDocument.DefaultPageSettings.Landscape = false;
            }
            printer.Title = TitleTextBox.Text;
            printer.SubTitle = SubTitleTextBox.Text;
            if(PageCheckBox.IsChecked == true)
            {
                printer.PageNumbers = true;
            }
            else
            {
                printer.PageNumbers = false;
            }
            if(HFComboBox.SelectedIndex == 1)
            {
                printer.PageNumberInHeader = false;
            }
            else if(HFComboBox.SelectedIndex == 0)
            {
                printer.PageNumberInHeader = true;
            }            
            printer.Footer = FooterTextBox.Text;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = System.Drawing.StringAlignment.Near;
            printer.SubTitleFormatFlags = System.Drawing.StringFormatFlags.LineLimit | System.Drawing.StringFormatFlags.NoClip;
            printer.FooterSpacing = 15;
            
            //printer.PageSeparator = " / ";
            //printer.PageSettings.PaperSize.Width = 526;
            //var paperSize = printDoc.PrinterSettings.PaperSizes.Cast<System.Drawing.Printing.PaperSize>().FirstOrDefault(e => e.PaperName == "A5");
            //printDoc.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;            
            printer.PrintPreviewDataGridView(Printdgv);
        }

        /*
        private void Print(Visual v)
        {

            if (!(v is FrameworkElement e))
                return;

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                //store original scale
                Transform originalScale = e.LayoutTransform;
                //get selected printer capabilities
                PrintCapabilities capabilities = pd.PrintQueue.GetPrintCapabilities(pd.PrintTicket);

                //get scale of the print wrt to screen of WPF visual
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / e.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                               e.ActualHeight);

                //Transform the Visual to scale
                e.LayoutTransform = new ScaleTransform(scale, scale);

                //get the size of the printer page
                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                //update the layout of the visual to the printer page size.
                e.Measure(sz);
                e.Arrange(new Rect(new System.Windows.Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                //now print the visual to printer to fit on the one page.
                pd.PrintVisual(v, "My Print");

                //apply the original transform.
                e.LayoutTransform = originalScale;
            }
        }
        */

        private void EidCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPEidTextBlock.Text = "Eid";
        }

        private void EidCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPEidTextBlock.Text = "";
        }

        private void NameCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPNameTextBlock.Text = ",Name";
        }

        private void NameCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPNameTextBlock.Text = "";
        }

        private void FHCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPFHTextBlock.Text = ",FatherOrHusbandName";
        }

        private void FHCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPFHTextBlock.Text = "";

        }

        private void RelationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPRelationTextBlock.Text = ",Relation";
        }

        private void RelationCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPRelationTextBlock.Text = "";
        }

        private void UANCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPUANTextBlock.Text = ",UAN";
        }

        private void UANCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPUANTextBlock.Text = "";
        }

        private void ESICCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPESICTextBlock.Text = ",[ESIC no]";
        }

        private void ESICCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPESICTextBlock.Text = "";
        }

        private void MobileCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPMobileTextBlock.Text = ",Mobile";
        }

        private void MobileCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPMobileTextBlock.Text = "";
        }

        private void AadhaarCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPAadhaarTextBlock.Text = ",Aadhaar";
        }

        private void AadhaarCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPAadhaarTextBlock.Text = "";
        }

        private void GenderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPGenderTextBlock.Text = ",Gender";
        }

        private void GenderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPGenderTextBlock.Text = "";
        }

        private void EmailCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPEmailTextBlock.Text = ",Email";
        }

        private void EmailCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPEmailTextBlock.Text = "";
        }

        private void DOJCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPDOJTextBlock.Text = ",DOJ";
        }

        private void DOJCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPDOJTextBlock.Text = "";
        }

        private void DOLCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPDOLTextBlock.Text = ",DOL";
        }

        private void DOLCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPDOLTextBlock.Text = "";
        }

        private void DOBCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPDOBTextBlock.Text = ",DOB";
        }

        private void DOBCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPDOBTextBlock.Text = "";
        }

        private void IFSCCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPIFSCTextBlock.Text = ",IFSC";
        }

        private void IFSCCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPIFSCTextBlock.Text = "";
        }

        private void AccountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPAccountTextBlock.Text = ",Account";
        }

        private void AccountCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPAccountTextBlock.Text = "";
        }

        private void RoleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPRoleTextBlock.Text = ",Role";
        }

        private void RoleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPRoleTextBlock.Text = "";
        }

        private void AddressCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPAddressTextBlock.Text = ",Address";
        }

        private void AddressCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPAddressTextBlock.Text = "";
        }

        private void ImageCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CPImageTextBlock.Text = ",Image";
        }

        private void ImageCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CPImageTextBlock.Text = "";
        }
    }    
}
