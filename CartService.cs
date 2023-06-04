namespace echo_store_api;

public class CartService
{
    private static readonly IList<CartItem> _itens = new List<CartItem>();

    public bool Add(Product product)
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

    public bool Remove(int productId)
    {
        var product = _itens.FirstOrDefault(query => query.Product.Id == productId);
        _itens.Remove(product);

        return true;
    }

    public IList<CartItem> Get()
        => _itens;
}