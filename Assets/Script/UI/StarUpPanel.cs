using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUpPanel : BasePanel {

    public Transform[] starUpUnits;
    public List<Equipment> itemList = new List<Equipment>();
	void Start () {
        type = PanelType.StarUp;
	}

    public void Sure()
    {
        for (int i = 0; i < starUpUnits.Length;i++)
        {
            if(starUpUnits[i].childCount!=0)
            {
                var equ = starUpUnits[i].GetChild(0).GetComponent<ItemInfo>().item as Equipment;
                itemList.Add(equ);
            }
        }
        if (itemList.Count == 4)//4个格子已经添加了物品
        {
            int count = 0;
            for (int i = 1; i < itemList.Count; i++)
            {
                if (itemList[i].id == itemList[0].id && itemList[i].star != 3)//装备和星级都相同,3星装备不可再升星
                {
                    count++;
                }
            }
            if (count == 3)//4件装备一样 达成条件
            {
                for (int i = itemList.Count; i > 0; i--)//将升星素材删掉
                {
                    BagData.Instance.DelItem(itemList[i-1]);
                }
                BagData.Instance.AddItem((ItemID)(((int)itemList[0].id) + 1));//生成一个更高星级的装备
                Close();
                TipPanel.Instance.info.text = "升星成功";
                TipPanel.Instance.Open();
            }
        }
        else
        {
            Close();           
            TipPanel.Instance.info.text = "条件不满足";
            TipPanel.Instance.Open();
        }
    }

    public override void Close()
    {
        if (GameObject.FindGameObjectWithTag("Tip") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Tip"));
        }
        thisCanvasGroup.alpha = 0;
        thisCanvasGroup.blocksRaycasts = false;
        thisCanvasGroup.transform.localPosition = Vector3.zero;
        if (itemList.Count != 0)
        {
            for (int i = itemList.Count-1; i >= 0; i--)
            {
                itemList.RemoveAt(i);//如果关闭面板，会将这个链表置为零
            }
        }
        for (int i = 0; i < starUpUnits.Length; i++)//将凹槽里的东西清空
        {
            if (starUpUnits[i].childCount != 0)
            {
                Destroy(starUpUnits[i].GetChild(0).gameObject);
            }
        }
        if (GameObject.FindGameObjectWithTag("Bag_Panel").GetComponent<BagPanelControl>().bagUnitArray.Length!=0)//刷新背包
            GameObject.FindGameObjectWithTag("Bag_Panel").GetComponent<BagPanelControl>().TidyBag();
    }

    public override void Open()
    {
        transform.localPosition = new Vector3(0, 70f, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
    }

    public RectTransform GetRect()
    {
        for (int i = 0; i < starUpUnits.Length; i++)
        {
            //鼠标是否在单元格范围内
            if (RectTransformUtility.RectangleContainsScreenPoint(starUpUnits[i].GetComponent<RectTransform>(), Input.mousePosition))
            {
                return starUpUnits[i].GetComponent<RectTransform>();
            }
        }
        return null;
    }
}
