using SQLite;
using System;

namespace XSunriseSunset
{

    public class LocalNames
    {
        public string en { get; set; }
        public string ru { get; set; }
    }

    public class CityLocation
    {
        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }


    public class Location
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(20)]
        public string latitude { get; set; }

        [MaxLength(20)]
        public string longtitude { get; set; }

        [MaxLength(20)]
        public string stationID { get; set; }

        [MaxLength(100)]
        public string name { get; set; }

        [MaxLength(20)]
        public string country { get; set; }

        [MaxLength(20)]
        public string state { get; set; }

    }
}
