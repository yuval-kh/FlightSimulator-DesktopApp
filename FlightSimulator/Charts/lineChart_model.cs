using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlightSimulator
{
    class LineChart_model : INotifyPropertyChanged
    {
        FlightController fc;
        mediaController mc;
        string[] _names;
        Dictionary<string, LinkedList<DataPoint>> datalists;
        public event PropertyChangedEventHandler PropertyChanged;
        int timeStamp;
        int secondsTocalc;

        DataCalculations dc;

        public LineChart_model()
        {
            secondsTocalc = 30;
            fc = FlightController.GetInstance;
            mc = mediaController.GetInstance;
            fc.PropertyChanged += UpdateMedia;
        //    Names = fc.Names;

            //Names = new string[] { "aileron", "elevator", "rudder", "flaps", "slats", "speed_brake",
            //    "engine1_throttle", "engine2_throttle", "engine1_pump", "engine2_pump", "electric1_pump",
            //    "electric2_pump", "external_power", "apu_generator", "latitude_deg", "longitude_deg", "altitude_ft",
            //    "roll_deg", "pitch_deg", "heading_deg", "side_slip_deg", "airspeed_kt", "glideslop", "vertical_speed_fps",
            //    "airspeed_indicator_indicated_speed_kt", "altimeter_indicated-altitude-ft", "altimeter_pressure-alt-ft",
            //    "attitude-indicator_indicated-pitch-deg", "attitude-indicator_indicated-roll-deg",
            //    "attitude-indicator_internal-pitch-deg", "attitude-indicator_internal-roll-deg", "encoder_indicated-altitude-ft",
            //    "encoder_pressure-alt-ft", "gps_indicated-altitude-ft", "gps_indicated-ground-speed-kt", "gps_indicated-vertical-speed", 
            //    "indicated-heading-deg", "magnetic-compass_indicated-heading-deg", "slip-skid-ball_indicated-slip-skid", "turn-indicator_indicated-turn-rate", 
            //    "vertical-speed-indicator_indicated-speed-fpm", "engine_rpm" }; //need to store this globally or get dynamically from XML

         //   this.NotifyPropertyChanged("names");
              datalists = new Dictionary<string, LinkedList<DataPoint>>();
            fc.dataUpdated += Update;
            mc.PropertyChanged += UpdateMedia;
            //for(int i = 0; i < names.Length; i++)
            //{
            //    datalists.Add(names[i], new LinkedList<DataPoint>());
            //}
            timeStamp = 0;

            dc = null;
        }


        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        private void Update(object sender, FlightControllerEventArgs e)
        {
            if(dc == null)
            {
                dc = fc.getDataCalculations;
            }
            for(int i=0;i<Names.Length;i++)
            {
                       datalists[Names[i]].AddLast(new DataPoint(mc.getCurrentTimeInMilisecs(),float.Parse(e.GetData(Names[i]))));            }
                NotifyPropertyChanged("list");

            //NotifyPropertyChanged("CorralatedList");
           // NotifyPropertyChanged("CorralatedName");
            timeStamp++;
        }

        public string []Names
        {
            get { return _names; }
        }

        public LinkedList<DataPoint> getList(string name)
        {
            if (!name.Equals(""))
                return datalists[name];
            return null;
        }
        
        private void UpdateMedia(object sender, PropertyChangedEventArgs e)
        {
            if(sender == mc && e.PropertyName.Equals("goto"))
            {
                for (int i = 0; i < Names.Length; i++)
                {
                    datalists[Names[i]] = new LinkedList<DataPoint>();
                }
            }
            if (sender == fc && e.PropertyName.Equals("Names"))
            {
                this._names = fc.Names;
                for (int i = 0; i < Names.Length; i++)
                {
                    datalists.Add(Names[i], new LinkedList<DataPoint>());
                }
                this.NotifyPropertyChanged("names");
            }
        }

        public string GetCorralatedName(string name)
        {
            if (dc != null)
            {
                return dc.GetMaxPearsonName(name);
            }
            return "";
        }


        //public Tuple<DataPoint,DataPoint> linearRegOfCorrelative(string name)
        //{
        //    if (dc != null)
        //    {
        //        string nameCor = dc.GetMaxPearsonName(name);
        //        Tuple<string, string> lineAandB = dc.linear_reg(name, nameCor);
        //        float a = float.Parse(lineAandB.Item1);
        //        float b = float.Parse(lineAandB.Item2);
        //        float x1 = (float)(dc.getMin(name));
        //        float x2 = (float)(dc.getMax(name));
        //        DataPoint p1 = new DataPoint(x1, a * x1 + b);
        //        DataPoint p2 = new DataPoint(x2, a * x2 + b);
        //        return new Tuple<DataPoint, DataPoint>(p1, p2);
        //    }
        //    return null;


        //}


        //public LinkedList<DataPoint> getPointsOfCorralated (string name)
        //{
        //    if (dc!=null)
        //    {
        //        Tuple<string, string>[] pointsTuple = dc.getCorralatedTuple(name);
        //        int length = pointsTuple.Length;
        //        LinkedList<DataPoint> list = new LinkedList<DataPoint>();
        //        for (int i=0; i < length; i++)
        //        {
        //            list.AddLast(new DataPoint(float.Parse(pointsTuple[i].Item1), float.Parse(pointsTuple[i].Item2)));
        //        }
        //        // return list;
        //        return calculateLastSeconds(name);//////////////////////////////////////////////////////////////
        //    }
        //    return null;
        //}
        
        public LinkedList<DataPoint> calculateLastSeconds (string name) //name of the original
        {//new//
            if (dc == null)
                return null;
            float maxX, minX;/////////
            int currentTime = mc.getCurrentTimeInMilisecs();
            int currentLine = mc.firstLine;
            int interval = mc.defaultSpeed;
            int beginTime = currentTime - ((secondsTocalc / 2) * 1000);
            int endTime = currentTime + ((secondsTocalc / 2) * 1000);
            if (beginTime < 0)
                beginTime = 0;
            if (endTime > mc.getTotalTimeInMilisecs())
                endTime = mc.getTotalTimeInMilisecs();
            int beginLine = beginTime / interval;
            int endLine = endTime / interval;
            string[] data = fc.getParser.GetDataByName(name);
            string[] dataCorralated = fc.getParser.GetDataByName(dc.GetMaxPearsonName(name));
            maxX = minX = float.Parse(data[beginLine]);////////////
            LinkedList<DataPoint> list = new LinkedList<DataPoint>();
            for(int i = beginLine;i<endLine;i++)
            {
                list.AddLast(new DataPoint(float.Parse(data[i]), float.Parse(dataCorralated[i])));
                ////////////////
                float info = float.Parse(data[i]);
                if (info > maxX)
                    maxX = info;
                if (info < minX)
                    minX = info;
                /////////////////
            }
            ////////////////////
            Tuple<string, string> lineAandB = dc.linear_reg(name, dc.GetMaxPearsonName(name));
            float a = float.Parse(lineAandB.Item1);
            float b = float.Parse(lineAandB.Item2);

            DataPoint p1 = new DataPoint(minX, a * minX + b);
            DataPoint p2 = new DataPoint(maxX, a * maxX + b);
            list.AddLast(p1);
            list.AddLast(p2);
            ////////////////////
            return list;
        }
    }
}
