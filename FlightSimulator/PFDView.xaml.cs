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
    /// Interaction logic for PFDView.xaml
    /// </summary>
    public partial class PFDView : UserControl
    {
        passData_VM vm;
        FlightController fc;
        public PFDView()
        {
            InitializeComponent();
            fc = FlightController.GetInstance;
            fc.dataUpdated += Fc_dataUpdated;
        }

        private void Fc_dataUpdated(object sender, FlightControllerEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => {
                rotate_trans.CenterX = ecc.ActualWidth / 2;
                rotate_trans.CenterY = ecc.ActualHeight / 2;
                rotate_trans.Angle = -1 * float.Parse(e.GetData("roll-deg"));
                setPitch(float.Parse(e.GetData("pitch-deg")));
            }));
        }
        private void setPitch(float val)
        {
            pfd_pitch.EndPoint = new Point(0, map(val, -45, 45, 0, 3));
        }
        public void SetVM(passData_VM vm)
        {
            this.vm = vm;
            DataContext = vm;
        }

        private double map(double x, double in_min, double in_max, double out_min, double out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
    }
}
