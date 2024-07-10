namespace GildedRoseKata.Updaters.Contracts;

/// <summary>
/// Sets the contract to updates the item.
/// </summary>
public interface IItemUpdater
{
    /// <summary>
    /// Updates the item according to business rules.
    /// </summary>
    /// <param name="item">Item to update.</param>
    public void Update(Item item);
}