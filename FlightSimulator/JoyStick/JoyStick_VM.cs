//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.ComponentModel;

//namespace FlightSimulator
//{

//    class JoyStick_VM : INotifyPropertyChanged
//    {
//        private JoyStick_Controller joyStick;
//        public JoyStick_VM()
//        {
//            this.joyStick = JoyStick_Controller.GetInstance;
//            //joyStick.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
//            //{
//            //   // NotifyPropertyChanged("VM_" + e.PropertyName);

//            //};
//            joyStick.PropertyChanged += JoyStick_PropertyChanged;
//        }

//        private void JoyStick_PropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            if (e.PropertyName.Equals("upDate")) {
//                this.Aliron = Double.Parse(joyStick.aileron);
//                this.Elevator = Double.Parse(joyStick.elevator);
//                NotifyPropertyChanged("positionChanged");
//            }
//        }

//        public void NotifyPropertyChanged(string propName)
//        {
//            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
//        }
        
//        public event PropertyChangedEventHandler PropertyChanged;
//        public double Aliron
//        {
//            get; set;
//        }
//        public double Elevator
//        {
//            get; set;
//        }
//        /*public double VM_aileron
//        {
//            get { return Double.Parse(joyStick.aileron); }
//        }
//        public double VM_elevator
//        {
//            get { return Double.Parse(joyStick.elevator); }
//        }*/
//        public string VM_rudder
//        {
//            get { return joyStick.rudder; }
//        }
//        public string VM_throttle
//        {
//            get { return joyStick.throttle; }
//        }
//    }
//}
