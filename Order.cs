namespace echo_store_api;

public class Order
{
    public string Id { get; private set; }
    public IList<CartItem> Itens { get; private set; }
    public decimal Price { get; private set; }

    public Order(IList<CartItem> itens)
    {
        Id = Guid.NewGuid().ToString();
        Itens = itens;
        Price = Itens.Sum(query => query.Product.Price * query.Count);
    }
    
}