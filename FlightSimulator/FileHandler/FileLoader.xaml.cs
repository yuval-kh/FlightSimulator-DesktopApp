using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for FileLoader.xaml
    /// </summary>
    public partial class FileLoader : Window
    {

        FileLoader_VM vm;

        public FileLoader(FileHandler fh)
        {
            InitializeComponent();
            vm = new FileLoader_VM(fh);
            DataContext = vm;
            CheckIfOpened();

        }


        private void checkFinish()
        {
            if (vm.VM_csvPath != "" && vm.VM_xmlPath != "" && vm.VM_fgPath != "" && vm.VM_anomalyCsvPath != "")
            {
                btn_submit.IsEnabled = true;
            }
        }

        private void CheckIfOpened()
        {
            if(File.Exists("paths.txt")){
                string[] lines = File.ReadAllLines("paths.txt");
                vm.VM_csvPath = lines[0];
                vm.VM_xmlPath = lines[1];
                vm.VM_fgPath = lines[2];
                vm.VM_anomalyCsvPath = lines[3];
                tb_csvPath.Text = lines[0];
                tb_xmlPath.Text = lines[1];
                tb_exePath.Text = lines[2];
                tb_anomaliesPath.Text = lines[3];
                checkFinish();
            }
        }


        private void btn_openXml_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.DefaultExt = ".xml";
            openFileDlg.Filter = "XML Files (*.xml)|*.xml";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                tb_xmlPath.Text = openFileDlg.FileName;
                vm.VM_xmlPath = openFileDlg.FileName;
                checkFinish();
            }
        }

        private void btn_opebCSV_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.DefaultExt = ".csv";
            openFileDlg.Filter = "CSV Files (*.csv)|*.csv";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                tb_csvPath.Text = openFileDlg.FileName;
                vm.VM_csvPath = openFileDlg.FileName;
                checkFinish();
            }
        }

        private void btn_opebEXE_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.DefaultExt = ".exe";
            openFileDlg.Filter = "Executable Files (*.exe)|*.exe";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                tb_exePath.Text = openFileDlg.FileName;
                vm.VM_fgPath = openFileDlg.FileName; ;
                checkFinish();
            }
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            string[] log = new string[4];
            log[0] = vm.VM_csvPath;
            log[1] = vm.VM_xmlPath;
            log[2] = vm.VM_fgPath;
            log[3] = vm.VM_anomalyCsvPath;
            File.WriteAllLines("paths.txt", log);
            this.Close();
        }

        private void btn_opebANOMALIES_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.DefaultExt = ".csv";
            openFileDlg.Filter = "Executable Files (*.csv)|*.csv";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                tb_anomaliesPath.Text = openFileDlg.FileName;
                vm.VM_anomalyCsvPath = openFileDlg.FileName; ;
                checkFinish();
            }
        }
    }
}
