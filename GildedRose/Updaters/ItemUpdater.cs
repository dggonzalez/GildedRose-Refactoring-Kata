using GildedRoseKata.Updaters.Contracts;

namespace GildedRoseKata.Updaters;

/// <summary>
/// Default item updater. Sets the base rules for the update.
/// </summary>
public class ItemUpdater : IItemUpdater
{
    /// <summary>
    /// The minimum value that Quantity can have.
    /// Use the 'new' keyword to override the assignment in a derived class.
    /// </summary>
    protected int MinQuality = 0;
    /// <summary>
    /// The maximum value that Quantity can have.
    /// Use the 'new' keyword to override the assignment in a derived class.
    /// </summary>
    protected int MaxQuality = 50;

    /// <summary>
    /// Default behavior to apply when updating an item's SellIn and Quantity properties.
    /// </summary>
    /// <param name="item">Item to update.</param>
    public virtual void Update(Item item)
    {
        item.SellIn--;

        //Once the sell by date has passed, Quality degrades twice as fast.
        IncreaseQuality(item, item.SellIn < 0 ? -2 : -1);
    }

    /// <summary>
    /// Increases (in a positive or negative direction) the quantity value
    /// as long as the value is within the limits established by the business rules.
    /// </summary>
    /// <param name="item">Item to update.</param>
    /// <param name="amount">Amount to increase or decrease.</param>
    protected virtual void IncreaseQuality(Item item, int amount)
    {
        var quality = item.Quality + amount;
        //The Quality of an item is never more than 50.
        if (quality > MaxQuality)
        {
            item.Quality = MaxQuality;
            return;
        }

        //The Quality of an item is never negative.
        if (quality < MinQuality)
        {
            item.Quality = MinQuality;
            return;
        }

        item.Quality = quality;
    }
}