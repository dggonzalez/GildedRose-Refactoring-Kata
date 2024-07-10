namespace GildedRoseKata.Updaters;

/// <summary>
/// Contains the business rules for the 'Sulfuras' item type.
/// </summary>
public sealed class SulfurasItemUpdater : ItemUpdater
{
    /// <summary>
    /// Behavior to apply for an 'Sulfuras' item type.
    /// </summary>
    /// <param name="item">Item to update.</param>
    public override void Update(Item item)
    {
        //'Sulfuras', being a legendary item, never has to be sold or decreases in Quality.
    }
}