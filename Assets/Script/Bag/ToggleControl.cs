using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControl : MonoBehaviour {

    Toggle[] toggles;//子节点的toggles组件
    public GridLayoutGroup[] tabs;//tab下的组件，管理背包格子
    public Transform curBagTab;//当前背包页面
    public BagUnit[] bagUnit;//格子

    void Awake()
    {
        toggles = transform.GetComponentsInChildren<Toggle>();
        tabs = transform.parent.GetComponentsInChildren<GridLayoutGroup>();
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(OnValueChange);
            toggle.isOn = false;
        }
        toggles[0].isOn = true;
        curBagTab = tabs[0].transform;
	}

    void OnValueChange(bool isOn)//哪个toggle是开的，就显示相应的tab
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if (toggles[i].isOn)
            {
                tabs[i].gameObject.SetActive(true);
                curBagTab = tabs[i].transform;
                if (curBagTab.name != "All")
                {
                    for (int j = 0; j < bagUnit.Length; j++)
                    {
                        for(int k=0;k<bagUnit[j].transform.childCount;k++)
                        {
                            Destroy(bagUnit[j].transform.GetChild(k).gameObject);//OnValueChange函数会执行两次，导致会多生成道具，所以换之前全部删除
                        }                    
                    }
                    bagUnit = curBagTab.transform.GetComponentsInChildren<BagUnit>();

                    ShowCurBagTab();
                }
            }
            else
            {
                tabs[i].gameObject.SetActive(false);
            }
        }
    }

    void ShowCurBagTab()
    {
        switch (curBagTab.name)
        {
            case "Equipment":
            {
                ShowTabItem(ItemID.Weapon1, ItemID.Task);//装备id区间
                break;
            }
            case "Drug":
            {
                ShowTabItem(ItemID.HP, ItemID.Weapon1);//药品id区间
                break;
            }
            case "Task":
            {
                ShowTabItem(ItemID.Task, ItemID.Book);//任务道具区间
                break;
            }
            case "Others":
            {
                ShowTabItem(ItemID.Book, ItemID.Others);
                break;
            }
        }
    }

    public void ShowTabItem(ItemID first, ItemID end)
    {
        int index = 0;
        for (int i = 0; i < BagData.Instance.bagItem.Count; i++)
        {
            if (BagData.Instance.bagItem[i].id >= first && BagData.Instance.bagItem[i].id < end)//在某个区间，比如药品在1-9 装备在10-70
            {
                //导入并且生成了物品的预制体
                var bagItemPrefab = ResourcesManager.Load<ItemInfo>("Prefabs\\Item");
                var bagItem = Instantiate<ItemInfo>(bagItemPrefab);
                //将背包数据中的数据物品在视觉层面展现
                bagItem.GetComponent<ItemInfo>().InitItem(BagData.Instance.bagItem[i]);
                //把这个物品挂在对应的单元格中
                bagItem.transform.SetParent(bagUnit[index].transform);
                bagItem.transform.localPosition = Vector3.zero;
                bagItem.transform.localScale = new Vector3(1, 1, 1);
                index++;
            }
        }
    }
    
}
