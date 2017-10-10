using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace CavernCrawler
{
    public enum State { IN_CONTAINER, IN_WORLD, EQUIPPED };

    class Item
    {
        State itemState;
        Image itemImage;
        Texture itemTexture;
        ContainerSlot parentSlot;
        List<ItemProperty> itemProperties;

        static public int itemIDCount = 0;

        public int itemID;
        public int goldValue;
        public float physicalDamage;
        public float magicDamage;
        public float darkDamage;
        public float fireDamage;
        public float iceDamage;
        public float windDamage;
        public float attackSpeed;
        public float defense;

        public string name;
        string prefix;
        string suffix;
        public string description;
        public string statDescription;
        public Vector2f position;
        bool isEquippable;
        bool isStackable;

        public Item(string pName)
        {
            itemIDCount++;
            itemID = itemIDCount;
            name = pName;
            itemProperties = new List<ItemProperty>();
            

            LoadItemData();

            itemImage.CreateMaskFromColor(Color.White);
            itemTexture = new Texture(itemImage);

            itemState = State.IN_CONTAINER;
            isEquippable = false;
            isStackable = false;
        }

        public void LoadItemData()
        {

            XDocument itemsFile = new XDocument();
            itemsFile = XDocument.Load(@"Content\Data\Items.xml");
            IEnumerable<XElement> elements = itemsFile.Descendants();

            foreach(XElement result in elements)
            {
                //This needs change to account for any type of item, not just weapons
                if (result.Name.LocalName == "Weapon")
                {
                    if (result.Element("Name").Value == name)
                    {
                        Console.WriteLine(result.Element("Name").Value);
                        //Load each eleements data into this item
                        description = result.Element("Description").Value;
                        physicalDamage = float.Parse(result.Element("PhysicalDamage").Value);
                        attackSpeed = float.Parse(result.Element("AttackSpeed").Value);
                        goldValue = int.Parse(result.Element("GoldValue").Value);
                        isEquippable = true;

                        // Elemental stats

                        statDescription = "Damage: " + physicalDamage + " \nAttack Speed: " + attackSpeed +
                        "\nDamage Type: Slashing \nGold Value: " + goldValue + "\n\n- Properties -  ";

                        itemImage = new Image(@"Content\Textures\Tx_Item\weapon\" + result.Element("FileName").Value);

                    }
                }
                
            }
            

        }

        public void Draw(GlobalResource globalResource)
        {
            Sprite drawSprite = new Sprite(itemTexture);
            drawSprite.Position = position;
            globalResource.GetWindow().Draw(drawSprite);
        }

        public void SetItemState(State state)
        {
            itemState = state;
        }

        public void UpdateItemDescription()
        {
            String propertiesString = null;
            StringBuilder sb = new StringBuilder();

            //Add property descriptions to end of item description
            for(int i = 0; i < itemProperties.Count; i++)
            {
                propertiesString = sb.Append("\n" + itemProperties[i].description).ToString();
            }

            statDescription = "Damage: " + physicalDamage + " \nAttack Speed: " + attackSpeed +
                        "\nDamage Type: Slashing \nGold Value: " + goldValue + "\n\n- Properties - " + propertiesString;
        }

        public void MoveItemToWorld(Vector2f pos)
        {
            position = pos;
            parentSlot = null;
            SetItemState(State.IN_WORLD);
        }

        public void MoveItemToContainer(ContainerSlot containerSlot)
        {
            parentSlot = containerSlot;
            position = parentSlot.position;
            SetItemState(State.IN_CONTAINER);
        }

        public void SetEquippable(bool value)
        {
            isEquippable = value;
        }

        public void AddProperty(ItemProperty property)
        {
            itemProperties.Add(property);

            if(itemProperties.Count == 1)
            {
                //Add prefix to name
                prefix = property.name;
                name = name.Insert(0, prefix + " ");
            }
            else if(itemProperties.Count == 2)
            {
                //Add suffix to name
                suffix = property.name;
                name = name.Insert(name.Length, " of " + suffix);
            }

            if (property.physicalDamageIncrease != 0)
            {
                physicalDamage *= ((100 / property.physicalDamageIncrease) + 1);
            }

            if (property.attackSpeedIncrease != 0)
            {
                attackSpeed *= ((100 / property.attackSpeedIncrease) + 1);
            }

            UpdateItemDescription();

        }

        public ContainerSlot GetParentSlot()
        {
            return parentSlot;
        }

        public void SetParentSlot(ContainerSlot slot)
        {
            parentSlot = slot;
        }

        public Texture GetTexture()
        {
            return itemTexture;
        }
    }
}
