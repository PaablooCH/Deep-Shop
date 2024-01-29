public class ItemQuantity
{
    private Item _item;
    private int _quantity;

    public ItemQuantity(Item item, int quantity)
    {
        _item = item;
        _quantity = quantity;
    }

    public Item Item { get => _item; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}
