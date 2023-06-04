namespace echo_store_api;

public class CartService
{
    private static readonly IList<Order> _orders = new List<Order>();
    private static readonly IList<CartItem> _itens = new List<CartItem>();

    public bool AddCartItem(Product product)
    {
        var exists = _itens.Any(query => query.Product.Id == product.Id);

        if (exists)
        {
            foreach (var item in _itens)
                if (item.Product.Id == product.Id)
                    item.Count++;

            return true;
        }
            
        _itens.Add(new CartItem
        {
            Product = product,
            Count = 1
        });
        
        return true;
    }

    public bool RemoveCartItem(int productId)
    {
        var product = _itens.FirstOrDefault(query => query.Product.Id == productId);

        if (product?.Count > 1)
            _itens[_itens.IndexOf(product)].Count--;
        else
            _itens.Remove(product);

        return true;
    }

    public IList<CartItem> GetCartItens()
        => _itens;

    public Order CreateOrder()
    {
        var order = new Order(
            itens: _itens);
        
        _orders.Add(order);

        return order;
    }

    public IList<Order> GetOrders()
        => _orders;
}