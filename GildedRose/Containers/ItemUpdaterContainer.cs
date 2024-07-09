using System;
using System.Collections.Generic;
using GildedRoseKata.Updaters;
using GildedRoseKata.Updaters.Contracts;

namespace GildedRoseKata.Containers;

public class ItemUpdaterContainer
{
    private readonly IDictionary<string, IItemUpdater> _updaters = new Dictionary<string, IItemUpdater>();

    public IItemUpdater GetInstanceFor(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            throw new ArgumentException("It's required", nameof(itemName));
        }

        if (itemName.StartsWith("Aged Brie", StringComparison.OrdinalIgnoreCase))
        {
            return GetInstance(typeof(AgedBrieItemUpdater));
        }

        if (itemName.StartsWith("Sulfuras", StringComparison.OrdinalIgnoreCase))
        {
            return GetInstance(typeof(SulfurasItemUpdater));
        }

        if (itemName.StartsWith("Backstage passes", StringComparison.OrdinalIgnoreCase))
        {
            return GetInstance(typeof(BackstagePassesItemUpdater));
        }

        if (itemName.StartsWith("Conjured", StringComparison.OrdinalIgnoreCase))
        {
            return GetInstance(typeof(ConjuredItemUpdater));
        }

        return GetInstance(typeof(ItemUpdater));

        IItemUpdater GetInstance(Type type)
        {
            var updaterKey = type.Name;
            if (!_updaters.TryGetValue(updaterKey, out var instance))
            {
                instance = (IItemUpdater)Activator.CreateInstance(type);
                _updaters.Add(updaterKey, instance);
            }
            return instance;
        }
    }
}