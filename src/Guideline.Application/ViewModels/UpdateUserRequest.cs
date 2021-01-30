using System;

namespace Guideline.Application.ViewModels
{
    public class UpdateUserRequest : IResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        
    }
}
