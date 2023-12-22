using AutoMapper;
using InterviewTask.Application.Contract.Driver;
using InterviewTask.Application.IRepositories;
using InterviewTask.Application.IServices;
using InterviewTask.Core.Constants;
using InterviewTask.Core.Entities;
using InterviewTask.Infrastructure.Services;
using Moq;
using Xunit;

namespace InterviewTask.Test.Services;
public class DriverServiceTests
{
    private readonly Mock<IDriverRepository> _driverRepositoryMock;
    private readonly IDriverService _driverService;
    private readonly Mock<IMapper> _mapperMock;

    public DriverServiceTests()
    {
        _driverRepositoryMock = new Mock<IDriverRepository>();
        _mapperMock = new Mock<IMapper>();
        _driverService = new DriverService(_driverRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task BulkCreateAsync_WithLengthEqualsZero_ShouldNotInsertSuccessfully()
    {
        //Arrange
        _driverRepositoryMock.Setup(d => d.BulkInsertAsync(It.IsAny<List<Driver>>())).ReturnsAsync(0);
        //Act
        var result = await _driverService.BulkCreateAsync(It.IsAny<int>());
        //Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task BulkCreateAsync_WithLengthGreaterThanZero_ShouldInsertSuccessfully()
    {
        //Arrange
        _driverRepositoryMock.Setup(d => d.BulkInsertAsync(It.IsAny<List<Driver>>())).ReturnsAsync(1);
        //Act
        var result = await _driverService.BulkCreateAsync(It.IsAny<int>());
        //Assert
        Assert.Equal(1, result);
    }


    [Fact]
    public async Task CreateAsync_WithInvalidDriver_ShouldNotInsertSuccessfully()
    {
        //Arrange
        _driverRepositoryMock.Setup(d => d.InsertAsync(It.IsAny<Driver>())).ReturnsAsync(0);
        //Act
        var result = await _driverService.CreateAsync(It.IsAny<CreateDriverDto>());
        //Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task CreateAsync_WithValidDriver_ShouldInsertSuccessfully()
    {
        //Arrange
        _driverRepositoryMock.Setup(d => d.InsertAsync(It.IsAny<Driver>())).ReturnsAsync(1);
        //Act
        var result = await _driverService.CreateAsync(It.IsAny<CreateDriverDto>());
        //Assert
        Assert.Equal(1, result);
    }


    [Fact]
    public async Task DeleteAsync_WithInvalidId_ShouldNotDeleteSuccessfully()
    {
        //Arrange
        _driverRepositoryMock.Setup(d => d.DeleteAsync(It.IsAny<int>())).ReturnsAsync(0);
        //Act
        var result = await _driverService.DeleteAsync(It.IsAny<int>());
        //Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldDeleteSuccessfully()
    {
        //Arrange
        _driverRepositoryMock.Setup(d => d.DeleteAsync(It.IsAny<int>())).ReturnsAsync(1);
        //Act
        var result = await _driverService.DeleteAsync(It.IsAny<int>());
        //Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetAllAsync_WithOrWithoutSortDirection_ShouldReturnNonNullableList()
    {
        //Arrange
        List<Driver> drivers = new List<Driver>();
        _driverRepositoryMock.Setup(d => d.GetAllAsync(It.IsAny<Constants.SortDirection>())).ReturnsAsync(drivers);
        _mapperMock.Setup(m => m.Map<List<DriverDto>>(drivers)).Returns(new List<DriverDto>());
        //Act
        var result = await _driverService.GetAllAsync(It.IsAny<Constants.SortDirection>());
        //Assert
        Assert.NotNull(result);
    }


    [Fact]
    public async Task GetByIdAsync_WithInvalidIdAndWithOrWithoutSortDirection_ShouldReturnNullableObject()
    {
        //Arrange
        _driverRepositoryMock.Setup(d => d.GetByIdAsync(It.IsAny<int>(), It.IsAny<Constants.SortDirection>())).ReturnsAsync(() => null);
        _mapperMock.Setup(m => m.Map<It.IsAnyType>(It.IsAny<It.IsAnyType>())).Returns(It.IsAny<It.IsAnyType>());
        //Act
        var result = await _driverService.GetByIdAsync(It.IsAny<int>(), It.IsAny<Constants.SortDirection>());
        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidIdAndWithOrWithoutSortDirection_ShouldReturnNonNullableObject()
    {
        //Arrange
        Driver driver = new Driver();
        _driverRepositoryMock.Setup(d => d.GetByIdAsync(It.IsAny<int>(), It.IsAny<Constants.SortDirection>())).ReturnsAsync(driver);
        _mapperMock.Setup(m => m.Map<DriverDto>(driver)).Returns(new DriverDto());
        //Act
        var result = await _driverService.GetByIdAsync(It.IsAny<int>(), It.IsAny<Constants.SortDirection>());
        //Assert
        Assert.NotNull(result);
    }


    [Fact]
    public async Task UpdateAsync_WithInvalidDriver_ShouldNotUpdateSuccessfully()
    {
        //Arrange
        _driverRepositoryMock.Setup(d => d.UpdateAsync(It.IsAny<Driver>())).ReturnsAsync(0);
        //Act
        var result = await _driverService.UpdateAsync(It.IsAny<UpdateDriverDto>());
        //Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task UpdateAsync_WithValidDriver_ShouldUpdateSuccessfully()
    {
        //Arrange
        _driverRepositoryMock.Setup(d => d.UpdateAsync(It.IsAny<Driver>())).ReturnsAsync(1);
        //Act
        var result = await _driverService.UpdateAsync(It.IsAny<UpdateDriverDto>());
        //Assert
        Assert.Equal(1, result);
    }
}