using System;

namespace Guideline.Domain.Entities.Base
{
    public class BaseEntity<T> where T : BaseEntity<T>
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }

        public BaseEntity()
        {
            if (Id == new Guid())
                Id = Guid.NewGuid();
            
            Created = DateTimeOffset.Now;
        }
    }
}
