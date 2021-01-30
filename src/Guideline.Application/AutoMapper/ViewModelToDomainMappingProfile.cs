using AutoMapper;
using Guideline.Application.ViewModels;
using Guideline.Domain.Entities;

namespace Guideline.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<UserResponse, User>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}
