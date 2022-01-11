using AutoMapper;
using Comparly.Data.Dtos;
using Comparly.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Profiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AddAppUserDto>().ReverseMap();
            CreateMap<Submission, SubmissionToReturnDto>().ReverseMap();
            CreateMap<Submission, ComparisonHistoryDto>().ReverseMap();
            CreateMap<Submission, ComparisonHistoryDetailsDto>().ReverseMap();
        }
    }
}
