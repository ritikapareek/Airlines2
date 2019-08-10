
namespace AirlineNetwork.Models
{
    public class Airline
    {
        #region Properties

        public string Name { get; set; }
        public string TwoDigitCode { get; set; }
        public string ThreeDigitCode { get; set; }
        public string Country { get; set; }

        #endregion

        #region Constructor
        public Airline(string name, string twoDigit, string threeDigit, string country)
        {
            Name = name;
            TwoDigitCode = twoDigit;
            ThreeDigitCode = threeDigit;
            Country = country;
        }

        #endregion Constructor
    }
}
