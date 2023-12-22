using AutoMapper;
using InterviewTask.Application.Contract.Driver;
using InterviewTask.Core.Entities;

namespace InterviewTask.Application.MapperProfile;
public class DriverProfile : Profile
{
    public DriverProfile()
    {
        CreateMap<DriverDto, Driver>().ReverseMap();
        CreateMap<CreateDriverDto, Driver>();
        CreateMap<UpdateDriverDto, Driver>();
    }
}