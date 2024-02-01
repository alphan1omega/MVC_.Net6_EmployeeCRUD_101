using AutoMapper;
using EmployeeCRUD.AppData;
using EmployeeCRUD.Models;

namespace EmployeeCRUD.Helper
{
    public class AutoMapperProf : Profile
    {
        public AutoMapperProf()
        {
            CreateMap<Employee, EmpViewModel>();
            CreateMap<EmpViewModel, Employee>();
        }
    }
}
