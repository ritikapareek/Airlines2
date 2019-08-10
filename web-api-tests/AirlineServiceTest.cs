using AirlineNetwork.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace web_api_tests
{
    public class AirlineServiceTest : IAirlineService
    {
        private readonly List<string> _routes;

        public AirlineServiceTest()
        {
            _routes = new List<string>
            {
                "JFK",
                "YYZ"
            };
        } 
        public async Task<List<string>> GetRoutes(string source, string destination)
        {
            return await Task.FromResult(_routes);
        }
    }
}
