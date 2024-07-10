using System;
using System.Collections.Generic;
using GildedRoseKata.Updaters;
using GildedRoseKata.Updaters.Contracts;

namespace GildedRoseKata.Containers;

/// <summary>
/// Manages item updater instances. Ensures that a single instance of each type is stored.
/// </summary>
public class ItemUpdaterContainer
{
    /// <summary>
    /// Provides quick access to item updater instances.
    /// Only a single instance of each type is saved, avoiding redundancy.
    /// </summary>
    private readonly IDictionary<string, IItemUpdater> _updaters = new Dictionary<string, IItemUpdater>();

    /// <summary>
    /// Provides the corresponding updater instance based on the item's name.
    /// </summary>
    /// <param name="itemName">item's name</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Will be thrown if the value of the item's name is null, empty or a white-space.</exception>
    public IItemUpdater GetInstanceFor(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            throw new ArgumentException("It's required.", nameof(itemName));
        }

        //An exact comparison can't be made since the actual item's name
        //is not the same as the one stated in the specifications post.

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

        //the default one.
        return GetInstance(typeof(ItemUpdater));

        //If the dictionary contains the instance, it's returned.
        //Otherwise, it's instantiated, added to the dictionary, and returned.
        //This ensures that only a single instance per type is stored in memory.
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