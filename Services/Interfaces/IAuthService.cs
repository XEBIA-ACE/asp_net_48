using MicroProjectApplication.Models;

namespace MicroProjectApplication.Services.Interfaces
{
    public enum RegistrationStatus
    {
        Created,
        InvalidRequest,
        EmailAlreadyExists
    }

    public class RegistrationResult
    {
        public RegistrationStatus Status { get; set; }
        public User User { get; set; }
    }

    public interface IAuthService
    {
        RegistrationResult RegisterByEmail(EmailRegistrationRequest request);
    }
}
