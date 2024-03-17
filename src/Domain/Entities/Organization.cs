using System.Collections.Generic;

namespace Domain.Entities
{
    public class Organization : BaseEntity
    {
        public string Name { get; set; }

        public IList<User> Users { get; set; }
    }
}