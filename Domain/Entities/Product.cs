using ShoppingCartSANA.Domain.Entities;
using System.Text.Json.Serialization;

public class Product
{
    public int Id { get; set; }
    public string Name_Product { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? Image { get; set; }

    // Relaciones opcionales

    [JsonIgnore]
    public ICollection<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();

    [JsonIgnore]
    public ICollection<ProductsCategories>? ProductsCategories { get; set; } = new List<ProductsCategories>();
}
