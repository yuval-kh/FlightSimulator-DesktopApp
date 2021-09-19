using System;
using System.Collections.Generic;
using System.Text;

namespace FlightSimulator
{
    class FlightControllerEventArgs  :EventArgs
    {
        Dictionary<string, string> dataValues;

        public FlightControllerEventArgs(Dictionary<string,string> values)
        {
            this.dataValues = values;
        }
        public string GetData(string data)
        {
            return dataValues[data];
        }
    }
}
