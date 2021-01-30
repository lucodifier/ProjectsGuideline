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
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IMemoryCache _cache;

        public UserTest()
        {
            _configuration = Setting.GetConfiguration();
            _userRepository = new UserRepository(_configuration);

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateUserRequest, User>();
                config.CreateMap<UpdateUserRequest, User>();
                config.CreateMap<User, UserResponse>();
            });
            _mapper = mapperConfiguration.CreateMapper();

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();

            _userService = new UserService(_mapper, memoryCache, _userRepository);
        }

        [Fact]
        public void CreateUser()
        {
            var user = new CreateUserRequest();
            user.Email = "usertest@test.com";
            user.Login = "usertest";
            user.Pass = "usertest";
            user.Name = "usertest";
            var result = _userService.CreateAsync(user).Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll()
        {
            var result = _userService.GetAllAsync().Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void GetById()
        {
            var id = new Guid("DFCFC6BD-D0D6-4D52-BA68-BF1C5DA1B3DF");
            var result = _userService.GetByIdAsync(id).Result;
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
            var user = new UpdateUserRequest();
            user.Id = new Guid("");
            user.Email = "usertest@test.com";
            user.Login = "usertest";
            user.Name = "usertest";
            var result = _userService.UpdateAsync(user).Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void Remove()
        {
            var id = new Guid("");
            var result = _userService.RemoveAsync(id).Result;
            Assert.NotNull(result);
        }
    }
}
