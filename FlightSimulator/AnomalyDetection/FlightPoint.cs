using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightSimulator
{
    class FlightPoint : ScatterPoint
    {
        public string PointInfo { get; private set; }
        public FlightPoint(double x, double y, string info) : base(x, y)
        {
            PointInfo = info;
        }
    }
}
