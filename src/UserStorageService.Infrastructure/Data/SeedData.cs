using Domain.Entities;

namespace UserStorageService.Infrastructure.Data
{
    public static class SeedData
    {
        public static Organization[] GetDefaultOrganizations()
        {
            return new[]
            {
                new Organization()
                {
                    Id = 1,
                    Name = "Google"
                },
                new Organization()
                {
                    Id = 2,
                    Name = "MegaCorp"
                },
                new Organization()
                {
                    Id = 3,
                    Name = "Amazon"
                }
            };
        }

        public static User[] GetDefaultUsers()
        {
            return new[]
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    PhoneNumber = "79999999999",
                    Email = "test@email.com",
                    OrganizationId = 2
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Петр",
                    LastName = "Петров",
                    MiddleName = "Петрович",
                    PhoneNumber = "71111111111",
                    Email = "my@testemail.com"
                },
            };
        }
    }
}