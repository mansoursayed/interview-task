using InterviewTask.Application.Api.Controllers;
using InterviewTask.Application.Contract.Driver;
using InterviewTask.Application.IServices;
using InterviewTask.Core.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InterviewTask.Test.Api;
public class DriverControllerTests
{
    private readonly Mock<IDriverService> _driverServiceMock;
    private readonly DriverController _driverController;
    private readonly Mock<ILogger<DriverController>> _loggerMock;

    public DriverControllerTests()
    {
        _driverServiceMock = new Mock<IDriverService>();
        _loggerMock = new Mock<ILogger<DriverController>>();
        _driverController = new DriverController(_driverServiceMock.Object, _loggerMock.Object);
    }


    [Fact]
    public async Task GetAllDrivers_WithOrWithoutSortdirection_ShouldReturnOkResult()
    {
        //Arrange
        _driverServiceMock.Setup(d => d.GetAllAsync(It.IsAny<Constants.SortDirection>())).ReturnsAsync(new List<DriverDto>());
        //Act
        var result = await _driverController.GetAllDrivers(It.IsAny<Constants.SortDirection>());
        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetDriverById_WhenExistedDriverId_ShouldReturnTheDriver()
    {
        //Arrange
        _driverServiceMock.Setup(d => d.GetByIdAsync(It.IsAny<int>(), It.IsAny<Constants.SortDirection>())).ReturnsAsync(new DriverDto());
        //Act
        var result = await _driverController.GetDriverById(It.IsAny<int>(), It.IsAny<Constants.SortDirection>());
        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetDriverById_WhenNonExistedDriverId_ShouldReturnMessageForNonExistance()
    {
        //Arrange
        _driverServiceMock.Setup(d => d.GetByIdAsync(It.IsAny<int>(), It.IsAny<Constants.SortDirection>())).ReturnsAsync(() => null);
        //Act
        var result = await _driverController.GetDriverById(It.IsAny<int>(), It.IsAny<Constants.SortDirection>());
        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateDriver_WithValidDriverModel_ShouldReturnOkResult()
    {
        //Arrange
        _driverServiceMock.Setup(d => d.CreateAsync(It.IsAny<CreateDriverDto>())).ReturnsAsync(1);
        //Act
        var result = await _driverController.CreateDriver(It.IsAny<CreateDriverDto>());
        //Assert
        Assert.IsType<OkResult>(result);
    }


    [Fact]
    public async Task DeleteDriver_WithValidId_ShouldReturnOkResult()
    {
        //Arrange
        _driverServiceMock.Setup(d => d.DeleteAsync(It.IsAny<int>())).ReturnsAsync(1);
        //Act
        var result = await _driverController.DeleteDriver(It.IsAny<int>());
        //Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task CreateDriversRandomly_ShouldReturnOkResult()
    {
        //Arrange
        _driverServiceMock.Setup(d => d.BulkCreateAsync(It.IsAny<int>())).ReturnsAsync(1);
        //Act
        var result = await _driverController.CreateDriversRandomly();
        //Assert
        Assert.IsType<OkResult>(result);
    }
}