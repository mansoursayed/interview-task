using InterviewTask.Application.Contract.Driver;
using InterviewTask.Application.IServices;
using InterviewTask.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTask.Application.Api.Controllers;
[ApiController]
[Route("api/drivers")]
public class DriverController : ControllerBase
{
    private readonly IDriverService _driverService;
    protected readonly ILogger<DriverController> _Logger;
    public DriverController(IDriverService driverService, ILogger<DriverController> logger)
    {
        _driverService = driverService;
        _Logger = logger;
    }

    [HttpGet]
    [Route("getAllDrivers")]
    public async Task<ActionResult<List<DriverDto>>> GetAllDrivers(Constants.SortDirection sortDirection)
    {
        _Logger.LogInformation("Get All Drivers");

        var drivers = await _driverService.GetAllAsync(sortDirection);

        return Ok(drivers);
    }

    [HttpGet]
    [Route("getDriverById/{id}")]
    public async Task<ActionResult<DriverDto>> GetDriverById(int id, Constants.SortDirection sortDirection)
    {
        var driver = await _driverService.GetByIdAsync(id, sortDirection);

        if (driver == null)
        {
            _Logger.LogInformation($"Driver doesn't exist for Id{id}");
            return Ok("Driver doesn't exist");
        }
        return Ok(driver);
    }

    [HttpPost]
    [Route("createDriver")]
    public async Task<IActionResult> CreateDriver([FromBody] CreateDriverDto model)
    {
        await _driverService.CreateAsync(model);

        return Ok();
    }

    [HttpPut]
    [Route("updateDriver")]
    public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverDto model)
    {
        await _driverService.UpdateAsync(model);

        return Ok();
    }

    [HttpDelete]
    [Route("deleteDriver/{id}")]
    public async Task<IActionResult> DeleteDriver(int id)
    {
        await _driverService.DeleteAsync(id);

        return Ok();
    }


    [HttpPost]
    [Route("createDriversRandomly")]
    public async Task<IActionResult> CreateDriversRandomly()
    {

        await _driverService.BulkCreateAsync(10);// 10 Drivers randomly;

        return Ok();
    }

}