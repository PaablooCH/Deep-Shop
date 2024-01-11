[System.Serializable]
public class ProductQuantity
{
    public int idProduct;
    public int quantity;

    public ProductQuantity(int idProduct, int quantity)
    {
        this.idProduct = idProduct;
        this.quantity = quantity;
    }
}
