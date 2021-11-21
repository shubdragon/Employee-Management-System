using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace EmployeeManagementSystem
{
    public class TotalConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal BEarnings = 0;
            decimal DAEarnings = 0;
            decimal CAEarnings = 0;
            decimal MAEarnings = 0;
            decimal HRAEarnings = 0;
            decimal FAEarnings = 0;
            decimal SAEarnings = 0;
            decimal OEarnings = 0;
            decimal TaxDeductions = 0;
            decimal EPFDeductions = 0;
            decimal ESICDeductions = 0;
            decimal LADeductions = 0;
            decimal ODeductions = 0;
            string TotalAmount = string.Empty;
            BEarnings = (values[0] != null && values[0] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[0]) : 0;
            DAEarnings = (values[0] != null && values[1] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[1]) : 0;
            CAEarnings = (values[0] != null && values[2] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[2]) : 0;
            MAEarnings = (values[0] != null && values[3] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[3]) : 0;
            HRAEarnings = (values[0] != null && values[4] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[4]) : 0;
            FAEarnings = (values[0] != null && values[5] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[5]) : 0;
            SAEarnings = (values[0] != null && values[6] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[6]) : 0;
            OEarnings = (values[0] != null && values[7] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[7]) : 0;
            TaxDeductions = (values[0] != null && values[8] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[8]) : 0;
            EPFDeductions = (values[0] != null && values[9] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[9]) : 0;
            ESICDeductions = (values[0] != null && values[10] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[10]) : 0;
            LADeductions = (values[0] != null && values[11] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[11]) : 0;
            ODeductions = (values[0] != null && values[12] != DependencyProperty.UnsetValue) ? System.Convert.ToDecimal(values[12]) : 0;
            TotalAmount = System.Convert.ToString(BEarnings + DAEarnings + CAEarnings + MAEarnings + HRAEarnings + FAEarnings + SAEarnings + OEarnings - TaxDeductions - EPFDeductions - ESICDeductions - LADeductions - ODeductions);
            return TotalAmount;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
