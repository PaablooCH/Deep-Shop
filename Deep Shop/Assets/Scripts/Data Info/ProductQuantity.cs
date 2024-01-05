public class ProductQuantity
{
    private int _idProduct;
    private int _quantity;

    public int IdProduct { get => _idProduct; set => _idProduct = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }

    public ProductQuantity(int idProduct, int quantity)
    {
        _idProduct = idProduct;
        _quantity = quantity;
    }
}
