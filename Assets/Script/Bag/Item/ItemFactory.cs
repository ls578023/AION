using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class ItemFactory
{
    public static Item CreateItem(ItemID _id)
    {
        Item item = null;
        //先赋值给物品一个ID，传入函数后，根据ID再生成其他属性
        switch (_id)
        {
            case ItemID.HP: { item = new Drug(DataCacheManager.Instance.GetItemData(_id) as Drug); break; }
            case ItemID.MP: { item = new Drug(DataCacheManager.Instance.GetItemData(_id) as Drug); break; }
            case ItemID.Weapon1:
            case ItemID.Weapon2:
            case ItemID.Weapon3:
            case ItemID.Weapon6:
            case ItemID.Weapon7:
            case ItemID.Weapon8:
            case ItemID.Armor1:
            case ItemID.Armor2:
            case ItemID.Armor3:
            case ItemID.Armor6:
            case ItemID.Armor7:
            case ItemID.Armor8:
            case ItemID.Helmet1:
            case ItemID.Helmet2:
            case ItemID.Helmet3:
            case ItemID.Helmet6:
            case ItemID.Helmet7:
            case ItemID.Helmet8:
            case ItemID.Glove1:
            case ItemID.Glove2:
            case ItemID.Glove3:
            case ItemID.Glove6:
            case ItemID.Glove7:
            case ItemID.Glove8:
            case ItemID.Pant1:
            case ItemID.Pant2:
            case ItemID.Pant3:
            case ItemID.Pant6:
            case ItemID.Pant7:
            case ItemID.Pant8:
            case ItemID.Shoes1:
            case ItemID.Shoes2:
            case ItemID.Shoes3:
            case ItemID.Shoes6:
            case ItemID.Shoes7:
            case ItemID.Shoes8:
                { item = new Equipment(DataCacheManager.Instance.GetItemData(_id) as Equipment); break; }
            case ItemID.Book:
            case ItemID.Book_lv:
                { item = new Book(DataCacheManager.Instance.GetItemData(_id) as Book); break; }
        }
        return item;
    }

}
