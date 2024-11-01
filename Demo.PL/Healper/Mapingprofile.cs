using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.View_Models;

namespace Demo.PL.Healper
{
    public class Mapingprofile : Profile
    {
        public Mapingprofile()
        {
            CreateMap<EployeeViewModel,Employees>().ReverseMap();
           
        }
    }
}
