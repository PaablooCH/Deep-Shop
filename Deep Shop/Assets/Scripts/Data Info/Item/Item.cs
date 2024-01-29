public class Item
{
    private ItemInfoSO _itemInfo;

    public ItemInfoSO ItemInfo { get => _itemInfo; }

    public Item (ItemInfoSO itemInfo)
    {
        _itemInfo = itemInfo;
    }

    public string GetItemId()
    {
        return _itemInfo.IdItem;
    }

    public float CalculateKarma(float priceUnit)
    {
        float percentatge = CalculatePercentatgeBuy(priceUnit);
        if (priceUnit < _itemInfo.BuyPrice) // example 0.75
        {
            percentatge -= 1; // 0.25
            return _itemInfo.Karma * (1 + percentatge); // karma * 1.25
        }
        if (priceUnit <= _itemInfo.BuyPrice + _itemInfo.MaxSoldPrice)
        {
            return _itemInfo.Karma;
        }
        if (percentatge < 2) // example 1.4
        {
            percentatge = 1 - (percentatge - (int)percentatge); // 1 - 0.4 -> 0.6
            return _itemInfo.Karma * percentatge; // karma * 0.6
        }
        else
        {
            return -_itemInfo.Karma * 1.5f;
        }
    }

    public float CalculatePercentatgeBuy(float priceTrade)
    {
        return priceTrade / _itemInfo.BuyPrice;
    }
}