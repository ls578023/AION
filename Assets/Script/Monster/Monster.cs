using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public enum MonsterType
{
    None,    
    Dragon,
    FireDragon,
    Boss
}

public class Monster : MonoBehaviour
{
    public string _name;
    public int maxHP;
    public int hp;
    public int atk;
    public int def;
    public MonsterType monsterType;
    public int lv;
    public int exp;
    public int money;
  

    protected void LoadXml()//属性赋值
    {
        TextAsset xml = ResourcesManager.Load<TextAsset>(@"Monster");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml.text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Monster").ChildNodes;
        foreach (XmlElement element in nodelist)
        {
            var type = (MonsterType)int.Parse(element.GetAttribute("type"));
            if (type == monsterType)
            {
                _name = element.GetAttribute("name");
                maxHP = int.Parse(element.GetAttribute("maxHP"));
                hp = maxHP;
                atk = int.Parse(element.GetAttribute("atk"));
                def = int.Parse(element.GetAttribute("def"));
                lv = int.Parse(element.GetAttribute("lv"));
                exp = int.Parse(element.GetAttribute("exp"));
                money = int.Parse(element.GetAttribute("money"));
                break;
            }
        }
    }
}
