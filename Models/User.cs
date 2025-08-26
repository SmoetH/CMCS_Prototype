namespace CMCS_Prototype.Models
{
    // This class represents a user in our database.
    public class User
    {
        // The unique identifier for each user.
        public int Id { get; set; }

        // The user's login username.
        public string? Username { get; set; }

        // The user's password. In a real app, this would be a hashed password, not plain text.
        public string? Password { get; set; }
    }
}
