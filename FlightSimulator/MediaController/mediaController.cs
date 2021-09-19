using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlightSimulator
{
    class mediaController : INotifyPropertyChanged
    {
        int SimulationSpeed;
        int DefaultSpeed;
        int FirstLine;
        int NumberOfLines;
        bool IsRunning;
        static mediaController instance = null;

        public event PropertyChangedEventHandler PropertyChanged;

        private mediaController()
        {
            DefaultSpeed = 100;
            simulationSpeed = defaultSpeed;

            firstLine = 0;
            numberOfLines = 0;
            isRunning = false;
        }

        public static mediaController GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new mediaController();
                }
                return instance;
            }
        }
        public int simulationSpeed
        {
            get { return this.SimulationSpeed; }
            set
            {
                SimulationSpeed = value;
                NotifyPropertyChanged("simulationSpeed");
            }
        }
        public int defaultSpeed
        {
            get { return this.DefaultSpeed; }
        }
        public int firstLine
        {
            get { return this.FirstLine; }
            set
            {
                if (FirstLine <= numberOfLines)
                {
                    FirstLine = value;
                    NotifyPropertyChanged("firstLine");
                    NotifyPropertyChanged("currentTime");
                    NotifyPropertyChanged("getCurrentTimeInSec");
                }
            }
        }
        public int numberOfLines
        {
            get { return this.NumberOfLines; }
            set
            {
                NumberOfLines = value;
                NotifyPropertyChanged("numberOfLines");
                NotifyPropertyChanged("totalTime");
                NotifyPropertyChanged("getTotalTimeInSec");
            }
        }

        public bool isRunning
        {
            get { return this.IsRunning && firstLine < numberOfLines; }
            set
            {
                IsRunning = value;
                NotifyPropertyChanged("isRunning");
            }
        }



        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string getTotalTime()
        {
            TimeSpan t = TimeSpan.FromMilliseconds(defaultSpeed * numberOfLines);
            return string.Format("{0:D2}m:{1:D2}s:{2:D3}ms",
                t.Hours * 60 + t.Minutes, t.Seconds, t.Milliseconds);
        }

        public string getCurrentTime()
        {
            TimeSpan t = TimeSpan.FromMilliseconds(defaultSpeed * firstLine);
            return string.Format("{0:D2}m:{1:D2}s:{2:D3}ms",
                t.Hours * 60 + t.Minutes, t.Seconds, t.Milliseconds);
        }
        public int getCurrentTimeInMilisecs()
        {
            return defaultSpeed * firstLine;
        }
        public int getTotalTimeInMilisecs()
        {
            return defaultSpeed * numberOfLines;
        }

        public void play()
        {
            if (isRunning == false)
            {
                isRunning = true;
                NotifyPropertyChanged("play");
            }
        }


        public void goTo(int precent)
        {
            isRunning = false;
            double gotoLine = ((double)precent / 100) * numberOfLines;
            firstLine = (int)gotoLine;
            System.Threading.Thread.Sleep(1000);//it was 1000 if its not working.
            play();
            NotifyPropertyChanged("goto");
        }
    }
}
