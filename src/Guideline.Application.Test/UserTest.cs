using AutoMapper;
using Guideline.Application.Configuration;
using Guideline.Application.Interfaces;
using Guideline.Application.Services;
using Guideline.Application.Tests;
using Guideline.Application.ViewModels;
using Guideline.Domain.Configuration;
using Guideline.Domain.Entities;
using Guideline.Domain.Interfaces;
using Guideline.Infra.Data.Repository;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace Guideline.Application.Test
{
    public class UserTest
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserTest()
        {
            _configuration = Setting.GetConfiguration();
            _userRepository = new UserRepository(_configuration);

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateUserViewModel, User>();
                config.CreateMap<UpdateUserViewModel, User>();
                config.CreateMap<LoginUserViewModel, User>();
                config.CreateMap<User, UserViewModel>();
            });
            _mapper = mapperConfiguration.CreateMapper();

            _userService = new UserService(_mapper, _userRepository );
        }

        [Fact]
        public void CreateUser()
        {
            var user = new CreateUserViewModel();
            user.Email = "usertest@test.com";
            user.Login = "usertest";
            user.Pass = "usertest";
            user.Name = "usertest";
            var result = _userService.Create(user).Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll()
        {
            var result = _userService.GetAll().Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void GetById()
        {
            var id = new Guid("");
            var result = _userService.GetById(id).Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void Login()
        {
            var login = "usertest";
            var pass = "usertest";
            var result = _userService.Get(login, pass).Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void Update()
        {
            var user = new UpdateUserViewModel();
            user.Id = new Guid("");
            user.Email = "usertest@test.com";
            user.Login = "usertest";
            user.Name = "usertest";
            var result = _userService.Update(user).Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void Remove()
        {
            var id = new Guid("");
            var result = _userService.Remove(id).Result;
            Assert.NotNull(result);
        }
    }
}
