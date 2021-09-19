using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace FlightSimulator
{
    class FlightDataParser : CSVParser //get lines from a csv file parse them as flight data.
    {
        string csv_file;
        string[] lines;
        Dictionary<int, string> data_headers;
        Dictionary<string, string[]> dataByName; //////////////////////////////////////////////////
       
        private string[] Names { get;  set; }

        public FlightDataParser(string csv_file, string[] names)
        {
            Names = names;




       //     StreamReader sr = new StreamReader(csv_file);
       //     string first = sr.ReadLine();
       //     int num = File.ReadAllLines(csv_file).Length;
       //     if (!Double.TryParse(first.Split(",")[0], out _))
       //     {
       //         sr.ReadLine();
       //         num--;
       //     }
       //  //   lines = new string[num];
       //     int lineIndex = 0;
       //     string line;
       // //    while ((line = sr.ReadLine())!=null)
       // //    {
       // //        lines[lineIndex]=line;
       ////         lineIndex++;
       // //    }
       //     sr.Close();


            this.csv_file = csv_file;
            lines = File.ReadAllLines(this.csv_file);

            if (!Double.TryParse(lines[0], out _))
            {
                lines = lines.Skip(1).ToArray();
            }


            data_headers = new Dictionary<int, string>();
            for(int i=0;i<Names.Length;i++)
            {
                data_headers.Add(i, Names[i]);
            }
            //data_headers.Add(0, "aileron");
            //data_headers.Add(1, "elevator");
            //data_headers.Add(2, "rudder");
            //data_headers.Add(3, "flaps");
            //data_headers.Add(4, "slats");
            //data_headers.Add(5, "speed_brake");
            //data_headers.Add(6, "engine1_throttle");
            //data_headers.Add(7, "engine2_throttle");
            //data_headers.Add(8, "engine1_pump");
            //data_headers.Add(9, "engine2_pump");
            //data_headers.Add(10, "electric1_pump");
            //data_headers.Add(11, "electric2_pump");
            //data_headers.Add(12, "external_power");
            //data_headers.Add(13, "apu_generator");
            //data_headers.Add(14, "latitude_deg");
            //data_headers.Add(15, "longitude_deg");
            //data_headers.Add(16, "altitude_ft");
            //data_headers.Add(17, "roll_deg");
            //data_headers.Add(18, "pitch_deg");
            //data_headers.Add(19, "heading_deg");
            //data_headers.Add(20, "side_slip_deg");
            //data_headers.Add(21, "airspeed_kt");
            //data_headers.Add(22, "glideslop");
            //data_headers.Add(23, "vertical_speed_fps");                                        //vertical speed fps???
            //data_headers.Add(24, "airspeed_indicator_indicated_speed_kt");
            //data_headers.Add(25, "altimeter_indicated-altitude-ft");
            //data_headers.Add(26, "altimeter_pressure-alt-ft");
            //data_headers.Add(27, "attitude-indicator_indicated-pitch-deg");
            //data_headers.Add(28, "attitude-indicator_indicated-roll-deg");
            //data_headers.Add(29, "attitude-indicator_internal-pitch-deg");
            //data_headers.Add(30, "attitude-indicator_internal-roll-deg");
            //data_headers.Add(31, "encoder_indicated-altitude-ft");
            //data_headers.Add(32, "encoder_pressure-alt-ft");
            //data_headers.Add(33, "gps_indicated-altitude-ft");
            //data_headers.Add(34, "gps_indicated-ground-speed-kt");
            //data_headers.Add(35, "gps_indicated-vertical-speed");
            //data_headers.Add(36, "indicated-heading-deg");
            //data_headers.Add(37, "magnetic-compass_indicated-heading-deg");
            //data_headers.Add(38, "slip-skid-ball_indicated-slip-skid");
            //data_headers.Add(39, "turn-indicator_indicated-turn-rate");
            //data_headers.Add(40, "vertical-speed-indicator_indicated-speed-fpm");
            //data_headers.Add(41, "engine_rpm");






            dataByName = new Dictionary<string, string[]>();//////////////////////////////////////
            for (int i = 0; i < Names.Length; i++)
            {
                dataByName.Add(Names[i], new string[lines.Length]);
            }
   //         for (int i = 0; i < Names.Length; i++)
                for(int j=0;j<lines.Length;j++)
                {
                    string[] line_data = GetLine(j).Split(',');
                 //   for(int k=line_data.Length-1;k>=0;k--)
                 for(int i = 0; i < Names.Length; i++)
                        dataByName[Names[i]][j] = line_data[i];
                }////////////////////////////////////////////////////////////////////////

        }

        public Dictionary<string,string> Parse(int line)
        {
            Dictionary<string, string>  data_values = new Dictionary<string, string>(); //actual values
            for(int i = 0; i < data_headers.Count; i++)
            {
                string[] line_data = GetLine(line).Split(',');
                data_values.Add(data_headers[i], line_data[i]);
            }

            return data_values;
        }
        public string GetDataFromLine(int line,string name)
        {
            return Parse(line)[name];
        }
        public string GetLine(int index)
        {
            return lines[index];
        }

        public int GetNumberOfLines()
        {
            return lines.Length;
        }




        public string[] GetDataByName(string name)
        {
            return dataByName[name];
        }
        public string[] getNames { get { return Names; } }
    }
}
