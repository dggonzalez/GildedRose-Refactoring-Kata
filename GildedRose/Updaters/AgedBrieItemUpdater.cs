namespace GildedRoseKata.Updaters;

/// <summary>
/// Contains the business rules for the 'Aged Brie' item type.
/// </summary>
public sealed class AgedBrieItemUpdater : ItemUpdater
{
    /// <summary>
    /// Behavior to apply for an 'Aged Brie' item type.
    /// </summary>
    /// <param name="item">Item to update.</param>
    public override void Update(Item item)
    {
        item.SellIn--;

        //'Aged Brie' actually increases in Quality the older it gets.
        //NOTE: For some reason, the original algorithm
        //increases Quality twice as fast when the sale date has passed,
        //a behavior not described in the specification post.
        IncreaseQuality(item, item.SellIn < 0 ? 2 : 1);
    }
}