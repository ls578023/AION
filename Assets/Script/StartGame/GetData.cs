using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;//file相关

public class GetData
{

    Dictionary<string, string> ID_Password = new Dictionary<string, string>();
    string directoryPath = Application.dataPath + @"/Resources";//文件夹的路径
    string fileName = "/Player.xml";//文件名字
    public string nowID;
    public string nowPassword;

    public bool CanEnterGame(string ID, string password)
    {
        //键值对相同则可以登录游戏
        if (ID_Password.ContainsKey(ID))
        {
            return ID_Password[ID] == password;
        }
        return false;
    }

    public bool IfCreatIDSuccess(string ID, string password)
    {
        if (File.Exists(directoryPath + fileName))
        {
            if (ID_Password.ContainsKey(ID))//判断是否已经存在这个ID
            {
                return false;
            }
            else
            {
                AddDateInXml(ID, password);//如果ID不存在，则可以创建
                return true;
            }
        }
        else
        {
            //Directory.CreateDirectory(directoryPath);创建一个文件夹
            CreateXml(ID, password);
            return true;
        }
    }

    //创建XML保存数据
    void CreateXml(string ID, string password)
    {
        XmlDocument xml = new XmlDocument();
        XmlNode root = xml.CreateElement("root");
        xml.AppendChild(root);//创建root节点，存为根结点

        XmlElement user = xml.CreateElement("user");
        user.SetAttribute("ID", ID);
        user.SetAttribute("password", password);
        xml.Save(directoryPath + fileName);
        ID_Password[ID] = password;
    }

    //添加数据到XML
    void AddDateInXml(string ID, string password)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(directoryPath + fileName);
        XmlNode root = xml.SelectSingleNode("root");

        XmlElement user = xml.CreateElement("user");
        user.SetAttribute("ID", ID);
        user.SetAttribute("password", password);
        root.AppendChild(user);//新账号密码要添加进下层
        xml.Save(directoryPath + fileName);
        ID_Password[ID] = password;
    }

    //获取Xml
    void LoadXml()
    {
        if (File.Exists(directoryPath + fileName))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(directoryPath + fileName);
            XmlNodeList list = xml.FirstChild.SelectNodes("user");//将所有名称为user的结点添加进去
            foreach(var element in list)
            {
                XmlElement user = element as XmlElement;
                ID_Password[user.GetAttribute("ID")] = user.GetAttribute("password");
            }
        }
    }
    //单例类
    private static GetData _instance = null;
    private GetData()
    {
        LoadXml();
    }
    public static GetData Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance=new GetData();
            }
            return _instance;
        }
    }
}
