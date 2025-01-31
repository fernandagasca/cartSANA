using Microsoft.EntityFrameworkCore;
using ShoppingCartSANA.Infrastructure.Persistence;
using ShoppingCartSANA.Infrastructure.Repositories;
using ShoppingCartSANA.Domain.Interfaces;
using ShoppingCartSANA.Aplicacion.Orders.Commands.ProcessOrder;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProcessOrderCommandHandler>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseCors("AllowReactApp"); 
app.UseAuthorization();


app.MapControllers();

app.Run();
