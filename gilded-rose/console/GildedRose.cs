using System.Collections.Generic;

namespace GildedRose.Console
{
    public class GildedRose
    {
        private IList<Item> Items;

        public GildedRose(IList<Item> items)
        {
            Items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.Name == "Sulfuras, Hand of Ragnaros")
                    continue; // Sulfuras never changes

                if (item.Name.Contains("Conjured"))
                {
                    DecreaseQuality(item, 2);
                }
                else if (item.Name == "Aged Brie")
                {
                    IncreaseQuality(item);
                }
                else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    UpdateBackstagePassQuality(item);
                }
                else
                {
                    DecreaseQuality(item, 1);
                }

                item.SellIn -= 1;

                if (item.SellIn < 0)
                {
                    if (item.Name.Contains("Conjured"))
                    {
                        DecreaseQuality(item, 2);
                    }
                    else if (item.Name == "Aged Brie")
                    {
                        IncreaseQuality(item);
                    }
                    else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        item.Quality = 0; // Quality drops to 0 after concert
                    }
                    else
                    {
                        DecreaseQuality(item, 1);
                    }
                }
            }
        }
        
        private void IncreaseQuality(Item item)
        {
            if (item.Quality < 50)
                item.Quality++;
        }

        private void DecreaseQuality(Item item, int amount)
        {
            item.Quality = Math.Max(0, item.Quality - amount);
        }

        private void UpdateBackstagePassQuality(Item item)
        {
            if (item.SellIn < 6)
                IncreaseQuality(item);
            if (item.SellIn < 11)
                IncreaseQuality(item);

            IncreaseQuality(item); // Default increase
        }

    }
}
