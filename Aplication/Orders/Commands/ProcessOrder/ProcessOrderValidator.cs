using FluentValidation;

namespace ShoppingCartSANA.Aplicacion.Orders.Commands.ProcessOrder
{
    public class ProcessOrderValidator : AbstractValidator<ProcessOrderCommand>
    {
        public ProcessOrderValidator()
        {
            // CustomerId debe ser > 0
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("Debe especificar un CustomerId válido.");

            // Al menos un ítem en la lista
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("La orden debe tener al menos un item.");

            // Todos los ProductId deben ser > 0
            RuleFor(x => x.Items)
                .Must(items => items.All(i => i.ProductId > 0))
                .WithMessage("Todos los productos deben tener un ProductId mayor a 0.");

            // Todas las cantidades deben ser > 0
            RuleFor(x => x.Items)
                .Must(items => items.All(i => i.Quantity > 0))
                .WithMessage("Cada ítem debe tener una cantidad mayor a 0.");
        }
    }
}
