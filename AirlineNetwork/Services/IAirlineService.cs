using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirlineNetwork.Services
{
    public interface IAirlineService
    {
         Task<List<string>> GetRoutes(string source, string destination);
    }
}
