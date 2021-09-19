using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for DataViewer.xaml
    /// </summary>
    public partial class DataViewer : UserControl
    {
        passData_VM vm;
        public DataViewer()
        {
            InitializeComponent();
        }

        public void SetVM(passData_VM vm)
        {
            this.vm = vm;
            DataContext = vm;
        }
    }
}
