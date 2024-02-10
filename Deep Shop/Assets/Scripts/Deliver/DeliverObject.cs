public class DeliverObject
{
    private float _timeToDeliver;
    private ItemQuantity _productQuantity;

    public DeliverObject(float timeToDeliver, ItemQuantity productQuantity)
    {
        _timeToDeliver = timeToDeliver;
        _productQuantity = productQuantity;
    }

    public float TimeToDeliver { get => _timeToDeliver; set => _timeToDeliver = value; }
    public ItemQuantity ProductQuantity { get => _productQuantity; set => _productQuantity = value; }
}