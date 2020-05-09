using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;//file相关

public enum Role
{
    Boy,
    Girl,
    All
}

public enum SkillType
{
    Atk,
    Buff
}

public enum ReleaseType
{
    Self,
    Monster,
    Position
}

public class SkillData :Singleton<SkillData>
{
    public Info skillInfo;
    public Dictionary<int,Info> skillDic=new Dictionary<int,Info>();//将技能信息加载进来
    public SkillItem[] skillItem;

    void Start()
    {
        LoadXml();
        skillItem = transform.GetComponentsInChildren<SkillItem>();
        for (int i = 0; i < 4; i++)
        {
            if (PlayerInfo.Instance.modelType == ModelType.Boy)
            {
                skillItem[i].id = i + 1;
            }
            else
            {
                skillItem[i].id = i + 4;
            }
            if (i == 3)
                skillItem[i].id = 7;
            skillItem[i].InitSkillInfo();
        }
    }

    void LoadXml()
    {

        TextAsset xml = ResourcesManager.Load<TextAsset>(@"SkillInfo");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml.text);
        XmlNodeList childList = xmlDoc.SelectSingleNode("root").ChildNodes;//所有的“skill”
        foreach(var skill in childList)
        {
            XmlElement element = skill as XmlElement;
            int skillId = int.Parse(element.GetAttribute("id"));
            Info info = new Info();
            info._name = element.GetAttribute("name");
            info.skillType = (SkillType)int.Parse(element.GetAttribute("skillType"));
            info.releaseTRype = (ReleaseType)int.Parse(element.GetAttribute("releaseType"));
            info.role = (Role)int.Parse(element.GetAttribute("role"));
            info.cd = float.Parse(element.GetAttribute("cd"));
            info.lv = int.Parse(element.GetAttribute("lv"));
            info.mp = int.Parse(element.GetAttribute("mp"));
            info.info = element.GetAttribute("info");
            info.path = element.GetAttribute("path");
            skillDic[skillId] = info;
        }
    }

}

public class Info
{
    public string _name;
    public SkillType skillType;
    public ReleaseType releaseTRype;
    public Role role;
    public float cd;
    public int lv;
    public int mp;
    public string info;
    public string path;
}
