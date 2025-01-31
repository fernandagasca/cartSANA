using Microsoft.AspNetCore.Mvc;
using ShoppingCartSANA.Aplicacion.Orders.Commands.ProcessOrder;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ProcessOrderCommandHandler _handler;

    public OrdersController(ProcessOrderCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost("ProcessOrder")]
    public async Task<IActionResult> ProcessOrder([FromBody] ProcessOrderCommand command)
    {
        try
        {
            int orderId = await _handler.ProcessAsync(command);
            return Ok(new { Message = "Orden creada", OrderId = orderId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
