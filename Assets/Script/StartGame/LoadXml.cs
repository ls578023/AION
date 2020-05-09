using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;//file相关

public class LoadXml 
{
    //根据选择的角色不同，将角色的信息初始化
    string directoryPath = Application.dataPath + @"/Resources";//文件夹的路径
    string fileName = "/Player.xml";//文件名字
    string initDate = "/StartData.xml";//文件名字
    public void InitDateInXml()
    {
        if (File.Exists(directoryPath + fileName))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(directoryPath + fileName);
            XmlNodeList list = xml.FirstChild.SelectNodes("user");//将所有名称为user的结点添加进去
            foreach (var element in list)
            {
                XmlElement user = element as XmlElement;

                if (user.GetAttribute("ID") == GetData.Instance.nowID && user.GetAttribute("password") == GetData.Instance.nowPassword)
                {
                    if (user.Attributes.Count==3)//新号，加载初始属性
                    {
                        PlayerInfo.Instance.modelType = (ModelType)(int.Parse(user.GetAttribute("modelType")));
                        InitDate();
                    }
                    else//老号，加载存储的属性
                    {
                        PlayerInfo.Instance.playerName = user.GetAttribute("PlayerName");
                        PlayerInfo.Instance.career = user.GetAttribute("career");
                        PlayerInfo.Instance.lv = int.Parse(user.GetAttribute("lv"));
                        PlayerInfo.Instance.atk = int.Parse(user.GetAttribute("atk"));
                        PlayerInfo.Instance.def = int.Parse(user.GetAttribute("def"));
                        PlayerInfo.Instance.maxHP = int.Parse(user.GetAttribute("maxHP"));
                        PlayerInfo.Instance.hp = PlayerInfo.Instance.maxHP;
                        PlayerInfo.Instance.maxMP = int.Parse(user.GetAttribute("maxMP"));
                        PlayerInfo.Instance.mp = PlayerInfo.Instance.maxMP;
                        PlayerInfo.Instance.money = int.Parse(user.GetAttribute("money"));
                        PlayerInfo.Instance.crit = int.Parse(user.GetAttribute("crit"));
                        PlayerInfo.Instance.modelType = (ModelType)(int.Parse(user.GetAttribute("modelType")));
                        PlayerInfo.Instance.exp = 0;
                        PlayerInfo.Instance.needExp = 0;
                    }
                }
            }
        }
    }

    void InitDate()
    {
        if (File.Exists(directoryPath + initDate))
        {
            //TextAsset xmlTextAsset = Resources.Load<TextAsset>("StartDate");

            XmlDocument xml = new XmlDocument();

            //xml.LoadXml(xmlTextAsset.text);
            xml.Load(directoryPath + initDate);
            XmlNode role = xml.SelectSingleNode("root"); ;
            if (PlayerInfo.Instance.modelType == ModelType.Boy)
            {
                role = role.SelectSingleNode("Boy");
            }
            else
            {
                role = role.SelectSingleNode("Girl");
            }
            XmlElement player = role as XmlElement;
            PlayerInfo.Instance.playerName = player.GetAttribute("PlayerName");
            PlayerInfo.Instance.career = player.GetAttribute("career");
            PlayerInfo.Instance.lv = int.Parse(player.GetAttribute("lv"));
            PlayerInfo.Instance.atk = int.Parse(player.GetAttribute("atk"));
            PlayerInfo.Instance.def = int.Parse(player.GetAttribute("def"));
            PlayerInfo.Instance.maxHP = int.Parse(player.GetAttribute("maxHP"));
            PlayerInfo.Instance.hp = PlayerInfo.Instance.maxHP;
            PlayerInfo.Instance.maxMP = int.Parse(player.GetAttribute("maxMP"));
            PlayerInfo.Instance.mp = PlayerInfo.Instance.maxMP;
            PlayerInfo.Instance.money = int.Parse(player.GetAttribute("money"));
            PlayerInfo.Instance.crit = int.Parse(player.GetAttribute("crit"));
            PlayerInfo.Instance.modelType = (ModelType)(int.Parse(player.GetAttribute("modelType")));
            PlayerInfo.Instance.exp = 0;
            PlayerInfo.Instance.needExp = 0;
        }
    }

    public void SaveXml()//把角色的选择结果存进去
    {
        if (File.Exists(directoryPath + fileName))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(directoryPath + fileName);
            XmlNodeList list = xml.FirstChild.SelectNodes("user");//将所有名称为user的结点添加进去
            foreach (var element in list)
            {
                XmlElement user = element as XmlElement;

                if (user.GetAttribute("ID") == GetData.Instance.nowID && user.GetAttribute("password") == GetData.Instance.nowPassword)
                {
                    user.SetAttribute("modelType", ((int)(SelectRole.Instance.modelType)).ToString());
                    xml.Save(directoryPath + fileName);
                    return;
                }
            }
        }
    }
     //单例类
    private static LoadXml _instance = null;
    private LoadXml()
    {

    }
    public static LoadXml Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new LoadXml();
            }
            return _instance;
        }
    }
}
