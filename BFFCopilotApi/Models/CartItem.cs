using BFFCopilotApi.Models;

public class CartItem
{
    public int CartItemId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; } // Foreign key to Product
    public int Quantity { get; set; } // Quantity of the Product

    // Navigation property to Product
    public Product Product { get; set; }
}

