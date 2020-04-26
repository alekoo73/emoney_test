using AutoMapper;
using Core.DTO;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MappingProfiles
{
    public class AccountMapping : Profile
    {
        public AccountMapping()
        {
            CreateMap<Account, FindAccountResponse>();
        }
    }
}
