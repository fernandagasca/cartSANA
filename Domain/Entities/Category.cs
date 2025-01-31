namespace ShoppingCartSANA.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name_Category { get; set; }

        // Relación con Products a través de ProductsCategories
        public ICollection<ProductsCategories> ProductsCategories { get; set; }
    }
}
