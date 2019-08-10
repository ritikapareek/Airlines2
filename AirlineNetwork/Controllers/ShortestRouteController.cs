using System;
using System.Threading.Tasks;
using AirlineNetwork.Services;
using Microsoft.AspNetCore.Mvc;


namespace AirlineNetwork.Controllers
{
    [Route("api/[controller]")]
    public class ShortestRouteController : Controller
    {
        private readonly IAirlineService _airlineService;

    
        public ShortestRouteController(IAirlineService airlineService)
        {
            _airlineService = airlineService;
        }

        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            return  "use api/shortestroute/source/destination" ;
        }

        // GET api/<controller>/source/destination
        [HttpGet("{source}/{destination}")]
        public async Task<string> Get(string source, string destination)
        {
            var shortestRoute = await _airlineService.GetRoutes(source,destination);
           
            return await Task.FromResult(String.Join<string>("->", shortestRoute));
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
