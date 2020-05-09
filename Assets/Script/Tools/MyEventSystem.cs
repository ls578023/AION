using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

public enum EventName
{
    HPLost,
    MPLost,
    BagCountUpdate,//更新数量
    BagUpdateWhenAdd,//添加物品后，更新视图
    ShowSplitPanel,//显示拆分面板
    UpdateEquiment,//刷新装备面板
    DischargeEquiment,//卸下装备后更新属性
    UpdateAttr,//刷新属性面板
    ShowSkill,
    UpdateTaskPanel,//更新技能面板的内容
    UpdateMoney,//更新背包金币
    UpdateSummon//更新精灵属性
}
public class MyEventSystem
{

    static Dictionary<EventName, Action<object>> eventDic = new Dictionary<EventName, Action<object>>();

    public static void Addlistener(EventName name,Action<object> action)
    {
        if(eventDic.ContainsKey(name))
        {
            eventDic[name] += action;
        }
        else
        {
            eventDic[name] = action;
        }
    }
    public static void Dellistener(EventName name, Action<object> action)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name] -= action;
        }
    }

    public static void SendMsg(EventName name,object obj=null)
    {
        if (eventDic.ContainsKey(name))
        {
            eventDic[name](obj);
        }
    }
}
