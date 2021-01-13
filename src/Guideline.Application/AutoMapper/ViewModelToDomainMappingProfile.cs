using AutoMapper;
using Guideline.Application.ViewModels;
using Guideline.Domain.Entities;

namespace Guideline.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CreateUserViewModel, User>();
            CreateMap<UserViewModel, User>();
            CreateMap<UpdateUserViewModel, User>();
        }
    }
}
