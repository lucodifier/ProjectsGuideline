﻿namespace Guideline.Application.ViewModels
{
    public  class CreateUserRequest : IResponse
    {
        public string Name { get; set; }
      
        public string Login { get; set; }

        public string Email { get; set; }

        public string Document { get; set; }

        public string Pass { get; set; }
    }
}
