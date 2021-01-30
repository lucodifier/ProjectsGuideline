using System;

namespace Guideline.Application.ViewModels
{
    public class CreatedUserResponse : ICreatedResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
    }
}
