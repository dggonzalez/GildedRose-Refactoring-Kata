namespace GildedRoseKata.Updaters;

public sealed class ConjuredItemUpdater : ItemUpdater
{
    public override void Update(Item item)
    {
        item.SellIn--;

        IncreaseQuality(item, -2);
    }
}