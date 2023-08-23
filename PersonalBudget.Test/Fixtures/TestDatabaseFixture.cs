using Microsoft.EntityFrameworkCore;
using PersonalBudget.DataAccess;
using PersonalBudget.Models;

namespace PersonalBudget.Test.Fixtures
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=EFTestSample;Trusted_Connection=True";
        private static readonly object _lock = new();
        private static bool _databaseInitialized;


        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        // seed user to database

                        var user = new ApplicationUser
                        {
                            UserName = "Mustafa",
                            NormalizedUserName = "MUSTAFA",
                            Email = "mustafa@gmail.com",
                            NormalizedEmail = "",
                            PasswordHash = "12345",
                            SecurityStamp = "12345",
                            ConcurrencyStamp = "12345",
                            PhoneNumber = "12345",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            LockoutEnabled = false,
                            AccessFailedCount = 0
                        };

                        context.Users.Add(user);
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public ApplicationDbContext CreateContext()
            => new ApplicationDbContext(
                    new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseSqlServer(ConnectionString).Options);
    }
}
