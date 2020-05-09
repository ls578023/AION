using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml;
using System.IO;//file相关

public class BagData
{
    public GameObject curItemPropTip = null;
    public List<Item> bagItem = new List<Item>();//记录当前背包中的所有物品
    public List<Item> tabBagItem = new List<Item>();//当前显示的分页中的道具
    string directoryPath = Application.dataPath + @"/Resources";//文件夹的路径
    string fileName = "/BagData.xml";//文件名字
    public void AddItem(ItemID id, int num=1)
    {
        if (bagItem.Count >= 30)//不能超过这个数量
        {
            TipPanel.Instance.info.text = "背包已满";
            TipPanel.Instance.Open();
            return;
        }
        for(int i=0;i<bagItem.Count;i++)
        {
            //直接添加数量，装备不可叠加
            if (id==bagItem[i].id && bagItem[i].count+num<=99 && bagItem[i].type<Type.Weapon)
            {
                bagItem[i].count += num;
                MyEventSystem.SendMsg(EventName.BagCountUpdate, bagItem[i]);
                return;
            }
        }
        //背包中没有物品,或者叠加到了上限,就需要创建新的物品
        Item obj = ItemFactory.CreateItem(id);
        obj.count = num;
        bagItem.Add(obj);
        MyEventSystem.SendMsg(EventName.BagUpdateWhenAdd, obj);
        SaveDataInXml();
    }

    public void UseItem(Item _item,int num=1)
    {
        switch (_item.type)
        {
            case Type.HP:
            {
                Drug item = _item as Drug;
                if(PlayerInfo.Instance.hp+item.hp>PlayerInfo.Instance.maxHP)
                {
                    PlayerInfo.Instance.hp=PlayerInfo.Instance.maxHP;
                }
                else
                {
                    PlayerInfo.Instance.hp += item.hp;//加血逻辑
                }
                break;
            }
            case Type.MP:
            {
                Drug item = _item as Drug;
                if (PlayerInfo.Instance.mp + item.mp > PlayerInfo.Instance.maxMP)
                {
                    PlayerInfo.Instance.mp = PlayerInfo.Instance.maxMP;
                }
                else
                {
                    PlayerInfo.Instance.mp += item.mp;//加蓝逻辑
                }
                break;
            }
            case Type.Book:
            {
                Book book = _item as Book;
                if (book.id==ItemID.Book)
                {
                    PlayerInfo.Instance.hasBook = true;
                    MyEventSystem.SendMsg(EventName.ShowSkill, null);
                }
                else if (book.id == ItemID.Book_lv)
                {
                    PlayerInfo.Instance.LvUp();                   
                }
                break;
            }
        }
        if (_item.count - num >= 0)
        {
            _item.count -= num;
            //视图层的更新
            MyEventSystem.SendMsg(EventName.BagCountUpdate, _item);

            //注意: 需要在视图层更新后，再删除对应的背包中的物品属性的引用
            if (_item.count == 0)
            {
                bagItem.Remove(_item);
            }
        }
        MyEventSystem.SendMsg(EventName.HPLost);
        MyEventSystem.SendMsg(EventName.MPLost);
        MyEventSystem.SendMsg(EventName.UpdateAttr);
        SaveDataInXml();
    }

    public void DelItem(Item item)
    {
        for (int i = 0; i < bagItem.Count; i++)
        {
            if (bagItem[i] == item)
            {
                bagItem.Remove(bagItem[i]);
                break;
            }
        }
        SaveDataInXml();
    }

    public void TidyBag()//整理背包
    {
        for (int i=0;i< bagItem.Count; i++)
        {
            for(int j=0;j< bagItem.Count-i-1; j++)
            {
                if((int)bagItem[j].id> (int)bagItem[j+1].id)
                {
                    var tmp = bagItem[j];
                    bagItem[j] = bagItem[j + 1];
                    bagItem[j + 1] = tmp;                   
                }
            }
        }
        SaveDataInXml();
    }


    //拆分
    public Item splitItem = null;
    public void SplitItenFunc(int num)
    {
        if (splitItem == null)
            return;
        //拆开数量，更新当前数量
        splitItem.count -= num;
        MyEventSystem.SendMsg(EventName.BagCountUpdate, splitItem);
        //生成一个相同的物体，数量为拆分数量
        Item newItem = new Item(splitItem);
        newItem.count = num;
        bagItem.Add(newItem);
        MyEventSystem.SendMsg(EventName.BagUpdateWhenAdd, newItem);
        SaveDataInXml();

    }
    public void LoadXml()//读取信息
    {
        //文件存在则读取
        if (File.Exists(directoryPath + fileName))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(directoryPath + fileName);
            XmlNodeList list = xml.FirstChild.SelectNodes("Bag");//找到所有存储的背包物品
            foreach(var element in list)
            {
                XmlElement item = element as XmlElement;
                ItemID id = (ItemID)int.Parse(item.GetAttribute("ID"));
                int count = int.Parse(item.GetAttribute("Count"));
                AddItem(id, count);//根据类型和数量，在背包中添加相应物品
            }
        }   
    }
    public void SaveDataInXml()//将当前拥有的物品数据，进行保存
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(directoryPath + fileName);
        XmlNode root = xml.SelectSingleNode("root");
        root.RemoveAll();//每次存的时候，把当前信息清空，再全部存储
        for(int i=0;i<bagItem.Count;i++)
        {
            XmlElement bag = xml.CreateElement("Bag");
            bag.SetAttribute("ID", ((int)bagItem[i].id).ToString());//把物品的ID和数量信息存进去
            bag.SetAttribute("Count", bagItem[i].count.ToString());
            root.AppendChild(bag);
        }
        xml.Save(directoryPath + fileName);
    }

     #region Singleton
    private static BagData _instance = null;
    private BagData()
    {
        LoadXml();
    }
    public static BagData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BagData();
            }

            return _instance;
        }
    }

    #endregion
}
