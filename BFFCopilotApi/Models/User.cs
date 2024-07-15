using BFFCopilotApi.Models;

public class User
{

    public ICollection<Address> Addresses { get; set; }

    public User()
    {
        Addresses = new List<Address>();
    }

    public int UserId { get; set; } // Added UserId property
    public string Name { get; set; }
 
    public string Mobile { get; set; }
}

