using AutoMapper;
using SharedModels.Entities.Admin;
using SharedModels.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.MapperProfiles.Admin
{
    public class AdminProfile:Profile
    {
        public AdminProfile()
        {
            CreateMap<NavBar, NavBarVM>();
            CreateMap<SP_GetMenu_Result, NavBarVM>();
        }
    }
}
