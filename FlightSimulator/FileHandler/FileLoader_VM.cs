using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlightSimulator
{
    class FileLoader_VM : INotifyPropertyChanged
    {
        private FileHandler fh;
        public event PropertyChangedEventHandler PropertyChanged;

        public FileLoader_VM(FileHandler fh)
        {

            this.fh = fh;
            fh.PropertyChanged += delegate (Object sender, System.ComponentModel.PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }

        public string VM_xmlPath
        {
            get {return fh.xmlPath; }

            set { fh.xmlPath = value; } 
        }

        public string VM_fgPath
        {
            get { return fh.fgPath; }

            set {fh.fgPath = value; }   
        }
        public string VM_csvPath
        {
            get { return fh.csvPath; }

            set { fh.csvPath = value; } 
        }
        public string VM_anomalyCsvPath
        {
            get { return fh.anomalyCsvPath; }

            set { fh.anomalyCsvPath = value; }
        }


    }
}
