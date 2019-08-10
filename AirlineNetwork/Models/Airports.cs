
namespace AirlineNetwork.Models
{
    public class Airports
    {
        #region Properties 

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string IATA3 { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        #endregion

        #region Constructor
        public Airports(string name, string city, string country, string iata3, string latitude, string longtitude)
        {
            Name = name;
            City = city;
            Country = country;
            IATA3 = iata3;
            Latitude = latitude;
            Longitude = longtitude;
        }

        #endregion 
    }
}
