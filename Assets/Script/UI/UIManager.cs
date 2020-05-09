using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class UIManager
{
    Dictionary<PanelType, BasePanel> panelDic = new Dictionary<PanelType, BasePanel>();
    Transform _canvas;
    Transform canvas
    {
        get
        {
            if(_canvas==null)
            {
                _canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
            }
            return _canvas;
        }
    }
    //根据传入的面板类型，获得相应的面板脚本
    public BasePanel GetPanel(PanelType type)
    {
        //如果面板该存在，就直接返回
        if(panelDic.ContainsKey(type))
        {
            return panelDic[type];
        }
        //如果不存在，就需要创建一个
        //如果存预制体的字典里有这个类型
        if(prefabDic.ContainsKey(type))
        {
            //根据字典中存储的路径，找到预制体，并且将其创建
            GameObject perfab = Resources.Load(prefabDic[type]) as GameObject;
            GameObject panel = GameObject.Instantiate(perfab);
            panel.transform.localScale = new Vector3(1, 1, 1);
            panel.transform.SetParent(canvas);
            panelDic[type] = panel.GetComponent<BasePanel>();
            return panelDic[type];
        }
        else
        {
            return null;
        }
    }
    //根据传入的面板类型，打开或关闭相应的面板
    public void ShowPanel(PanelType type)
    {
        if (GetPanel(type).thisCanvasGroup.alpha == 0)//这个面板是关闭状态
        {
            GetPanel(type).Open();
        }
        else
        {
            GetPanel(type).Close();
            if(type==PanelType.BagPanel)
            {
                BagData.Instance.SaveDataInXml();//如果关闭的是背包，就存储背包信息
            }
        }
    }

    //记录了ui预制体的种类和预制体路径，用于生成ui实例
    Dictionary<PanelType, string> prefabDic = new Dictionary<PanelType, string>();
    void LoadXml()
    {
        TextAsset xml = Resources.Load<TextAsset>("UIConfig");//找到记录UI预制体的xml
        XmlDocument xmlDoc=new XmlDocument();
        xmlDoc.LoadXml(xml.text);
        XmlNodeList list = xmlDoc.SelectSingleNode("root").ChildNodes;
        for(int i=0;i<list.Count;i++)
        {
            XmlElement element = list[i] as XmlElement;
            prefabDic[(PanelType)int.Parse(element.GetAttribute("type"))] = element.GetAttribute("path");
        }
    }

    static UIManager _instance = null;
    UIManager()
    {
        LoadXml();
    }

    public static UIManager instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
}
