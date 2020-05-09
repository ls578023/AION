using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class DataCacheManager 
{
    public List<Item> ItemPropDataList = new List<Item>();

    public void ParseXml()
    {
        string file = Application.dataPath + @"/Resources/ItemConfig.xml";
        if (file == null || file.Length == 0)
            return;
        //判断文件是否存在
        if (file.EndsWith(file))
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            var rootElement = doc.SelectSingleNode("game");
            //一个链表，用于存放root的子元素
            XmlNodeList childList = rootElement.ChildNodes;
            foreach (XmlElement item in childList)
            {
                Type type = (Type)(int.Parse(item.GetAttribute("Type")));//根据类型生成物品
                Item obj = null;
                switch (type)
                {
                    default:
                    case Type.HP: { obj = new Drug(); break; }
                    case Type.MP: { obj = new Drug(); break; }
                    case Type.Weapon: { obj = new Equipment(); break; }
                    case Type.Armor: { obj = new Equipment(); break; }
                    case Type.Helmet: { obj = new Equipment(); break; }
                    case Type.Glove: { obj = new Equipment(); break; }
                    case Type.Pant: { obj = new Equipment(); break; }
                    case Type.Shoes: { obj = new Equipment(); break; }
                    case Type.Book: { obj = new Book(); break; }
                }
                //通用属性
                obj.id = (ItemID)(int.Parse(item.GetAttribute("ID")));
                obj.type = (Type)(int.Parse(item.GetAttribute("Type")));
                obj._name = item.GetAttribute("name");
                obj.info = item.GetAttribute("info");
                obj.buyPrice = int.Parse(item.GetAttribute("buyPrice"));
                obj.sellPrice = int.Parse(item.GetAttribute("sellPrice"));
                obj.path = item.GetAttribute("path");
                obj.attr= item.GetAttribute("attr");
                obj.lvInfo = item.GetAttribute("lvInfo");
                //特殊属性
                if (type == Type.HP)
                {
                    ((Drug)obj).hp = int.Parse(item.GetAttribute("hp"));
                }
                else if (type == Type.MP)
                {
                    ((Drug)obj).mp = int.Parse(item.GetAttribute("mp"));
                }
                else if (type == Type.Weapon)
                {
                    ((Equipment)obj).atk = int.Parse(item.GetAttribute("atk"));
                    ((Equipment)obj).crit = int.Parse(item.GetAttribute("crit"));
                    ((Equipment)obj).modelType = (ModelType)(int.Parse(item.GetAttribute("modelType")));//男女职业
                    ((Equipment)obj).star = int.Parse(item.GetAttribute("star"));
                }
                else if (type == Type.Armor)
                {
                    ((Equipment)obj).def = int.Parse(item.GetAttribute("def"));
                    ((Equipment)obj).maxHP = int.Parse(item.GetAttribute("maxHP"));
                    ((Equipment)obj).modelType = (ModelType)(int.Parse(item.GetAttribute("modelType")));
                    ((Equipment)obj).star = int.Parse(item.GetAttribute("star"));
                }
                else if (type == Type.Helmet)//头盔
                {
                    ((Equipment)obj).def = int.Parse(item.GetAttribute("def"));
                    ((Equipment)obj).maxHP = int.Parse(item.GetAttribute("maxHP"));
                    ((Equipment)obj).modelType = (ModelType)(int.Parse(item.GetAttribute("modelType")));
                    ((Equipment)obj).star = int.Parse(item.GetAttribute("star"));
                }
                else if (type == Type.Glove)//手套
                {
                    ((Equipment)obj).def = int.Parse(item.GetAttribute("def"));
                    ((Equipment)obj).atk = int.Parse(item.GetAttribute("atk"));
                    ((Equipment)obj).modelType = (ModelType)(int.Parse(item.GetAttribute("modelType")));
                    ((Equipment)obj).star = int.Parse(item.GetAttribute("star"));
                }
                else if (type == Type.Pant)//护腿
                {
                    ((Equipment)obj).def = int.Parse(item.GetAttribute("def"));
                    ((Equipment)obj).maxHP = int.Parse(item.GetAttribute("maxHP"));
                    ((Equipment)obj).modelType = (ModelType)(int.Parse(item.GetAttribute("modelType")));
                    ((Equipment)obj).star = int.Parse(item.GetAttribute("star"));
                }
                else if (type == Type.Shoes)
                {
                    ((Equipment)obj).def = int.Parse(item.GetAttribute("def"));
                    ((Equipment)obj).modelType = (ModelType)(int.Parse(item.GetAttribute("modelType")));
                    ((Equipment)obj).star = int.Parse(item.GetAttribute("star"));
                }
                ItemPropDataList.Add(obj);
            }
        }
        else
        {
            Debug.LogError("xml文件不存在");
        }
    }


    public Item GetItemData(ItemID _id)
    {
        foreach (var item in ItemPropDataList)
        {
            if (item.id == _id)
            {
                return item;
            }
        }
        return null;
    }

    #region 单例类
    private static DataCacheManager _instance = null;
    private DataCacheManager()
    {
        ParseXml();
    }
    public static DataCacheManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataCacheManager();
            }
            return _instance;
        }
    }

    #endregion
}
