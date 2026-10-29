namespace MicroProjectApplication.Models
{
    // US-001 (FR-US-001-AC-1 / US-001-ac-1): EmailRegistrationRequest.
    // SPEC GAP: only the email is referenced by the spec; no password/name fields are
    // defined. See docs/spec-gaps-US-001.md (GAP-01).
    public class EmailRegistrationRequest
    {
        public string Email { get; set; }
    }
}
