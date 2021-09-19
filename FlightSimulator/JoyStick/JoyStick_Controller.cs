/*using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace FlightSimulator
{
    class JoyStick_Controller : INotifyPropertyChanged
    {
        FlightController fc;
        static JoyStick_Controller instance = null;
        private JoyStick_Controller()
        {
            this._aileron = "0";
            this._elevator = "0";
            this._rudder = "0";
            this._engine1_throttle = "0";
            fc = FlightController.GetInstance;
            fc.dataUpdated += Fc_dataUpdated;
        }

        private void Fc_dataUpdated(object sender, FlightControllerEventArgs e)
        {
            this._aileron = e.GetData("aileron");
            this._elevator = e.GetData("elevator");
            //this._rudder = e.GetData("rudder");
            //this._engine1_throttle = e.GetData("engine1_throttle");
            NotifyPropertyChanged("upDate");
        }

        public static JoyStick_Controller GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JoyStick_Controller();
                }
                return instance;
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        string _aileron;
        public string aileron
        {
            get { return this._aileron; }
            set {
                this._aileron = value;
                NotifyPropertyChanged("aileron");
            }
        }
        string _elevator;
        public string elevator
        {
            get { return this._elevator; }
            set {
                this._elevator = value;
                NotifyPropertyChanged("elevator");
            }
        }
        string _rudder;
        public string rudder
        {
            get { return this._rudder; }
            set
            {
                this._rudder = value;
                NotifyPropertyChanged("rudder");
            }
        }
        string _engine1_throttle;
        public string throttle
        {
            get { return this._engine1_throttle; }
            set
            {
                this._engine1_throttle = value;
                NotifyPropertyChanged("engine1_throttle");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
*/