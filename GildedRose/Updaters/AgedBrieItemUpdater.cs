namespace GildedRoseKata.Updaters;

public sealed class AgedBrieItemUpdater : ItemUpdater
{
    public override void Update(Item item)
    {
        item.SellIn--;

        IncreaseQuality(item, item.SellIn < 0 ? 2 : 1);
    }
}