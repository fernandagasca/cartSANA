using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartSANA.Domain.Entities
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public int Order_Id { get; set; }
        public int Quantity { get; set; }

        // Propiedades de navegación

        [ForeignKey("Order_Id")]
        public Order Order { get; set; }


        [ForeignKey("Product_Id")]
        public Product Product { get; set; }
    }
}
