using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BagPanelControl : MonoBehaviour
{
    public AudioSource sound;
    Transform curBagTab;//当前使用的背包页
    public BagUnit[] bagUnitArray;//背包页的所有背包单元格
    ToggleControl tc;//找到ToggleControl脚本
    Transform splitPanel;//拆分面板
    bool firstInit = true;

	// Use this for initialization
	void Start () {
        //获取当前可见的面板和其中的背包单元格
        tc=GameObject.FindGameObjectWithTag("ToggleControl").GetComponent<ToggleControl>();
        //当前面板信息的同步
        curBagTab = tc.curBagTab;
        if (curBagTab)
            bagUnitArray = transform.GetComponentsInChildren<BagUnit>();
        if (firstInit)
        {
            InitBag();
        }
        MyEventSystem.Addlistener(EventName.BagCountUpdate, UpdateCount);
        MyEventSystem.Addlistener(EventName.BagUpdateWhenAdd, UpdateViewWhenAdd);
        splitPanel = GameObject.FindGameObjectWithTag("SplitPanel").transform;
        splitPanel.transform.localScale = Vector3.zero;
        MyEventSystem.Addlistener(EventName.ShowSplitPanel, ShowSplitPanel);//展示拆分面板      
    }


    void InitBag()
    {
        if (BagData.Instance.bagItem.Count != 0)
        {
            for (int i = 0; i < BagData.Instance.bagItem.Count; i++)
            {
                //导入并且生成了物品的预制体
                var bagItemPrefab = ResourcesManager.Load<ItemInfo>("Prefabs\\Item");
                var bagItem = Instantiate<ItemInfo>(bagItemPrefab);
                //将背包数据中的数据物品在视觉层面展现
                bagItem.GetComponent<ItemInfo>().InitItem(BagData.Instance.bagItem[i]);
                //把这个物品挂在对应的单元格中
                bagItem.transform.SetParent(bagUnitArray[i].transform);
                bagItem.transform.localPosition = Vector3.zero;
                bagItem.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        firstInit = false;
    }
    //判断当前鼠标是否在背包格子里面
    public RectTransform GetRect()
    {
        for (int i = 0; i < bagUnitArray.Length; i++)
        {
            //鼠标是否在单元格范围内
            if (RectTransformUtility.RectangleContainsScreenPoint(bagUnitArray[i].GetComponent<RectTransform>(), Input.mousePosition))
            {
                return bagUnitArray[i].GetComponent<RectTransform>();
            }
        }
        return null;
    }

    //更新背包中的物品数量
    void UpdateCount(object _item)
    {
        Item tmpItem = (Item)_item;
        //遍历背包单元格查找有物品的单元格
        for(int i=0;i<bagUnitArray.Length;i++)
        {
            if(bagUnitArray[i].transform.childCount!=0)
            {
                //当前遍历到的这个单元格中的物品
                var thisItem = bagUnitArray[i].transform.GetChild(0).GetComponent<ItemInfo>();
                if (thisItem.item.id == tmpItem.id )
                {
                    thisItem.UpdateItem();//更新物品数量
                }
            }
        }
    }
    //增加物品后更新背包中的物品样子
    void UpdateViewWhenAdd(object _item)
    {
        var tmpItem = _item as Item;
        for(int i=0;i<bagUnitArray.Length-1;i++)//减去删除格
        {
            //找到空的背包
            if(bagUnitArray[i].transform.childCount==0)
            {
                var bagItemPrefab = ResourcesManager.Load<ItemInfo>("Prefabs\\Item");
                var bagItem = Instantiate(bagItemPrefab);
                //把这个预制体变成传过来的这个物品
                bagItem.GetComponent<ItemInfo>().InitItem(tmpItem);
                bagItem.transform.SetParent(bagUnitArray[i].transform);
                bagItem.transform.localPosition = Vector3.zero;
                return;
            }
        }
    }

    void ShowSplitPanel(object obj)
    {
        splitPanel.transform.localScale = new Vector3(1,1,1);
    }

    public void TidyBag()//整理背包,先删再加
    {
        sound.Play();
        BagData.Instance.TidyBag();//根据ID排序
        for(int i=0;i< bagUnitArray.Length;i++)
        {
            if (bagUnitArray[i].transform.childCount != 0)
            {
                Destroy(bagUnitArray[i].transform.GetChild(0).gameObject);
            }
        }
        for (int i = 0; i < BagData.Instance.bagItem.Count; i++)
        {
            var bagItemPrefab = ResourcesManager.Load<ItemInfo>("Prefabs\\Item");
            var bagItem = Instantiate(bagItemPrefab);
            bagItem.GetComponent<ItemInfo>().InitItem(BagData.Instance.bagItem[i]);
            bagItem.transform.SetParent(bagUnitArray[i].transform);
            bagItem.transform.localPosition = Vector3.zero;
            bagItem.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
