using System;
using Guideline.Domain.Entities.Base;

namespace Guideline.Domain.Entities
{
    public class User : BaseEntity<User>
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
    }
}
