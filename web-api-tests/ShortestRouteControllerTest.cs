using AirlineNetwork.Controllers;
using AirlineNetwork.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace web_api_tests
{
   
    public class ShortestRouteControllerTest
    {
        ShortestRouteController _controller;
        IAirlineService _service;

        public ShortestRouteControllerTest()
        {
            _service = new AirlineServiceTest();
            _controller = new ShortestRouteController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<string>(okResult);
        }

        [Fact]
        public async System.Threading.Tasks.Task Get_WhenCalled_ReturnsAllItemsAsync()
        {
            // Act
            var okResult = await _controller.Get("JFK", "YYZ");

            // Assert
            var item = Assert.IsType<string>(okResult);

            var shortestRoute = new List<string>() { "JFK", "YYZ" };
            var expected = String.Join<string>("->", shortestRoute);
             
            Assert.Equal(expected, item);
        }
    }
}
