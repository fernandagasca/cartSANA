using ShoppingCartSANA.Domain.Entities;

public class ProductsCategories
{
    public int Product_Id { get; set; }
    public int Category_Id { get; set; }

    // Navegación opcional
    public Product? Product { get; set; }
    public Category? Category { get; set; }
}
