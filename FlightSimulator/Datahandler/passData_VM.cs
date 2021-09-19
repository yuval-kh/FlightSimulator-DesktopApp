using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlightSimulator
{
    public class passData_VM : INotifyPropertyChanged
    {
        private FlightSimulator.FlightController controller;
        public event PropertyChangedEventHandler PropertyChanged;
        float airspeed;
        float altitude;
        float headingDeg;
        float rollDeg;
        float pitchDeg;
        float sideSlipDeg;
        float throttle; //0-1
        float rudder; //-1-1
        float aileron;
        float elevator;




        public passData_VM()
        {
            controller = FlightSimulator.FlightController.GetInstance;
            controller.dataUpdated += Update;
            VM_Airspeed = 0;
            VM_Altitude = 0;
            VM_HeadingDeg = 0;
            VM_RollDeg = 0;
            VM_PitchDeg = 0;
            VM_SideSlipDeg = 0;
            VM_throttle = 0;
            VM_rudder = 0;
            VM_aileron = 0;
            VM_elevator = 0;

        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propName));
        }

        private void Update(object sender, FlightControllerEventArgs e)
        {
            VM_Airspeed = float.Parse(e.GetData("airspeed-kt"));
            VM_Altitude = float.Parse(e.GetData("altitude-ft"));
            VM_HeadingDeg = float.Parse(e.GetData("heading-deg"));
            VM_RollDeg = float.Parse(e.GetData("roll-deg"));
            VM_PitchDeg = float.Parse(e.GetData("pitch-deg"));
            VM_SideSlipDeg = float.Parse(e.GetData("side-slip-deg"));
            VM_throttle = float.Parse(e.GetData("throttle"));
            VM_rudder = float.Parse(e.GetData("rudder"));
            VM_aileron = float.Parse(e.GetData("aileron"));
            VM_elevator = float.Parse(e.GetData("elevator"));
            //NotifyPropertyChanged("VM_Airspeed");

        }
        public float VM_Airspeed
        {
            get
            {
                return airspeed;
            }
            set
            {

                airspeed = value;
                NotifyPropertyChanged("VM_Airspeed");
            }
        }
        public float VM_Altitude
        {
            get
            {
                return altitude;
            }
            set
            {

                altitude = value;
                NotifyPropertyChanged("VM_Altitude");
            }
        }

        public float VM_HeadingDeg
        {
            get
            {
                return headingDeg;
            }
            set
            {

                headingDeg = value;
                NotifyPropertyChanged("VM_HeadingDeg");
            }
        }
        public float VM_RollDeg
        {
            get
            {
                return rollDeg;
            }
            set
            {

                rollDeg = value;
                NotifyPropertyChanged("VM_RollDeg");
            }
        }
        public float VM_PitchDeg
        {
            get
            {
                return pitchDeg;
            }
            set
            {

                pitchDeg = value;
                NotifyPropertyChanged("VM_PitchDeg");
            }
        }
        public float VM_SideSlipDeg
        {
            get
            {
                return sideSlipDeg;
            }
            set
            {

                sideSlipDeg = value;
                NotifyPropertyChanged("VM_SideSlipDeg");
            }
        }
        public float VM_throttle
        {
            get
            {
                return throttle;
            }
            set
            {

                throttle = value;
                NotifyPropertyChanged("VM_throttle");
            }
        }
        public float VM_rudder
        {
            get
            {
                return rudder;
            }
            set
            {

                rudder = value;
                NotifyPropertyChanged("VM_rudder");
            }
        }

        public float VM_elevator
        {
            get
            {
                return elevator;
            }
            set
            {

                elevator = value;
                NotifyPropertyChanged("VM_elevator");
            }
        }
        public float VM_aileron
        {
            get
            {
                return aileron;
            }
            set
            {

                aileron = value;
                NotifyPropertyChanged("VM_aileron");
            }
        }
    }
}
