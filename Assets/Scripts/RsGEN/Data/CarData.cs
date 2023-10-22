using System;

namespace RsGEN.Data
{
    [Serializable]
    public struct CarDataCollection
    {
        public CarData[] cars;
    }

    [Serializable]
    public struct CarData
    {
        public string name;
        public string country;
        public string manufacturer;
        public short year;
        public float base_pp;
        public string category;
        public string drivetrain;
        public ushort power_bhp;
        public ushort weight_kg;
        public string aspiration;
        public string[] tags;
    }
}