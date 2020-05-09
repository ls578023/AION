using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttrPanel : BasePanel
{
    Text nameInfo;
    Text careerInfo;
    Text lvInfo;
    Text hpInfo;
    Text mpInfo;
    Text atkInfo;
    Text defInfo;
    Text critInfo;//暴击
    Text expInfo;
	void Start () {
        type = PanelType.AttributePanel;
        nameInfo = GameObject.FindGameObjectWithTag("Name").GetComponent<Text>();
        careerInfo = GameObject.FindGameObjectWithTag("Career").GetComponent<Text>();
        lvInfo = GameObject.FindGameObjectWithTag("LV").GetComponent<Text>();
        hpInfo = GameObject.FindGameObjectWithTag("HPInfo").GetComponent<Text>();
        mpInfo = GameObject.FindGameObjectWithTag("MPInfo").GetComponent<Text>();
        atkInfo = GameObject.FindGameObjectWithTag("Atk").GetComponent<Text>();
        defInfo = GameObject.FindGameObjectWithTag("Def").GetComponent<Text>();
        critInfo = GameObject.FindGameObjectWithTag("Crit").GetComponent<Text>();
        expInfo= GameObject.FindGameObjectWithTag("Exp").GetComponent<Text>();
        MyEventSystem.Addlistener(EventName.UpdateAttr, UpdateAttr);
        MyEventSystem.SendMsg(EventName.UpdateAttr, null);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAttr(null);
	}

    void UpdateAttr(object obj)
    {
        nameInfo.text = "昵称:" + PlayerInfo.Instance.playerName;
        careerInfo.text = "职业:" + PlayerInfo.Instance.career;
        lvInfo.text = PlayerInfo.Instance.lv.ToString();
        hpInfo.text = "生命值:" + PlayerInfo.Instance.hp + "/" + PlayerInfo.Instance.maxHP;
        mpInfo.text = "魔法值:" + PlayerInfo.Instance.mp + "/" + PlayerInfo.Instance.maxMP;
        atkInfo.text = "攻击力:" + PlayerInfo.Instance.atk;
        defInfo.text = "防御:" + PlayerInfo.Instance.def;
        critInfo.text = "暴击率:" + PlayerInfo.Instance.crit + "%";
        expInfo.text = "经验值:" + PlayerInfo.Instance.exp + "/" + PlayerInfo.Instance.needExp;
        MyEventSystem.SendMsg(EventName.UpdateSummon);
    }

    public override void Open()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Panel").transform);
        transform.SetAsFirstSibling();
        transform.localPosition = new Vector3(85f, 0, 0);
        transform.DOLocalMoveX(444f, 0.5f);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
    }

   

}
