using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// Use this for initialization
    GameObject thisObj;
    Transform canvas;
    Vector3 offset;
    Vector3 anchored;//当前物品的锚点
    Transform tmpBagUnit;
    ItemInfo itemInfo;
    InitTip tip;
    RectTransform curBag;
    RectTransform equBag;
    RectTransform starUpUnit;
    
	void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        itemInfo = transform.GetComponent<ItemInfo>();
        thisObj = transform.gameObject;
        tip = transform.GetComponent<InitTip>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("ToggleControl").GetComponent<ToggleControl>().curBagTab.name == "All")
        {
            tip.isKeep = false;
            //当物品已经放在背包里面了，就让他出来，不属于当前的单元格
            tmpBagUnit = thisObj.transform.parent.transform;//标记开始的那个格子
            thisObj.transform.SetParent(canvas);
            thisObj.transform.SetAsLastSibling();//让拖拽中的这个物品处在最前面
            anchored = thisObj.GetComponent<RectTransform>().anchoredPosition;
            offset = anchored - Input.mousePosition;//鼠标和当前物品锚点的偏差
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("ToggleControl").GetComponent<ToggleControl>().curBagTab.name == "All")
        {
            tip.isKeep = false;//拖拽的时候不产生tip
            anchored = Input.mousePosition + offset;//让锚点跟着鼠标走
            thisObj.GetComponent<RectTransform>().anchoredPosition = anchored;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("ToggleControl").GetComponent<ToggleControl>().curBagTab.name == "All")
        {
            if (GameObject.FindGameObjectWithTag("Bag").GetComponent<CanvasGroup>().alpha == 1)
                curBag = GameObject.FindGameObjectWithTag("Bag_Panel").GetComponent<BagPanelControl>().GetRect();
            if (GameObject.FindGameObjectWithTag("Panel").GetComponent<CanvasGroup>().alpha==1)//装备面板处于显示状态
                equBag = GameObject.FindGameObjectWithTag("Panel").GetComponent<EquPanel>().GetRect();//装备框
            if (GameObject.FindGameObjectWithTag("StarUp").GetComponent<CanvasGroup>().alpha == 1)
                starUpUnit = GameObject.FindGameObjectWithTag("StarUp").GetComponent<StarUpPanel>().GetRect();
            //从背包拖到背包，或者从装备栏拖到背包
            if (curBag != null)
            {
                if (curBag.childCount == 0)
                {
                    thisObj.transform.SetParent(curBag);
                    thisObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    thisObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    thisObj.GetComponent<RectTransform>().sizeDelta = curBag.sizeDelta - new Vector2(10f, 10f);
                    if (tmpBagUnit.tag == "Equipment")//如果是从装备栏拖进来的
                    {
                        BagData.Instance.bagItem.Add(itemInfo.item);//背包数据中添加
                        MyEventSystem.SendMsg(EventName.DischargeEquiment, itemInfo.item);//减去这件装备带来的角色属性加成
                        MyEventSystem.SendMsg(EventName.UpdateAttr, null);//刷新属性
                    }
                    if (curBag.name == "Del")
                    {
                        if (GameObject.FindGameObjectWithTag("Tip") != null)
                            Destroy(GameObject.FindGameObjectWithTag("Tip"));
                        PlayerInfo.Instance.money += itemInfo.item.sellPrice * itemInfo.item.count;//售价乘以数量
                        MyEventSystem.SendMsg(EventName.UpdateMoney, null);
                        BagData.Instance.DelItem(itemInfo.item);//数据层面删除
                        Destroy(thisObj);
                    }
                }
                else//当前格子里有东西
                {
                    var tempItem = curBag.transform.GetChild(0);
                    //id相同，并且非装备类型，可以叠加
                    if (itemInfo.item.id == tempItem.GetComponent<ItemInfo>().item.id
                        && itemInfo.item.type < Type.Weapon && itemInfo.item.type > Type.Pant)//拖到的不是装备
                    {
                        if (GameObject.FindGameObjectWithTag("Tip") != null)
                        {
                            Destroy(GameObject.FindGameObjectWithTag("Tip"));
                        }
                        tempItem.GetComponent<ItemInfo>().item.count += itemInfo.item.count;
                        MyEventSystem.SendMsg(EventName.BagCountUpdate, tempItem.GetComponent<ItemInfo>().item);
                        BagData.Instance.bagItem.Remove(itemInfo.item);
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (tmpBagUnit.tag == "Equipment")//从装备栏拖出来装备到背包的
                        {
                            if (tempItem.GetComponent<ItemInfo>().item.type.ToString() == tmpBagUnit.name)//必须要跟相应的装备对换才行
                            {
                                thisObj.transform.SetParent(curBag);
                                thisObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                                thisObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                                thisObj.GetComponent<RectTransform>().sizeDelta = (curBag as RectTransform).sizeDelta - new Vector2(10f, 10f);
                                MyEventSystem.SendMsg(EventName.DischargeEquiment, itemInfo.item);//原始装备已经拖到背包了，要减少属性
                                BagData.Instance.bagItem.Add(itemInfo.item);//背包数据中添加

                                tempItem.SetParent(tmpBagUnit);//这个装备要放到装备栏
                                tempItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                                tempItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                                tempItem.GetComponent<RectTransform>().sizeDelta = (tmpBagUnit as RectTransform).sizeDelta - new Vector2(10f, 10f);
                                MyEventSystem.SendMsg(EventName.UpdateEquiment, tempItem.GetComponent<ItemInfo>().item);//加上新装备的属性                                       
                                MyEventSystem.SendMsg(EventName.UpdateAttr, null);//刷新面板属性
                                BagData.Instance.DelItem(tempItem.GetComponent<ItemInfo>().item);//背包的东西放到装备栏了，所以数据层面将其删除
                            }
                            else//弹回
                            {
                                thisObj.transform.SetParent(tmpBagUnit);
                                thisObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                            }
                        }
                        else if (tmpBagUnit.tag == "BagUnit")//背包里的物品互换格子
                        {
                            //当前格子里的子对象（物品）放到自己之前的格子里，跟自己的格子对换
                            tempItem.SetParent(tmpBagUnit);
                            tempItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                            tempItem.GetComponent<RectTransform>().sizeDelta = (tmpBagUnit as RectTransform).sizeDelta - new Vector2(10f, 10f);
                            thisObj.transform.SetParent(curBag);
                            thisObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                            thisObj.GetComponent<RectTransform>().sizeDelta = curBag.sizeDelta - new Vector2(10f, 10f);
                        }
                    }
                }
            }
            //从背包拖到装备栏
            else if (equBag != null)
            {
                if (itemInfo.item.type.ToString() == equBag.name)//如果类型与装备栏名字相同，代表拖到了对应的装备栏
                {
                    if (equBag.childCount == 0)//装备栏里面是空的
                    {
                        thisObj.transform.SetParent(equBag);
                        thisObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        thisObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        thisObj.GetComponent<RectTransform>().sizeDelta = equBag.sizeDelta - new Vector2(10f, 10f);//图片改变大小
                        MyEventSystem.SendMsg(EventName.UpdateEquiment, itemInfo.item);//人物增加属性
                        MyEventSystem.SendMsg(EventName.UpdateAttr, null);
                        BagData.Instance.DelItem(itemInfo.item);//背包删除这个物品
                    }
                    else//装备栏不是空的，要交换装备
                    {
                        var tempEqu = equBag.transform.GetChild(0);//当前装备栏中的装备
                        tempEqu.SetParent(tmpBagUnit);
                        tempEqu.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        tempEqu.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        tempEqu.GetComponent<RectTransform>().sizeDelta = (tmpBagUnit as RectTransform).sizeDelta - new Vector2(10f, 10f);
                        MyEventSystem.SendMsg(EventName.DischargeEquiment, tempEqu.GetComponent<ItemInfo>().item);//把装备栏物品放到背包中，相应的减去属性加成

                        thisObj.transform.SetParent(equBag);
                        thisObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        thisObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        thisObj.GetComponent<RectTransform>().sizeDelta = equBag.sizeDelta - new Vector2(10f, 10f);
                        MyEventSystem.SendMsg(EventName.UpdateEquiment, itemInfo.item);//加上新装备的属性
                        MyEventSystem.SendMsg(EventName.UpdateAttr, null);//刷新面板属性

                        BagData.Instance.bagItem.Add(tempEqu.GetComponent<ItemInfo>().item);//背包数据中添加
                        BagData.Instance.DelItem(itemInfo.item);//背包物品拖到装备栏了，要删除
                    }

                }
                else//拖到了错误的装备框
                {
                    thisObj.transform.SetParent(tmpBagUnit);
                    thisObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }
            else if (starUpUnit != null && itemInfo.item.type>=Type.Weapon && itemInfo.item.type<=Type.Pant)//这个物品必须是装备
            {
                if(starUpUnit.childCount==0)
                {
                    thisObj.transform.SetParent(starUpUnit);
                    thisObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    thisObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    thisObj.GetComponent<RectTransform>().sizeDelta = starUpUnit.sizeDelta - new Vector2(68f, 68f);//图片改变大小
                    //var equ = itemInfo.item as Equipment;
                    //GameObject.FindGameObjectWithTag("StarUp").GetComponent<StarUpPanel>().itemList.Add(equ);//把这个物品加入链表，方便判断升星条件，如果成功，则删除这些物品；如果不成功，则清空链表，装备还原
                }
            }
            else
            {
                thisObj.transform.SetParent(tmpBagUnit);
                thisObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            tip.isKeep = true;
        }
    }   
}
