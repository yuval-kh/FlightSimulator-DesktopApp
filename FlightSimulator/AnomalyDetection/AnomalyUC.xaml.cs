using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for AnomalyUC.xaml
    /// </summary>
    public partial class AnomalyUC : UserControl
    {
        PlotModel plotModel;
        Anomalies_vm anomalies_Vm;
        public AnomalyUC()
        {
            InitializeComponent();
            plotModel = initModel();
            plot.Model = plotModel;
        }

        PlotModel initModel()
        {
            PlotModel model = new PlotModel { Title = "Anomaly Graph" };
            model.LegendPosition = LegendPosition.RightBottom;
            model.LegendPlacement = LegendPlacement.Outside;
            model.LegendOrientation = LegendOrientation.Horizontal;
            var Yaxis = new OxyPlot.Axes.LinearAxis();
            var XAxis = new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom };
            model.Axes.Add(Yaxis);
            model.Axes.Add(XAxis);
            return model;
        }
        public double getValue(string func, double x)
        {
            string funcCopy = func;
            string result;
            funcCopy = funcCopy.Replace("x", x.ToString());
            funcCopy = funcCopy.Replace("X", x.ToString());
            NCalc.Expression e = new NCalc.Expression(funcCopy);
            result = e.Evaluate().ToString();
            return Double.Parse(result);
            //return 20.0;
        }

        public void LoadCSVS(string normal, string anomaly)
        {
            anomalies_Vm = new Anomalies_vm(normal, anomaly);
            anomalies_Vm.PropertyChanged += Update;
            Dispatcher.BeginInvoke(new Action(() => {
                DataContext = anomalies_Vm;
                cmb_items.ItemsSource = anomalies_Vm.Names;
            }));
        }

        private void Update(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("data") && sender == anomalies_Vm)
            {
                //cmb_items.ItemsSource = anomalies_Vm.Names;
                plot.Model = null;
                plotModel = new PlotModel();
                plotModel = initModel();
                plotModel.Series.Clear();
                PairData data = anomalies_Vm.dataPair;
                //add functions.

                
                foreach(string f in data.function)
                {
                    FunctionSeries functionSeries =  new FunctionSeries((x) => getValue(f,x), data.minPoint, data.maxPoint, (data.maxPoint - data.minPoint) / 100) { Color = OxyColors.Black };
                    plotModel.Series.Add(functionSeries);
                }
                ScatterSeries normal_scatterSeries = new ScatterSeries();
                normal_scatterSeries.MarkerType = MarkerType.Circle;
                normal_scatterSeries.MarkerSize = 2;
                normal_scatterSeries.MarkerFill = OxyColors.Blue;
                normal_scatterSeries.TrackerFormatString = "{PointInfo}";

                foreach(FlightPoint p in data.normal_points)
                {
                    normal_scatterSeries.Points.Add(p);
                }

                ScatterSeries anoamloy_scatterSeries = new ScatterSeries();
                anoamloy_scatterSeries.MarkerType = MarkerType.Circle;
                anoamloy_scatterSeries.MarkerSize = 2;
                anoamloy_scatterSeries.MarkerFill = OxyColors.Red;
                anoamloy_scatterSeries.TrackerFormatString = "{PointInfo}";

                foreach (FlightPoint p in data.anoamly_points)
                {
                    anoamloy_scatterSeries.Points.Add(p);
                }
                plotModel.Series.Add(anoamloy_scatterSeries);
                plotModel.Series.Add(normal_scatterSeries);
                plot.Model = plotModel;
            }
        }

        private void cmb_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmb_items.SelectedItem != null)
            {
                //System.Diagnostics.Trace.WriteLine(cmb_items.SelectedItem.ToString());
                anomalies_Vm.dataUpdate(cmb_items.SelectedItem.ToString());
            }
        }
    }
}
