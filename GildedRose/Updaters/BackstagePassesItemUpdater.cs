namespace GildedRoseKata.Updaters;

public sealed class BackstagePassesItemUpdater : ItemUpdater
{
    public override void Update(Item item)
    {
        item.SellIn--;

        if (item.SellIn < 0)
        {
            item.Quality = MinQuality;
            return;
        }

        if (item.SellIn < 5)
        {
            IncreaseQuality(item, 3);
            return;
        }

        if (item.SellIn < 10)
        {
            IncreaseQuality(item, 2);
            return;
        }

        IncreaseQuality(item, 1);
    }
}