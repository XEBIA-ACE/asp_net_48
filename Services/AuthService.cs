using MicroProjectApplication.Models;
using MicroProjectApplication.Repositories.Interfaces;
using MicroProjectApplication.Services.Interfaces;
using System;
using System.Net.Mail;

namespace MicroProjectApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // US-001 / FR-US-001-AC-1, FR-US-001-AC-2 (US-001-ac-1, US-001-ac-2)
        public RegistrationResult RegisterByEmail(EmailRegistrationRequest request)
        {
            var email = request?.Email?.Trim();
            if (!IsValidEmail(email))
                return new RegistrationResult { Status = RegistrationStatus.InvalidRequest };

            var user = _userRepository.AddIfEmailUnique(email);
            if (user == null)
                return new RegistrationResult { Status = RegistrationStatus.EmailAlreadyExists };

            return new RegistrationResult { Status = RegistrationStatus.Created, User = user };
        }

        // SPEC GAP (GAP-03): no email validation rules are specified; this minimal syntactic
        // check is a temporary placeholder.
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            try
            {
                var parsed = new MailAddress(email);
                return string.Equals(parsed.Address, email, StringComparison.Ordinal);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
