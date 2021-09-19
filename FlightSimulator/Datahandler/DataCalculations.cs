using System;
using System.Collections.Generic;
using System.Text;

namespace FlightSimulator
{
    class DataCalculations
    {
        FlightDataParser parser;
        Dictionary<string, DataInfo> dict;
        string nameDataZeroA;
        string nameDataZeroB;
        public DataCalculations(FlightDataParser parser)
        {
            this.parser = parser;
            nameDataZeroA = "";
            nameDataZeroB = "";
            initNewDic();
        }
    

        private void initNewDic()
        {
            int namesLength = parser.getNames.Length;
            string []names = parser.getNames;
            dict = new Dictionary< string , DataInfo>();
            for (int i=0; i< namesLength;i++)
            {
                dict.Add(names[i], new DataInfo(names[i]));
                avgAndVarByName(names[i]);
            }

            calcAllCovs();
            calcAllPearsons();

            for (int i = 0; i < namesLength; i++)
                maxPearsonOfName(names[i]);
            calcAllTuples();
        }

        private void avgAndVarByName(string name)
        {
            string[] data = parser.GetDataByName(name);
            double sum = 0;
            double sumOfVar = 0;
            int length = data.Length;
            double min = double.Parse(data[0]);
            double max = double.Parse(data[0]);
            for (int i = 0; i < length; i++)
            {
                double val = double.Parse(data[i]);
                sum += val;
                if (val > max)
                    max = val;
                if (val < min)
                    min = val;
                //var
                double toAddVar = (double.Parse(data[i]) * double.Parse(data[i]));
                sumOfVar = sumOfVar + toAddVar;
            }
            if (sum == 0)
            {
                if (nameDataZeroA.Equals("") && !(nameDataZeroB.Equals(name)))
                    nameDataZeroA = name;
                if (nameDataZeroB.Equals("") && !(nameDataZeroA.Equals(name)))
                    nameDataZeroB = name;
            }
            dict[name].MaxVal = max;
            dict[name].MinVal = min;
            dict[name].avg = sum / length;
            //var
            sumOfVar = sumOfVar / length;
            double av = dict[name].avg;
            dict[name].var = sumOfVar - av * av;
        }
        double covOfTwoVals(string name1, string name2)
        {
            double sum = 0;
            string[] data1 = parser.GetDataByName(name1);
            string[] data2 = parser.GetDataByName(name2);
            int length = data1.Length;
            for (int i = 0; i < length; i++)
            {
                sum += double.Parse(data1[i]) * double.Parse(data2[i]);
            }
            sum /= length;

            return sum - dict[name1].avg * dict[name2].avg;
            // return 1;
        }

        void calcAllCovs()
        {
            string[] names = parser.getNames;
            int length = names.Length;
            for (int i=0;i < length; i++)
                for(int j = 0; j < length; j++)
                {
                    if (i != j)
                    {
                        dict[names[i]].setCov(names[j], covOfTwoVals(names[i], names[j]));
                    }
                }
        }

        private double pearsonOfTwoVals(string name1, string name2)
        {

            double cov = dict[name1].getCov(name2);
            double var1 = Math.Sqrt(dict[name1].var);
            double var2 = Math.Sqrt(dict[name2].var);
            if (cov == 0 || var1 == 0 || var2 == 0)
            {
                return 0;
            }
            return cov / (var1 * var2);
        }
        void calcAllPearsons()
        {
            string[] names = parser.getNames;
            int length = names.Length;
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                {
                    if (i != j)
                    {
                        dict[names[i]].setPearson(names[j], pearsonOfTwoVals(names[i], names[j]));
                    }
                }
        }


        private void maxPearsonOfName(string name)
        {

            string[] names = parser.getNames;
            string maxName = "";
            double maxPearson = 0;
            int namesLength = names.Length;
            for (int i = 0; i < namesLength; i++)
            {
                if (!names[i].Equals(name))
                {
                    double pearson = Math.Abs(dict[name].getPearson(names[i]));
                    if (pearson >= maxPearson)
                    {
                        maxPearson = pearson;
                        maxName = names[i];
                    }
                }
            }

            dict[name].MaxPearson = maxPearson;

            if (maxPearson == 0 && !name.Equals(nameDataZeroA))
                dict[name].MaxPearsonName = nameDataZeroA;
            else if (maxPearson == 0 && !name.Equals(nameDataZeroB))
                dict[name].MaxPearsonName = nameDataZeroB;
            else
                dict[name].MaxPearsonName = maxName;
        }





        ////////////////////////////////////////////////////////////////
        
        private Tuple<string,string>[] calculateTuple(string name1,string name2)
        {
            string[] data1 = parser.GetDataByName(name1);
            string[] data2 = parser.GetDataByName(name2);
            int length = data1.Length;
            Tuple<string, string>[] result = new Tuple<string, string>[length];
            for(int i = 0; i < length; i++)
                result[i] = new Tuple<string, string>(data1[i], data2[i]);
            return result;
        }

        private void calcAllTuples ()
        {
            string[] names = parser.getNames;
            int length = names.Length;
            for(int i=0; i< length; i++)
            {
                dict[names[i]].corralatedPoints = calculateTuple(names[i], dict[names[i]].MaxPearsonName);
            }
        }
        ////!!!!
        
        public Tuple<string, string>[] getCorralatedTuple (string name)
        {
            return dict[name].corralatedPoints;
        }

        /////!!!


        /// <returns> returns the line by tuple of a and b  at y=ax+b equation</returns>
        public Tuple<string,string> linear_reg (string name1 , string name2)
        {
            double a = dict[name1].getCov(name2) / dict[name1].var;
            double b = dict[name2].avg - a * (dict[name1].avg);
            return new Tuple<string , string>(a.ToString(),b.ToString());
            
        }

        //////////////////////////////////////////////////////////



        public string GetMaxPearsonName(string name)
        {
            if (!name.Equals(""))
                return dict[name].MaxPearsonName;
            return "";
        }

        public double? getMaxPearson(string name)
        {
            if (!name.Equals(""))
                return dict[name].MaxPearson;
            return null;
        }

        public double? geCov(string name1 , string name2)
        {
            if (!name1.Equals(""))
                return dict[name1].getCov(name2);
            return null;
        }

        public double? getPearson(string name1, string name2)
        {
            if (!name1.Equals(""))
                return dict[name1].getPearson(name2);
            return null;
        }

        public double? getAvg(string name)
        {
            if (!name.Equals(""))
                return dict[name].avg;
            return null;
        }

        public double? getVar(string name)
        {
            if (!name.Equals(""))
                return dict[name].var;
            return null;
        }
        public double? getMax(string name)
        {
            if (!name.Equals(""))
                return dict[name].MaxVal;
            return null;
        }

        public double? getMin(string name)
        {
            if (!name.Equals(""))
                return dict[name].MinVal;
            return null;
        }


    }

}
