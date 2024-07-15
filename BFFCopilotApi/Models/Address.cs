namespace BFFCopilotApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        // Foreign key for User
        public int UserId { get; set; } // Previously UserId

        // Navigation property back to User
       // public User User { get; set; } // Previously User
    }

}
