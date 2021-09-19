using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
    /// Interaction logic for AnomalyGraphWindow.xaml
    /// </summary>
    public partial class AnomalyGraphWindow : Window
    {
        string normal_flight_csv;
        string anomaly_flight_csv;
        public AnomalyGraphWindow()
        {
            InitializeComponent();
            anomaly_uc.Visibility = Visibility.Hidden;
        }
        bool loadDLL()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = ".dll";
            fd.Filter = "DLL Files (*.dll)|*.dll";
            Nullable<bool> result = fd.ShowDialog();
            if (result == true)
            {
                File.Copy(fd.FileName, Environment.CurrentDirectory + @"/AnomalyDetectionLib.dll",true);
                return true;
            }
            return false;
        }
        public void LoadCSV(string normal, string anomaly)
        {
            normal_flight_csv = normal;
            anomaly_flight_csv = anomaly;
            if (loadDLL())
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        anomaly_uc.Visibility = Visibility.Hidden;
                        txt_loading.Visibility = Visibility.Visible;
                    }));
                    anomaly_uc.LoadCSVS(normal, anomaly);
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        anomaly_uc.Visibility = Visibility.Visible;
                        txt_loading.Visibility = Visibility.Hidden;
                    }));
                }).Start();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadCSV(normal_flight_csv,anomaly_flight_csv);
        }
    }
}
