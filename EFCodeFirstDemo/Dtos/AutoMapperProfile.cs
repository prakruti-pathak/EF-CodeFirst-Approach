using AutoMapper;
using EFCodeFirstDemo.Models;

namespace EFCodeFirstDemo.Dtos
{
    /// <summary>
    /// install nuget package automapper of latest version
    /// create class of automapper inherit profile 
    /// in service use Imapper and remove comparing and use mapper
    /// register automapper in program.cs
    /// </summary>
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<Employee, EDto>()
           .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
           .ReverseMap();
        }
    }
}
