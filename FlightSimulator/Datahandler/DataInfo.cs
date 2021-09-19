using System;
using System.Collections.Generic;
using System.Text;

namespace FlightSimulator
{
    class DataInfo
    {
        private string name;
        Dictionary<string, double> cov;
        Dictionary<string, double> pearson;
      //  Tuple<string, string> []corraletedPoints;

        public DataInfo(string name)
        {
            this.name = name;
            cov = new Dictionary<string, double>();
            pearson = new Dictionary<string, double>();
        }

        public double avg { get; set; } 

        public double var { get; set; } 

        public string MaxPearsonName { get; set; }

        public double MaxPearson { get; set; }  

        public double MaxVal { get; set; } 

        public double MinVal { get; set; } 

        public void setCov (string name, double cov)
        {
            this.cov.Add(name, cov);
        }
        public double getCov(string name)
        {
            return cov[name];
        }
        public void setPearson(string name, double cov)
        {
            this.pearson.Add(name, cov);
        }
        public double getPearson(string name)
        {
            return pearson[name];
        }


        public Tuple<string,string>[] corralatedPoints { get; set; }
    }
}
