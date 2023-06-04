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

#region Cart

app.MapGet("/cart/get-cart-itens", () => Results.Ok(cartService.GetCartItens()));

app.MapDelete("/cart/delete-cart-item/{productId}", (int productId) =>
    Results.Accepted("/cart/delete-cart-item", cartService.RemoveCartItem(productId)));

app.MapPost("/cart/add-to-cart", (Product product) =>
{
    cartService.AddCartItem(product);
    return Results.Created("/cart/add-to-cart", product);
});

#endregion

#region Orders

app.MapGet("/order/get-orders", () => Results.Ok(cartService.GetOrders()));

app.MapPost("/order/create-order", () => 
    Results.Created("/order/create-order", cartService.CreateOrder()));

#endregion

app.Run();
