using Xunit;
using System.Collections.Generic;
using GildedRoseKata;
using GildedRoseKata.Containers;
using GildedRoseKata.Updaters;
using System;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void foo()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("foo", Items[0].Name);
    }

    [Fact]
    public void ItemUpdaterShouldBeDefaultItemUpdater()
    {
        const string itemName = "Any Name";
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(itemName);

        Assert.IsType<ItemUpdater>(updater);
    }

    [Fact]
    public void ItemUpdaterShouldBeAgedBrieItemUpdater() 
    {
        const string itemName = "aged brie";
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(itemName);

        Assert.IsType<AgedBrieItemUpdater>(updater);
    }

    [Fact]
    public void ItemUpdaterShouldBeSulfurasItemUpdater() 
    {
        const string itemName = "sulfuras...";
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(itemName);

        Assert.IsType<SulfurasItemUpdater>(updater);
    }

    [Fact]
    public void ItemUpdaterShouldBeBackstagePassesItemUpdater() 
    {
        const string itemName = "backstage passes...";
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(itemName);

        Assert.IsType<BackstagePassesItemUpdater>(updater);
    }

    [Fact]
    public void ItemUpdaterShouldBeConjuredItemUpdater() 
    {
        const string itemName = "conjured...";
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(itemName);

        Assert.IsType<ConjuredItemUpdater>(updater);
    }

    [Fact]
    public void DefaultItemUpdaterShouldReturnQualityOfSixteen()
    {
        // Arrange
        var item = new Item
        {
            Name = "Any Name",
            SellIn = 2,
            Quality = 20
        };

        // Act
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(item.Name);

        for (var i = 0; i < 3; i++)
        {
            updater.Update(item);
        }

        // Assert
        Assert.IsType<ItemUpdater>(updater);
        Assert.Equal(item.Quality, 16);
    }

    [Fact]
    public void AgedBrieItemUpdaterShouldReturnQualityOfEight()
    {
        // Arrange
        var item = new Item
        {
            Name = "aged brie...",
            SellIn = 2,
            Quality = 0
        };

        // Act
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(item.Name);

        for (var i = 0; i < 5; i++)
        {
            updater.Update(item);
        }

        // Assert
        Assert.IsType<AgedBrieItemUpdater>(updater);
        Assert.Equal(8, item.Quality);
    }

    [Fact]
    public void SulfurasItemUpdaterShouldKeepQualityValue()
    {
        // Arrange
        const int quality = 80;
        var item = new Item
        {
            Name = "Sulfuras",
            SellIn = -1,
            Quality = quality
        };

        // Act
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(item.Name);

        for (var i = 0; i < 100; i++)
        {
            updater.Update(item);
        }

        // Assert
        Assert.IsType<SulfurasItemUpdater>(updater);
        Assert.Equal(quality, item.Quality);
    }

    [Fact]
    public void BackstagePassesItemUpdaterShouldReturnQualityOfZero()
    {
        // Arrange
        var item = new Item
        {
            Name = "Backstage Passes...",
            SellIn = 2,
            Quality = 49
        };

        // Act
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(item.Name);

        for (var i = 0; i < 3; i++)
        {
            updater.Update(item);
        }

        // Assert
        Assert.IsType<BackstagePassesItemUpdater>(updater);
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void ConjuredItemUpdaterShouldReturnQualityOfZero()
    {
        // Arrange
        var item = new Item
        {
            Name = "Conjured...",
            SellIn = 3,
            Quality = 10
        };

        // Act
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(item.Name);

        for (var i = 0; i < 3; i++)
        {
            updater.Update(item);
        }

        // Assert
        Assert.IsType<ConjuredItemUpdater>(updater);
        Assert.Equal(4, item.Quality);
    }

    [Fact]
    public void ContainerShouldThrowAnException()
    {
        // Arrange
        const string itemName = "";

        // Act
        var container = new ItemUpdaterContainer();

        // Assert
        Assert.Throws<ArgumentException>(() => container.GetInstanceFor(itemName));
    }
}