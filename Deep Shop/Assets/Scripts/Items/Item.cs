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
        // If you sell the item lower than its price you gain more karma
        if (priceUnit < _itemInfo.BuyPrice) 
        {
            percentatge -= 1;
            return _itemInfo.Karma * (1 + percentatge);
        }
        // If the price is lower or equal than its maxSoldPrice you gain base karma
        if (priceUnit <= _itemInfo.BuyPrice + _itemInfo.MaxSoldPrice)
        {
            return _itemInfo.Karma;
        }
        // If the price is lower than the double you gain less karma
        if (percentatge < 2)
        {
            percentatge = 1 - (percentatge - (int)percentatge);
            return _itemInfo.Karma * percentatge;
        }
        // Return negative karma
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