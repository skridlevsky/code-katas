using GildedRose.Console;

namespace tests;

public class GildedRoseTests
{
    [Fact]
    public void NormalItem_DecreasesQualityBy1_BeforeSellByDate()
    {
        var item = new Item { Name = "Normal Item", SellIn = 10, Quality = 20 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });
        
        gildedRose.UpdateQuality();
        
        Assert.Equal(19, item.Quality);
        Assert.Equal(9, item.SellIn);
    }
    
    [Fact]
    public void AgedBrie_IncreasesQualityAsItGetsOlder()
    {
        var item = new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });
    
        gildedRose.UpdateQuality();
    
        Assert.Equal(1, item.Quality);
        Assert.Equal(1, item.SellIn);
    }
    
    [Fact]
    public void Quality_IsCappedAt50_ExceptForSulfuras()
    {
        var items = new List<Item>
        {
            new Item { Name = "Aged Brie", SellIn = 2, Quality = 50 }, // Already at max
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 49 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } // Sulfuras quality is always 80
        };
        var gildedRose = new GildedRose.Console.GildedRose(items);
        
        gildedRose.UpdateQuality();
        
        Assert.Equal(50, items[0].Quality);
        Assert.Equal(50, items[1].Quality);
        Assert.Equal(80, items[2].Quality); // Sulfuras quality remains 80
    }
    
    [Fact]
    public void Sulfuras_NeverChanges()
    {
        var item = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });

        gildedRose.UpdateQuality();

        Assert.Equal(80, item.Quality); // Quality remains the same
        Assert.Equal(0, item.SellIn);   // SellIn remains the same
    }
    
    [Fact]
    public void NormalItem_DegradesTwiceAsFast_AfterSellByDate()
    {
        var item = new Item { Name = "Normal Item", SellIn = -1, Quality = 10 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });
        
        gildedRose.UpdateQuality();
        
        Assert.Equal(8, item.Quality);
        Assert.Equal(-2, item.SellIn);
    }

    [Fact]
    public void NormalItem_QualityNeverGoesNegative()
    {
        var item = new Item { Name = "Normal Item", SellIn = 5, Quality = 0 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });

        gildedRose.UpdateQuality();

        Assert.Equal(0, item.Quality); // Quality stays at 0
        Assert.Equal(4, item.SellIn);  // SellIn decreases as usual
    }
    
    [Fact]
    public void BackstagePasses_IncreaseQualityBy2_When10DaysOrLess()
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });

        gildedRose.UpdateQuality();

        Assert.Equal(22, item.Quality); // Increases by 2
        Assert.Equal(9, item.SellIn);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityBy3_When5DaysOrLess()
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });

        gildedRose.UpdateQuality();

        Assert.Equal(23, item.Quality); // Increases by 3
        Assert.Equal(4, item.SellIn);
    }

    [Fact]
    public void BackstagePasses_QualityDropsToZero_AfterConcert()
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });

        gildedRose.UpdateQuality();

        Assert.Equal(0, item.Quality); // Drops to 0
        Assert.Equal(-1, item.SellIn); // SellIn decreases
    }
    
    [Fact]
    public void AgedBrie_QualityIncreasesTwiceAsFast_AfterSellByDate()
    {
        var item = new Item { Name = "Aged Brie", SellIn = -1, Quality = 10 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });

        gildedRose.UpdateQuality();

        Assert.Equal(12, item.Quality); // Increases by 2
        Assert.Equal(-2, item.SellIn);
    }
    
    [Fact]
    public void ConjuredItem_DecreasesQualityTwiceAsFast_BeforeSellByDate()
    {
        var item = new Item { Name = "Conjured Item", SellIn = 5, Quality = 10 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });

        gildedRose.UpdateQuality();

        Assert.Equal(8, item.Quality); // Decreases by 2
        Assert.Equal(4, item.SellIn);  // Decreases by 1
    }

    [Fact]
    public void ConjuredItem_DecreasesQualityTwiceAsFast_AfterSellByDate()
    {
        var item = new Item { Name = "Conjured Item", SellIn = 0, Quality = 10 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });

        gildedRose.UpdateQuality();

        Assert.Equal(6, item.Quality); // Decreases by 4
        Assert.Equal(-1, item.SellIn); // Decreases by 1
    }

    [Fact]
    public void ConjuredItem_QualityNeverGoesNegative()
    {
        var item = new Item { Name = "Conjured Item", SellIn = 3, Quality = 1 };
        var gildedRose = new GildedRose.Console.GildedRose(new List<Item> { item });

        gildedRose.UpdateQuality();

        Assert.Equal(0, item.Quality); // Quality stays at 0
        Assert.Equal(2, item.SellIn);  // Decreases by 1
    }

}
