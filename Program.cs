using echo_store_api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UsePathBase("/swagger");

var cartService = new CartService();

app.MapGet("/", () => "It's running!");

app.MapGet("get-cart-itens", () => Results.Ok(cartService.Get()));

app.MapDelete("delete-cart-item/{productId}", (int productId) =>
    Results.Accepted("delete-cart-item", cartService.Remove(productId)));

app.MapPost("/add-to-cart", (Product product) =>
{
    cartService.Add(product);
    return Results.Created("/add-to-cart", product);
});

app.Run();
