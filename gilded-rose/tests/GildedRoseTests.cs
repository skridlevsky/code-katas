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

}
