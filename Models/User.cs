using System;

namespace MicroProjectApplication.Models
{
    // US-001 (FR-US-001-AC-1): user account created by email registration.
    // SPEC GAP: no user fields are specified beyond the email. See docs/spec-gaps-US-001.md (GAP-01).
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
