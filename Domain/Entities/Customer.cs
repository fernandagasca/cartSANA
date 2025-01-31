namespace ShoppingCartSANA.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name_Customer { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
