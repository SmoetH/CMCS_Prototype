namespace CMCS_Prototype.Models
{
    // This class represents a single claim in our database.
    public class Claim
    {
        // The unique identifier for each claim.
        public int Id { get; set; }

        // The name of the lecturer submitting the claim.
        public string? LecturerName { get; set; }

        // The month for which the claim is being submitted.
        public string? ClaimMonth { get; set; }

        // The number of hours worked.
        public int Hours { get; set; }

        // The status of the claim (e.g., Pending, Approved, Rejected).
        // We'll use a simple string for now.
        public string Status { get; set; } = "Pending";
    }
}
