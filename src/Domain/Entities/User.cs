using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [ForeignKey(nameof(Organization))]
        public int? OrganizationId { get; set; }
        public virtual Organization? Organization { get; set; }
    }
}