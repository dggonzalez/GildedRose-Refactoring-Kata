namespace GildedRoseKata.Updaters;

/// <summary>
/// Contains the business rules for the 'Backstage Passes' item type.
/// </summary>
public sealed class BackstagePassesItemUpdater : ItemUpdater
{
    /// <summary>
    /// Behavior to apply for an 'Backstage Passes' item type.
    /// </summary>
    /// <param name="item">Item to update.</param>
    public override void Update(Item item)
    {
        item.SellIn--;

        //Quality drops to 0 after the concert.
        if (item.SellIn < 0)
        {
            item.Quality = MinQuality;
            return;
        }

        //Quality increases by 3 when there are 5 days or less.
        if (item.SellIn < 5)
        {
            IncreaseQuality(item, 3);
            return;
        }

        //Quality increases by 2 when there are 10 days or less.
        if (item.SellIn < 10)
        {
            IncreaseQuality(item, 2);
            return;
        }

        //Default decrease.
        IncreaseQuality(item, 1);
    }
}