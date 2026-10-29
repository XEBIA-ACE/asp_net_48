namespace MicroProjectApplication.Models
{
    // US-001 (FR-US-001-AC-2 / US-001-ac-2): ErrorResponse.
    // SPEC GAP: field-level schema is not defined in the spec; Code + Message is a
    // temporary placeholder. See docs/spec-gaps-US-001.md (GAP-02).
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
