using System.Collections.Generic;
using GildedRoseKata.Containers;

namespace GildedRoseKata;

/// <summary>
/// Contains the logic related to managing the sale of goods at the Gilded Rose small inn.
/// </summary>
/// <param name="items">List of items managed by the inn.</param>
public class GildedRose(IList<Item> items)
{
    /// <summary>
    /// Container of item updater instances
    /// </summary>
    private readonly ItemUpdaterContainer _updaterContainer = new();

    /// <summary>
    /// Updates the SellIn and Quality properties of the goods
    /// managed by the inn based on the business rules.
    /// </summary>
    public void UpdateQuality()
    {
        foreach (var item in items)
        {
            //For each item, the container provides the
            //corresponding updater instance based on the item's name.
            var updater = _updaterContainer.GetInstanceFor(item.Name);
            //then the item is updated based on the business rules.
            updater.Update(item);
        }
    }
}