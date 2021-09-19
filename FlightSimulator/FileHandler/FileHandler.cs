using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlightSimulator
{
    public class FileHandler : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string xmlpath;
        private string csvpath;
        private string fgpath;
        private string anomalycsvPath;

        public string xmlPath
        {
            get { return xmlpath; }

            set
            {

                this.xmlpath = value;
                string flightgearNeededPath = xmlpath.Replace(@"data\Protocol\playback_small.xml", @"bin\fgfs.exe");
                if (fgpath.Equals("") && System.IO.File.Exists(flightgearNeededPath) && !flightgearNeededPath.Equals(xmlpath))
                    this.fgPath = flightgearNeededPath;

                if (!fgPath.Equals(""))
                {
                    string xmlNeededPath = fgPath.Replace(@"bin\fgfs.exe", @"data\Protocol\playback_small.xml");
                    if (!xmlPath.Equals(xmlNeededPath))
                    {
                        System.IO.File.Copy(xmlPath, xmlNeededPath ,true);
                        xmlPath = xmlNeededPath;
                    }
                }

                NotifyPropertyChanged("xmlPath");
            }
        }
        public string fgPath
        {
            get { return fgpath; }

            set
            {
                this.fgpath = value;
                string xmlNeededPath = fgpath.Replace(@"bin\fgfs.exe", @"data\Protocol\playback_small.xml");
                if (xmlpath.Equals("") && System.IO.File.Exists(xmlNeededPath))
                    xmlPath = xmlNeededPath;
                if (!xmlPath.Equals(xmlNeededPath) && !xmlPath.Equals(""))
                {
                    System.IO.File.Copy(xmlPath, xmlNeededPath ,true);
                    xmlPath = xmlNeededPath;
                }

                NotifyPropertyChanged("fgPath");
            }
        }
        public string csvPath
        {
            get { return csvpath; }

            set
            {
          //      if (!value.Equals(""))
                    this.csvpath = value;
                NotifyPropertyChanged("csvPath");
            }
        }

        public FileHandler()
        {
            xmlpath = "";
            fgpath = "";
            csvpath = "";
        }

        public string anomalyCsvPath
        {
            get { return anomalycsvPath; }

            set
            {
                //      if (!value.Equals(""))
                this.anomalycsvPath = value;
                NotifyPropertyChanged("anomalyCsvPath");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
    }
}
