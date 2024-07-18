namespace BFFCopilotApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }

        public decimal Price { get; set; } // Added Price property

        public int Stock { get; set; } // Added Stock property

    }
}
