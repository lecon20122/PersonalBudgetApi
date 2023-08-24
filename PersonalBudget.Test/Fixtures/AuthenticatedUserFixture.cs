using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using PersonalBudget.Models;
using System.Security.Claims;

namespace PersonalBudget.Test.Fixtures
{
    public class AuthenticatedUserFixture
    {
        public Mock<UserManager<ApplicationUser>> MockUserManager { get; }

        public Mock<IHttpContextAccessor> MockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        public AuthenticatedUserFixture()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();

            MockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            MockUserManager.Setup
                (m => m.GetUserId(It.IsAny<ClaimsPrincipal>()))
                    .Returns("1");

            MockHttpContextAccessor.Setup
                (m => m.HttpContext.User)
                    .Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, "1"),
                        new Claim(ClaimTypes.Name, "Mustafa"),
                    }, "mock")));
        }
    }
}
