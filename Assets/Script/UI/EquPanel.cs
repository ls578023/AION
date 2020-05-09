using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class EquPanel : BasePanel
{
    Button toRight;
    Button toLeft;
    Transform model;
    public Transform[] euqiment;
	// Use this for initialization
	void Start () {
        type = PanelType.EquimentPanel;
        if(PlayerInfo.Instance.modelType==ModelType.Boy)
        {
            model = GameObject.FindGameObjectWithTag("PlayerModel").transform;
        }
        else
        {
            model = GameObject.FindGameObjectWithTag("ModelGirl").transform;
        }

        toRight = GameObject.FindGameObjectWithTag("RotateToRight").GetComponent<Button>();
        toRight.onClick.AddListener(RotateRight);
        toLeft=GameObject.FindGameObjectWithTag("RotateToLeft").GetComponent<Button>();
        toLeft.onClick.AddListener(RotateLeft);
        MyEventSystem.Addlistener(EventName.UpdateEquiment, UpdateEquiment);
        MyEventSystem.Addlistener(EventName.DischargeEquiment, DischargeEquiment);
        for(int i=0;i<euqiment.Length;i++)
        {
            if(euqiment[i].transform.childCount!=0)//装备栏有装备
            {
                UpdateEquiment(euqiment[i].transform.GetChild(0));//把这个装备传进去
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateEquiment(object _item)
    {
       var item = (Equipment)_item;
       switch(item.type)
       {
           case Type.Weapon:
           {
               PlayerInfo.Instance.atk += item.atk;
               PlayerInfo.Instance.crit += item.crit;
               break;
           }
           case Type.Helmet:
           {
               PlayerInfo.Instance.def += item.def;
               PlayerInfo.Instance.maxHP += item.maxHP;
               break;
           }
           case Type.Glove:
           {
               PlayerInfo.Instance.def += item.def;
               PlayerInfo.Instance.atk += item.atk;
               break;
           }
           case Type.Armor:
           {
               PlayerInfo.Instance.def += item.def;
               PlayerInfo.Instance.maxHP += item.maxHP;
               break;
           }
           case Type.Shoes:
           {
               PlayerInfo.Instance.def += item.def;
               break;
           }
           case Type.Pant:
           {
               PlayerInfo.Instance.def += item.def;
               PlayerInfo.Instance.maxHP += item.maxHP;
               break;
           }
       }
    }

    void DischargeEquiment(object _item)//卸下装备
    {
        var item = (Equipment)_item;
        switch (item.type)
        {
            case Type.Weapon:
                {
                    PlayerInfo.Instance.atk -= item.atk;
                    PlayerInfo.Instance.crit -= item.crit;
                    break;
                }
            case Type.Helmet:
                {
                    PlayerInfo.Instance.def -= item.def;
                    PlayerInfo.Instance.maxHP -= item.maxHP;
                    break;
                }
            case Type.Glove:
                {
                    PlayerInfo.Instance.def -= item.def;
                    PlayerInfo.Instance.atk -= item.atk;
                    break;
                }
            case Type.Armor:
                {
                    PlayerInfo.Instance.def -= item.def;
                    PlayerInfo.Instance.maxHP -= item.maxHP;
                    break;
                }
            case Type.Shoes:
                {
                    PlayerInfo.Instance.def -= item.def;
                    break;
                }
            case Type.Pant:
                {
                    PlayerInfo.Instance.def -= item.def;
                    PlayerInfo.Instance.maxHP -= item.maxHP;
                    break;
                }
        }
    }

    public RectTransform GetRect()
    {
        for (int i = 0; i < euqiment.Length; i++)
        {
            //鼠标是否在单元格范围内
            if (RectTransformUtility.RectangleContainsScreenPoint(euqiment[i].GetComponent<RectTransform>(), Input.mousePosition))
            {
                return euqiment[i].GetComponent<RectTransform>();
            }
        }
        return null;
    }

    public override void Open()//打开面板
    {
        transform.localPosition = new Vector3(-580f, 70f, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
    }

    public override void Close()
    {
        thisCanvasGroup.alpha = 0;
        thisCanvasGroup.blocksRaycasts = false;
        thisCanvasGroup.transform.localPosition = Vector3.zero;//都放在中间
        if (GameObject.FindGameObjectWithTag("Attr") != null)
        {
            GameObject.FindGameObjectWithTag("Attr").transform.SetAsFirstSibling();
            GameObject.FindGameObjectWithTag("Attr").transform.localPosition = new Vector3(85f, 0, 0);
        }
    }

    public void RotateRight()//模型转向
    {
        if (model.rotation == Quaternion.Euler(0, 0, 0))
        {
            model.DORotate(new Vector3(0, -180, 0), 1f).SetLoops(2, LoopType.Yoyo);
        }
        else
        {
            model.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void RotateLeft()
    {
        if (model.rotation == Quaternion.Euler(0, 0, 0))
        {
            model.DORotate(new Vector3(0, 180, 0), 1f).SetLoops(2, LoopType.Yoyo);
        }
        else
        {
            model.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
