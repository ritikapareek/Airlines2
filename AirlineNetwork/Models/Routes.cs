
namespace AirlineNetwork.Models
{
    public class Routes
    {
        #region Properties 

        public string AirlineId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        #endregion Properties 

        #region Constructor
        public Routes(string airlineID, string origin, string destination)
        {
            AirlineId = airlineID;
            Origin = origin;
            Destination = destination;
        }

        #endregion 
    }
}
