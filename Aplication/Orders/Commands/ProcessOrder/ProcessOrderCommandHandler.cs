using ShoppingCartSANA.Domain.Entities;
using ShoppingCartSANA.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCartSANA.Aplicacion.Orders.Commands.ProcessOrder
{
        public class ProcessOrderCommandHandler
    {
        private readonly AppDbContext _context;

        public ProcessOrderCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Procesa la orden y devuelve el Id de la orden creada.
        /// </summary>
        public async Task<int> ProcessAsync(ProcessOrderCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                // Validar que la solicitud contiene ítems
                if (request.Items == null || !request.Items.Any())
                    throw new Exception("La orden debe contener al menos un producto.");

                // 1) Verificar cliente
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);
                if (customer == null)
                    throw new Exception($"No existe el cliente con Id={request.CustomerId}.");

                // 2) Crear la orden y guardarla
                var newOrder = new Order
                {
                    Customer_Id = request.CustomerId,
                    Date_Order = DateTime.Now
                };
                _context.Orders.Add(newOrder);
                //Guarda la orden
                await _context.SaveChangesAsync(cancellationToken);

                
                var productIds = request.Items.Select(x => x.ProductId).ToList();
                var products = await _context.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToListAsync(cancellationToken);

                
                foreach (var item in request.Items)
                {
                    var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                    if (product == null)
                        throw new Exception($"No existe un producto con Id={item.ProductId}");

                    if (item.Quantity > product.Stock)
                        throw new Exception($"Stock insuficiente para '{product.Name_Product}'.");

                    // Reducir stock
                    product.Stock -= item.Quantity;

                    var detail = new OrderDetail
                    {
                        Order_Id = newOrder.Id_Order, 
                        Product_Id = product.Id,
                        Quantity = item.Quantity
                    };
                    _context.OrderDetails.Add(detail);
                }

                // 5) Guardar todos los cambios en una sola transacción
                await _context.SaveChangesAsync(cancellationToken);

                return newOrder.Id_Order;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error al guardar en la base de datos: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado: {ex.Message}");
            }
        }
    }
}
