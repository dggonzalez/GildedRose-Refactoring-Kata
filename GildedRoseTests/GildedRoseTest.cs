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
        // Arrange
        const string itemName = "Any Name";

        // Act
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(itemName);

        // Assert
        Assert.IsType<ItemUpdater>(updater);
    }

    [Fact]
    public void ItemUpdaterShouldBeAgedBrieItemUpdater() 
    {
        // Arrange
        const string itemName = "aged brie";

        // Act
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(itemName);

        // Assert
        Assert.IsType<AgedBrieItemUpdater>(updater);
    }

    [Fact]
    public void ItemUpdaterShouldBeSulfurasItemUpdater() 
    {
        // Arrange
        const string itemName = "sulfuras...";

        // Act
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(itemName);

        // Assert
        Assert.IsType<SulfurasItemUpdater>(updater);
    }

    [Fact]
    public void ItemUpdaterShouldBeBackstagePassesItemUpdater() 
    {
        // Arrange
        const string itemName = "backstage passes...";

        // Act
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(itemName);

        // Assert
        Assert.IsType<BackstagePassesItemUpdater>(updater);
    }

    [Fact]
    public void ItemUpdaterShouldBeConjuredItemUpdater() 
    {
        // Arrange
        const string itemName = "conjured...";

        // Act
        var container = new ItemUpdaterContainer();
        var updater = container.GetInstanceFor(itemName);

        // Assert
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
        //Once the sell by date has passed, Quality degrades twice as fast,
        //so the loop ends 1 day after the sell by date, getting 2 points,
        //which added to the other 2 points obtained in the 2 days before the sell by date,
        //resulting in 4 points. 20 - 4 = 16.
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
        //The Quality increases its value twice as fast when the sale date has passed, 
        //so 3 days after the sale 2 x 3 days = 6, 6 + 2 points got before sell by date = 8.
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
        //No matter the number of days, the Quality should not change.
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
        //The Quality should be zero after the concert.
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void ConjuredItemUpdaterShouldReturnQualityOfFour()
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
        //The Quality should decrease by 2, so after 3 days, its value should be 4.
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
        //ArgumentException will be thrown if the value of the item's name is null, empty or a white-space.
        Assert.Throws<ArgumentException>(() => container.GetInstanceFor(itemName));
    }
}