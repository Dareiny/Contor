using AutoMapper;
using Contracts.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentRegistrar.MapProfiles
{
    public class DateProfile : Profile
    {
        public DateProfile()
        {
            CreateMap<Domain.Dates, DatesDto>();
            CreateMap<DatesDto, Domain.Dates>();
        }
    }
}
