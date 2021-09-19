using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlightSimulator
{
    class Anomalies_vm : INotifyPropertyChanged
    {
        DLLDataParser parser;
        PairData data;
        
        public event PropertyChangedEventHandler PropertyChanged;
        public Anomalies_vm(string normal, string anomaly)
        {
            parser = new DLLDataParser(normal, anomaly);
            parser.PropertyChanged += Update;
           // NotifyPropertyChanged("VM_Names");
        }
        private void Update(object sender, PropertyChangedEventArgs e)
        {
       //     if (e.PropertyName.Equals("Names"))
      //          NotifyPropertyChanged("VM_Names");
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string[] Names { get { return parser.getNamesPairs(); } }

        public void dataUpdate(string pair)
        {
            dataPair = parser.getPair(pair);
        }

        public PairData dataPair
        {
            get
            {
                return data;
            }
           private set
            {
                data = value;
                NotifyPropertyChanged("data");
            }
        }
    }
}
