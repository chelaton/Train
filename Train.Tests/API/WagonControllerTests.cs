using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Train.API.Controllers;
using Train.Core.Interfaces;
using Train.Core.Models;
using Xunit;

namespace Train.Tests.API
{
    public class WagonControllerTests
    {
        [Fact]
        public async void GetWagon_ReturnsNotFoundResult_WhenWagonIsNotFound()
        {
            // Arrange
            var wagonService = Substitute.For<IWagonService>();
            var wagonController = new WagonController(wagonService);

            var wagonId = 1;
            wagonService.GetWagonAsync(wagonId).Returns(Task.FromResult<WagonModel>(null));

            // Act
            var result = await wagonController.GetWagonAsync(wagonId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetWagon_ReturnsOkResult_WhenWagonIsFound()
        {
            // Arrange
            var wagonService = Substitute.For<IWagonService>();
            var wagonController = new WagonController(wagonService);

            var wagon = new WagonModel
            {
                WagonId = 1,
                NumberOfChairs =2,
                Chairs = null
            };

            wagonService.GetWagonAsync(wagon.WagonId).Returns(Task.FromResult<WagonModel>(wagon));

            // Act
            var result = await wagonController.GetWagonAsync(wagon.WagonId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
