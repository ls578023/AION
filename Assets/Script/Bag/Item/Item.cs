using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Type
{
    None,
    HP,
    MP,
    Weapon,
    Helmet,//头盔
    Glove,
    Armor,//胸甲
    Shoes,
    Pant,//护腿
    Book
}

public enum ItemID
{
    None,
    HP,
    MP,
    Weapon1 = 10,//男武器
    Weapon2,
    Weapon3,
    Weapon6 =15,//女武器
    Weapon7,
    Weapon8,
    Helmet1=20,//头盔
    Helmet2,
    Helmet3,
    Helmet6=25,
    Helmet7,
    Helmet8,
    Glove1=30,
    Glove2,
    Glove3,
    Glove6=35,
    Glove7,
    Glove8,
    Armor1=40,//胸甲
    Armor2,
    Armor3,
    Armor6=45,
    Armor7,
    Armor8,
    Shoes1=50,
    Shoes2,
    Shoes3,
    Shoes6=55,
    Shoes7,
    Shoes8,
    Pant1=60,//护腿
    Pant2,
    Pant3,
    Pant6=65,
    Pant7,
    Pant8,
    Task=70,
    Book=90,
    Book_lv,
    Others=120,
}
public class Item 
{

    public string _name;
    public string info;
    public string attr;//属性
    public string lvInfo;
    public int count;
    public int buyPrice;
    public int sellPrice;
    public ItemID id;
    public Type type;
    public string path;

    public Item()
    {
        _name = "";
        info = "";
        count = 1;
        buyPrice = 0;
        sellPrice = 0;
        id = ItemID.None;
        type = Type.None;
        path = "";
        attr = "";
        lvInfo = "";
    }

    public Item(Item _item)
    {
        _name = _item._name;
        info = _item.info;
        count = _item.count;
        buyPrice = _item.buyPrice;
        sellPrice = _item.sellPrice;
        id = _item.id;
        type = _item.type;
        path = _item.path;
        attr = _item.attr;
        lvInfo = _item.lvInfo;
    }
}
