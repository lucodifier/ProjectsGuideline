using AutoMapper;
using FluentValidation.Results;
using Guideline.Application.Interfaces;
using Guideline.Application.Validation;
using Guideline.Application.ViewModels;
using Guideline.Domain.Entities;
using Guideline.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guideline.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ValidationResultViewModel> Create(CreateUserViewModel createUserViewModel)
        {
            var validator = new CreateUserValidation();
            var result = await validator.ValidateAsync(createUserViewModel);
            var validationResult = new ValidationResultViewModel(result);
            if (result.IsValid)
            {
                var registerUser = _mapper.Map<User>(createUserViewModel);
                var created = await _userRepository.Add(registerUser);
                validationResult.Id = created.Id;
            }

            return validationResult;
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
            return _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetWithDocuments());
        }

        public async Task<IEnumerable<UserViewModel>> GetByDocument(string document)
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetByDocument(document));
        }

        public async Task<UserViewModel> GetById(Guid id)
        {
            return _mapper.Map<UserViewModel>(await _userRepository.GetById(id));
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
