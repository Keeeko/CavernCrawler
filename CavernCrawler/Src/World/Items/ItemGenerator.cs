using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CavernCrawler
{
    //This class will be used to generate random items and return the finished object to whatever requests it
    static class ItemGenerator
    {
        static public Item GenerateItem()
        {
            //This function will take in item type, rarity and item level, it will then load the correct properties from
            //properties definitions in the xml files and apply them to the item;
            Item item = new Item("Short Sword");
            ItemProperty twistingProperty = new ItemProperty("Twisting");
            ItemProperty hasteProperty = new ItemProperty("Haste");
            item.AddProperty(twistingProperty);
            item.AddProperty(hasteProperty);
            return item;
        }
    }
}
