using AutoMapper;
using Guideline.Application.ViewModels;
using Guideline.Domain.Entities;

namespace Guideline.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
