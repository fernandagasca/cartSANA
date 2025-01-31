namespace ShoppingCartSANA.Aplicacion.Orders.Commands.ProcessOrder
{
    /// <summary>
    /// Crear orden
    /// </summary>
    public class ProcessOrderCommand
    {
        public int CustomerId { get; set; }
        public List<ProcessOrderItemDto> Items { get; set; } = new List<ProcessOrderItemDto>();
    }

    public class ProcessOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
