using System.Collections.Generic;
using GildedRoseKata.Containers;

namespace GildedRoseKata;

public class GildedRose(IList<Item> items)
{
    private readonly ItemUpdaterContainer _updaterContainer = new();

    public void UpdateQuality()
    {
        foreach (var item in items)
        {
            var updater = _updaterContainer.GetInstanceFor(item.Name);
            updater.Update(item);
        }
    }
}