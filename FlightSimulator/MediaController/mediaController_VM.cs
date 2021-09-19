using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlightSimulator
{
    class mediaController_VM : INotifyPropertyChanged
    {
        private mediaController media;
        public event PropertyChangedEventHandler PropertyChanged;
        public mediaController_VM ()
        {
            this.media = mediaController.GetInstance;
            media.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public void getTotalTimeInSec(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "totalTime")
            {
                NotifyPropertyChanged("VM_getTotalTimeInSec");
            }
            //if (e.PropertyName == "currentTime")
            //{
            //    NotifyPropertyChanged("VM_getCurrentTimeInSec");
            //}
        }

        public void VM_play()
        {
            media.play();
        }
        public void VM_pause()
        {
            media.isRunning = false;
        }
        public void VM_goto(int precent)
        {
            media.goTo(precent);
        }
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        internal void VM_setSpeed(double newSpeed)
        {
            newSpeed = ((double)media.defaultSpeed / newSpeed);
            media.simulationSpeed = (int)newSpeed;
            System.Threading.Thread.Sleep(1000);
            VM_play();
        }

        public int timePrecents()
        {
            if (media.getTotalTimeInMilisecs() != 0)
            {
                float precents = (media.getCurrentTimeInMilisecs()*100 / media.getTotalTimeInMilisecs());
                return (int)precents;
            }
            return 0;
        }

        public string VM_totalTime { get { return media.getTotalTime(); } }

        public int VM_getTotalTimeInSec { get { return media.getTotalTimeInMilisecs() / 1000; } }

        public int VM_getCurrentTimeInSec { get { return media.getCurrentTimeInMilisecs() / 1000; }  }

        public string VM_currentTime { get { NotifyPropertyChanged("sliderUpdate");return media.getCurrentTime(); }}
    }
}
