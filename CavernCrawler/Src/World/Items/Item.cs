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
        public string description;
        public string statDescription;
        public Vector2f position;
        bool isEquippable;

        public Item(string pName)
        {
            itemIDCount++;
            itemID = itemIDCount;
            name = pName;

            LoadItemData();

            itemImage.CreateMaskFromColor(Color.White);
            itemTexture = new Texture(itemImage);

            itemState = State.IN_CONTAINER;
            isEquippable = false;
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
                        "\nDamage Type: Slashing \nGold Value: " + goldValue + "\n\n- Attributes -\n* One handed \n* Double Strike  ";

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
