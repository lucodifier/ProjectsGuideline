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

        public async Task<CreatedUserResponse> CreateAsync(CreateUserRequest request)
        {
            var registerUser = _mapper.Map<User>(request);
            var created = await _userRepository.CreateAsync(registerUser);
            return _mapper.Map<CreatedUserResponse>(created);
        }

        public async Task<UserResponse> Get(string login, string pass)
        {
            var user = await _userRepository.GetByLoginAsync(login, pass);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var cacheEntry = _cache.GetOrCreateAsync($"AllUsers", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SetPriority(CacheItemPriority.High);

                return _mapper.Map<IEnumerable<UserResponse>>(await _userRepository.GetAllAsync());
            });

            return await cacheEntry;
        }

        public async Task<IEnumerable<UserResponse>> GetWithDocumentAsync()
        {
            var cacheEntry = _cache.GetOrCreateAsync($"UserWithDocuments", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SetPriority(CacheItemPriority.High);

                return _mapper.Map<IEnumerable<UserResponse>>(await _userRepository.GetWithDocumentsAsync());
            });

            return await cacheEntry;
        }

        public async Task<IEnumerable<UserResponse>> GetByDocumentAsync(string document)
        {
            var cacheEntry = _cache.GetOrCreateAsync($"UserByDocument{document}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SetPriority(CacheItemPriority.High);

                return _mapper.Map<IEnumerable<UserResponse>>(await _userRepository.GetByDocumentAsync(document));
            });

            return await cacheEntry;           
        }

        public async Task<UserResponse> GetByIdAsync(Guid id)
        {
            var cacheEntry = _cache.GetOrCreateAsync($"UserId{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SetPriority(CacheItemPriority.High);

                return _mapper.Map<UserResponse>(await _userRepository.GetByIdAsync(id));
            });

            return await cacheEntry;
        }

        public async Task<CreatedUserResponse> UpdateAsync(UpdateUserRequest request)
        {
            var registerUser = _mapper.Map<User>(request);
            var updated = await _userRepository.UpdateAsync(registerUser);
            return _mapper.Map<CreatedUserResponse>(updated);
        }

        public async Task<Guid> RemoveAsync(Guid id)
        {
            return await _userRepository.RemoveAsync(id);
        }
    }
}
