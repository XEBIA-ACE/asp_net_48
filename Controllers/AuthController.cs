using MicroProjectApplication.Models;
using MicroProjectApplication.Services.Interfaces;
using System.Net;
using System.Web.Http;

namespace MicroProjectApplication.Controllers
{
    // US-001 / SVC-002
    [RoutePrefix("api/v1/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // FR-US-001-AC-1 (US-001-ac-1): create a user when the email is unique.
        // FR-US-001-AC-2 (US-001-ac-2): 409 Conflict + ErrorResponse when the email exists.
        [HttpPost]
        [Route("register/email")]
        public IHttpActionResult RegisterByEmail(EmailRegistrationRequest request)
        {
            var result = _authService.RegisterByEmail(request);
            switch (result.Status)
            {
                case RegistrationStatus.Created:
                    // SPEC GAP (GAP-04): success status/body not specified; 201 + created user is a placeholder.
                    return Content(HttpStatusCode.Created, result.User);
                case RegistrationStatus.EmailAlreadyExists:
                    return Content(HttpStatusCode.Conflict, new ErrorResponse
                    {
                        Code = "email_already_exists",
                        Message = "A user with this email already exists."
                    });
                default:
                    return Content(HttpStatusCode.BadRequest, new ErrorResponse
                    {
                        Code = "invalid_request",
                        Message = "A valid email is required."
                    });
            }
        }
    }
}
