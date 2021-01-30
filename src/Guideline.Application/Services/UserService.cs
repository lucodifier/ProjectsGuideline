using AutoMapper;
using FluentValidation.Results;
using Guideline.Application.Interfaces;
using Guideline.Application.Validation;
using Guideline.Application.ViewModels;
using Guideline.Domain.Entities;
using Guideline.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guideline.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _cache;

        public UserService(IMapper mapper,
            IMemoryCache cache,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _cache = cache;
            _userRepository = userRepository;
        }

        public async Task<CreatedUserViewModel> Create(CreateUserViewModel createUserViewModel)
        {
            var registerUser = _mapper.Map<User>(createUserViewModel);
            var created = await _userRepository.Add(registerUser);
            return _mapper.Map<CreatedUserViewModel>(created);
        }

        public async Task<UserViewModel> Get(string login, string pass)
        {
            var user = await _userRepository.Get(login, pass);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetAll());
        }

        public async Task<IEnumerable<UserViewModel>> GetWithDocument()
        {
            var cacheEntry = _cache.GetOrCreateAsync($"UserWithDocuments", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SetPriority(CacheItemPriority.High);

                return _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetWithDocuments());
            });

            return await cacheEntry;
        }

        public async Task<IEnumerable<UserViewModel>> GetByDocument(string document)
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetByDocument(document));
        }

        public async Task<UserViewModel> GetById(Guid id)
        {
            var cacheEntry = _cache.GetOrCreateAsync($"UserId{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SetPriority(CacheItemPriority.High);

                return _mapper.Map<UserViewModel>(await _userRepository.GetById(id));
            });

            return await cacheEntry;
        }

        public async Task<ValidationResultViewModel> Update(UpdateUserViewModel updateUserViewModel)
        {
            var validator = new UpdateUserValidation();
            var result = await validator.ValidateAsync(updateUserViewModel);
            var validationResult = new ValidationResultViewModel(result);
            if (result.IsValid)
            {
                var registerUser = _mapper.Map<User>(updateUserViewModel);
                var updated = await _userRepository.Update(registerUser);
                validationResult.Id = updated.Id;
            }

            return validationResult;
        }

        public async Task<Guid> Remove(Guid id)
        {
            return await _userRepository.Remove(id);
        }
    }
}
