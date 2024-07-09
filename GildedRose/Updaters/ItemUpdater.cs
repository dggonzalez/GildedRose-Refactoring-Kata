using GildedRoseKata.Updaters.Contracts;

namespace GildedRoseKata.Updaters;

public class ItemUpdater : IItemUpdater
{
    protected int MinQuality = 0;
    protected int MaxQuality = 50;

    public virtual void Update(Item item)
    {
        item.SellIn--;

        IncreaseQuality(item, item.SellIn < 0 ? -2 : -1);
    }

    protected virtual void IncreaseQuality(Item item, int amount)
    {
        var quality = item.Quality + amount;
        if (quality > MaxQuality)
        {
            item.Quality = MaxQuality;
            return;
        }

        if (quality < MinQuality)
        {
            item.Quality = MinQuality;
            return;
        }

        item.Quality = quality;
    }
}