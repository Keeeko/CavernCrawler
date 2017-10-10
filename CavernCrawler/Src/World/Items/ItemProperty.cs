using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CavernCrawler
{
    class ItemProperty
    {
        public string name;
        public string description;

        public float physicalDamageIncrease;
        public float magicDamageIncrease;
        public float darkDamageIncrease;
        public float fireDamageIncrease;
        public float iceDamageIncrease;
        public float windDamageIncrease;
        public float attackSpeedIncrease;
        public float defenseIncrease;

        Dictionary<string, float> statIncreases;

        public ItemProperty(string theName)
        {
            name = theName;
            statIncreases = new Dictionary<String, float>();
            LoadPropertyData();
        }

        public void LoadPropertyData()
        {
            XDocument itemsFile = new XDocument();
            itemsFile = XDocument.Load(@"Content\Data\Properties.xml");
            IEnumerable<XElement> elements = itemsFile.Descendants();

            foreach (XElement result in elements)
            {
                //This needs change to account for any type of item, not just weapons
                if (result.Name.LocalName == "Property")
                {
                    if (result.Element("Name").Value == name)
                    {
                        IEnumerable<XElement> propertyStats = result.Descendants();

                        foreach(XElement stat in propertyStats)
                        {
                            if (stat.Name.LocalName.ToString() != "Name" && stat.Name.LocalName.ToString() != "Description" && stat.Name.LocalName.ToString() != "Min"
                                && stat.Name.LocalName.ToString() != "Max")
                            {
                                statIncreases.Add(stat.Name.ToString(), float.Parse(stat.Element("Min").Value));
                            }
                            Console.WriteLine(stat.Name.LocalName.ToString());
                        }

                        //Load each eleements data into this item property
                        physicalDamageIncrease = float.Parse(result.Element("PhysicalDamage").Element("Min").Value);
                        attackSpeedIncrease = float.Parse(result.Element("AttackSpeed").Element("Min").Value);
                        description = result.Element("Description").Value;

                        if (physicalDamageIncrease != 0)
                        {
                            description = string.Format(description, physicalDamageIncrease);
                        }

                        if(attackSpeedIncrease != 0)
                        {
                            description = string.Format(description, attackSpeedIncrease);
                        }

                    }
                }

            }
        }
    }
}
