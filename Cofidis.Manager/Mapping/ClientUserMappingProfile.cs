using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cofidis.Data.Models;
using Cofidis.Data.Models.External;

namespace Cofidis.Manager.Mapping
{
    public class ClientUserMappingProfile : Profile
    {

        public ClientUserMappingProfile() 
        {
            CreateMap<ClientUser, User>();
        }
    }
}
