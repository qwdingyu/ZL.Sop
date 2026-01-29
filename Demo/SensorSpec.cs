using System;

namespace Demo
{
    public class KeyTrafficLogModel
    {
        public DateTime Timestamp { get; set; }

        public string StepName { get; set; }

        public string Direction { get; set; }

        public string DataHex { get; set; }

        public string Meaning { get; set; }

        public bool IsError { get; set; }
    }
    public class SensorSpec
    {
        public string Name { get; set; }

        public int LCL { get; set; }

        public int UCL { get; set; }

        public string Unit { get; set; }

        public SensorSpec(string name, int lcl, int ucl)
        {
            Name = name;
            LCL = lcl;
            UCL = ucl;
        }
    }
    public class SensorValues
    {
        public int SeatPosition { get; set; }

        public int BackrestPosition { get; set; }

        public int CushionPosition { get; set; }
    }
}
