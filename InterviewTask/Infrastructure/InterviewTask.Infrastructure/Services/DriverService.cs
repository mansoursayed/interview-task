using AutoMapper;
using InterviewTask.Application.Contract.Driver;
using InterviewTask.Application.IRepositories;
using InterviewTask.Application.IServices;
using InterviewTask.Core.Constants;
using InterviewTask.Core.Entities;

namespace InterviewTask.Infrastructure.Services;
public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IMapper _mapper;
    public DriverService(IDriverRepository driverRepository, IMapper mapper)
    {
        _driverRepository = driverRepository;
        _mapper = mapper;
    }

    public async Task<int> BulkCreateAsync(int length)
    {
        List<CreateDriverDto> driversDto = new List<CreateDriverDto>();
        for (int i = 0; i < length; i++)
        {
            var driver = new CreateDriverDto()
            {
                FirstName =Helper.Helper.RandomString(10),// random string with 5 charchters
                LastName = Helper.Helper.RandomString(10)// random string with 5 charchters
            };

            driversDto.Add(driver);
        }
        var drivers = _mapper.Map<List<Driver>>(driversDto);
        return await _driverRepository.BulkInsertAsync(drivers);
    }

    public async Task<int> CreateAsync(CreateDriverDto driver)
    {
        var driverEntity = _mapper.Map<Driver>(driver);
        return await _driverRepository.InsertAsync(driverEntity);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _driverRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<DriverDto>> GetAllAsync(Constants.SortDirection sortDirection)
    {
        var drivers = await _driverRepository.GetAllAsync(sortDirection);
        var driversDto = _mapper.Map<IEnumerable<DriverDto>>(drivers);
        return driversDto;
    }

    public async Task<DriverDto> GetByIdAsync(int id, Constants.SortDirection sortDirection)
    {
        var driver = await _driverRepository.GetByIdAsync(id, sortDirection);
        var driverDto = _mapper.Map<DriverDto>(driver);
        return driverDto;

    }

    public async Task<int> UpdateAsync(UpdateDriverDto driver)
    {
        var driverEntity = _mapper.Map<Driver>(driver);
        return await _driverRepository.UpdateAsync(driverEntity);
    }
}