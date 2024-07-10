namespace GildedRoseKata.Updaters;

/// <summary>
/// (Fixed) Contains the business rules for the 'Conjured' item type.
/// </summary>
public sealed class ConjuredItemUpdater : ItemUpdater
{
    /// <summary>
    /// (Fixed) Behavior to apply for an 'Conjured' item type.
    /// </summary>
    /// <param name="item">Item to update.</param>
    public override void Update(Item item)
    {
        item.SellIn--;

        //'Conjured' items degrade in Quality twice as fast as normal items.
        //NOTE: The original algorithm decreased its value by 1.
        //This was changed to align with the specifications.
        //Now it decreases twice as fast.
        IncreaseQuality(item, -2);
    }
}