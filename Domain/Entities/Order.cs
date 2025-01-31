namespace ShoppingCartSANA.Domain.Entities
{
    public class Order
    {
        public int Id_Order { get; set; }        // PK Identity
        public int Customer_Id { get; set; }     // FK a Customers(Id)
        public DateTime Date_Order { get; set; } // Fecha/hora de la orden

        // Navegación (opcional)
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();
    }

}
