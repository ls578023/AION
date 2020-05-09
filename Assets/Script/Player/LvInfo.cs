using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;//file相关

public class LvInfo 
{

    public Dictionary<int, Lv> lvDic = new Dictionary<int, Lv>();

    void LoadXml()
    {
        TextAsset xml = ResourcesManager.Load<TextAsset>(@"LvInfo");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml.text);
        XmlNodeList childList = xmlDoc.SelectSingleNode("root").ChildNodes;//所有的“lvinfo”
        foreach (var lvInfo in childList)
        {
            XmlElement element = lvInfo as XmlElement;
            Lv tmp = new Lv();
            int lv= int.Parse(element.GetAttribute("lv"));
            tmp.atk = int.Parse(element.GetAttribute("atk"));
            tmp.def = int.Parse(element.GetAttribute("def"));
            tmp.maxHP = int.Parse(element.GetAttribute("maxHP"));
            tmp.maxMP = int.Parse(element.GetAttribute("maxMP"));
            tmp.needExp= int.Parse(element.GetAttribute("needExp"));
            lvDic[lv] = tmp;
        }
    }

    static LvInfo _instance = null;
    LvInfo()
    {
        LoadXml();
    }
    public static LvInfo Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LvInfo();
            }
            return _instance;
        }
    }
}

public class Lv
{
    public int atk;
    public int def;
    public int maxHP;
    public int maxMP;
    public int needExp;
}
