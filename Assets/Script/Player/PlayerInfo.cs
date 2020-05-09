using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml;
using System.IO;//file相关 

public class PlayerInfo : Singleton<PlayerInfo>
{

    public string playerName = "";
    public string career = "";
    public int lv = 0;
    public int atk = 0;
    public int def = 0;
    public int maxHP = 0;
    public int maxMP = 0;
    public int hp = 0;
    public int mp = 0;
    public int crit = 0;
    public int money = 0;
    public int exp = 0;
    public int needExp = 0;
    public ModelType modelType;
    public GameObject boyModel;
    public GameObject girlModel;
    public bool isBeHurt = false;//是否处于战斗状态
    public bool hasBook = false;
    Image hp_bar;
    Image mp_bar;
    public bool onTransfer = false;//是否在移动平台
    void OnEnable()
    {
        LoadXml.Instance.InitDateInXml();
        if (PlayerInfo.Instance.modelType == ModelType.Boy)
        {
            boyModel.SetActive(true);
            girlModel.SetActive(false);
            GameObject.FindGameObjectWithTag("ModelGirl").SetActive(false);
            GameObject.FindGameObjectWithTag("PlayerModel").SetActive(true);
            transform.SetParent(boyModel.transform);
        }
        else
        {
            girlModel.SetActive(true);
            boyModel.SetActive(false);
            GameObject.FindGameObjectWithTag("PlayerModel").SetActive(false);
            GameObject.FindGameObjectWithTag("ModelGirl").SetActive(true);
            transform.SetParent(girlModel.transform);
        }
        hp_bar = GameObject.FindGameObjectWithTag("HP").GetComponent<Image>();
        mp_bar= GameObject.FindGameObjectWithTag("MP").GetComponent<Image>();
        MyEventSystem.Addlistener(EventName.HPLost, HPLost);
        MyEventSystem.Addlistener(EventName.MPLost, MPLost);
        needExp = 1000;
    }

    void HPLost(object obj)
    {
        hp_bar.fillAmount = (float)hp / (float)maxHP;
        if(hp_bar.fillAmount<=0)
        {
            //角色死亡
            hp_bar.fillAmount = 0;
            GameObject.FindGameObjectWithTag("Quit").GetComponent<QuitGame>().Open();
            Time.timeScale = 0;
        }
    }

    void MPLost(object obj)
    {
        mp_bar.fillAmount = (float)mp / (float)maxMP;
        if (mp_bar.fillAmount <= 0)
        {
            mp_bar.fillAmount = 0;
        }
    }

    public void LvUp()
    {
        lv++;
        if(lv>30)
        {
            lv = 30;
        }
        atk += LvInfo.Instance.lvDic[lv].atk;
        def += LvInfo.Instance.lvDic[lv].def;
        maxHP += LvInfo.Instance.lvDic[lv].maxHP;
        maxMP += LvInfo.Instance.lvDic[lv].maxMP;
        hp = maxHP;
        mp = maxMP;
        needExp = LvInfo.Instance.lvDic[lv].needExp;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CreateEffect>().LvUp();//升级特效
        MyEventSystem.SendMsg(EventName.UpdateAttr, null);//刷新面板属性
        MyEventSystem.SendMsg(EventName.ShowSkill, null);
    }

}
