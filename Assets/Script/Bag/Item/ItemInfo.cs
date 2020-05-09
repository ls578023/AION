using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

enum PropTipPos
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}
public class ItemInfo : MonoBehaviour ,IPointerClickHandler
{

    public Item item;//物品属性
    public Text countInfo;//个数
    public Image image;
    public int count;

    void Awake()
    {
        image = GetComponent<Image>();
        countInfo = transform.GetComponentInChildren<Text>();
    }
   
    public void InitItem(Item _item)
    {
        item = _item;
        var _sprite = ResourcesManager.Load<Sprite>(item.path);
        image.sprite = _sprite;
        if (item.count > 1)
        {
            countInfo.text = item.count.ToString();
        }
    }

    public void UpdateItem()//更新物品数量
    {
        if (item.count == 0)//物品用完了或者删除掉了
        {
            //更新BagView中物品
            Destroy(this.gameObject);
        }
        else if (item.count > 1)
        {
            countInfo.text = item.count.ToString();
        }
        else if(item.count == 1)
        {
            countInfo.text = "";
        }
    }


    public void OnPointerClick(PointerEventData eventData)//双击物品或者右键物品的判定
    {//只有在All面板才能使用
        if (GameObject.FindGameObjectWithTag("ToggleControl").GetComponent<ToggleControl>().curBagTab.name == "All")
        {
            //判断当前按下事件对应的是鼠标左键
            if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
            {
                Destroy(transform.GetComponent<InitTip>().tmpTip);
                if (item.type < Type.Weapon || item.type==Type.Book)//这个类型是药品或者书
                {
                    BagData.Instance.UseItem(item);//双击鼠标左键使用物品 
                }
                else//使用的是装备
                {
                    var _item = item as Equipment;
                    var tmpBagUnit = transform.parent.transform;//标记开始的那个格子
                    EquPanel equPanel = GameObject.FindGameObjectWithTag("Panel").GetComponent<EquPanel>();
                    for (int i = 0; i < equPanel.euqiment.Length; i++)//遍历6个装备槽
                    {
                        if (_item.type.ToString() == equPanel.euqiment[i].name && _item.modelType==PlayerInfo.Instance.modelType)//物品类型和装备槽相同，并且职业相同 可以装备
                        {
                            if (equPanel.euqiment[i].childCount == 0)//装备栏里面是空的
                            {
                                BagData.Instance.bagItem.Remove(_item);
                                transform.SetParent(equPanel.euqiment[i]);
                                GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                                GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                                GetComponent<RectTransform>().sizeDelta = equPanel.euqiment[i].GetComponent<RectTransform>().sizeDelta - new Vector2(10f, 10f);//图片改变大小
                                MyEventSystem.SendMsg(EventName.UpdateEquiment, _item);//人物增加属性
                                MyEventSystem.SendMsg(EventName.UpdateAttr, null);
                                BagData.Instance.DelItem(_item);//背包删除这个物品
                            }
                            else//装备栏不是空的，要交换装备
                            {
                                var tempEqu = equPanel.euqiment[i].transform.GetChild(0);//当前装备栏中的装备
                                tempEqu.SetParent(tmpBagUnit);
                                tempEqu.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                                tempEqu.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                                tempEqu.GetComponent<RectTransform>().sizeDelta = (tmpBagUnit as RectTransform).sizeDelta - new Vector2(10f, 10f);
                                MyEventSystem.SendMsg(EventName.DischargeEquiment, tempEqu.GetComponent<ItemInfo>().item);//把装备栏物品放到背包中，相应的减去属性加成

                                transform.SetParent(equPanel.euqiment[i]);//背包中的这个装备要换到装备栏
                                GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                                GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                                GetComponent<RectTransform>().sizeDelta = equPanel.euqiment[i].GetComponent<RectTransform>().sizeDelta - new Vector2(10f, 10f);
                                MyEventSystem.SendMsg(EventName.UpdateEquiment, _item);//加上新装备的属性
                                MyEventSystem.SendMsg(EventName.UpdateAttr, null);//刷新面板属性

                                BagData.Instance.bagItem.Add(tempEqu.GetComponent<ItemInfo>().item);//背包数据中添加
                                BagData.Instance.DelItem(_item);//背包物品拖到装备栏了，要删除
                            }
                            return;
                        }
                    }
                }

            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                //记录当前被分拆的物品
                BagData.Instance.splitItem = item;
                //打开拆分界面
                MyEventSystem.SendMsg(EventName.ShowSplitPanel, null);
            }
        }
    }  
}
