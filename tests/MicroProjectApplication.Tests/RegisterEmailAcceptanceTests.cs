using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using MicroProjectApplication.Controllers;
using MicroProjectApplication.Data;
using MicroProjectApplication.Models;
using MicroProjectApplication.Repositories;
using MicroProjectApplication.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroProjectApplication.Tests
{
    // Story US-001, service SVC-002.
    // Acceptance obligations (required_before=release, required_environment=ci):
    //   f32a63e9-7181-47b9-8fb4-5fc1b80f8088:test:acceptance:US-001-ac-1
    //   f32a63e9-7181-47b9-8fb4-5fc1b80f8088:test:acceptance:US-001-ac-2
    [TestClass]
    public class RegisterEmailAcceptanceTests
    {
        private AuthController _controller;

        [TestInitialize]
        public void Setup()
        {
            lock (InMemoryDataStore.UsersLock)
            {
                InMemoryDataStore.Users.Clear();
            }
            _controller = new AuthController(new AuthService(new UserRepository()));
        }

        // [US-001-ac-1] / FR-US-001-AC-1 / T-acceptance: unique email creates a user account.
        [TestMethod]
        public void US_001_ac_1_RegisterEmail_UniqueEmail_CreatesUser()
        {
            var result = _controller.RegisterByEmail(new EmailRegistrationRequest { Email = "new.user@example.com" })
                as NegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual("new.user@example.com", result.Content.Email);
            Assert.AreEqual(1, InMemoryDataStore.Users.Count);
            Assert.AreEqual("new.user@example.com", InMemoryDataStore.Users[0].Email);
        }

        // [US-001-ac-2] / FR-US-001-AC-2: existing email returns 409 with an ErrorResponse body.
        [TestMethod]
        public void US_001_ac_2_RegisterEmail_ExistingEmail_Returns409WithErrorResponse()
        {
            _controller.RegisterByEmail(new EmailRegistrationRequest { Email = "dup@example.com" });

            var result = _controller.RegisterByEmail(new EmailRegistrationRequest { Email = "dup@example.com" })
                as NegotiatedContentResult<ErrorResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);
            Assert.IsFalse(string.IsNullOrEmpty(result.Content.Code));
            Assert.IsFalse(string.IsNullOrEmpty(result.Content.Message));
            Assert.AreEqual(1, InMemoryDataStore.Users.Count, "duplicate must not create a second user");
        }

        // [US-001-ac-2] email uniqueness is case-insensitive (placeholder rule, see GAP-03).
        [TestMethod]
        public void US_001_ac_2_RegisterEmail_ExistingEmailDifferentCase_Returns409()
        {
            _controller.RegisterByEmail(new EmailRegistrationRequest { Email = "Case@Example.com" });

            var result = _controller.RegisterByEmail(new EmailRegistrationRequest { Email = "case@example.com" })
                as NegotiatedContentResult<ErrorResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);
        }

        // [US-001-ac-1] invalid/missing email is not accepted (placeholder 400, see GAP-03).
        [TestMethod]
        public void US_001_ac_1_RegisterEmail_InvalidEmail_Returns400AndCreatesNoUser()
        {
            var result = _controller.RegisterByEmail(new EmailRegistrationRequest { Email = "not-an-email" })
                as NegotiatedContentResult<ErrorResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(0, InMemoryDataStore.Users.Count);

            var nullBody = _controller.RegisterByEmail(null) as NegotiatedContentResult<ErrorResponse>;
            Assert.IsNotNull(nullBody);
            Assert.AreEqual(HttpStatusCode.BadRequest, nullBody.StatusCode);
        }
    }
}
