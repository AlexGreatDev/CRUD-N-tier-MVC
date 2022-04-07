using AutoMapper;
using Core.Common.DTOs.Employee;
using SimpleCRUDExample.Models.Employee;

namespace SimpleCRUDExample.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, EmployeeDto>();
        }
    }
}
