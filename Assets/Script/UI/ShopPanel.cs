using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel 
{
    public GameObject itemPrefab;
    public Transform parent;
    public Scrollbar scrollbar;
	void Start()
    {
        type = PanelType.ShopPanel;
        StartCoroutine(InitShop());
    }

    IEnumerator InitShop()//初始化商店中的物品
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject obj = GameObject.Instantiate(itemPrefab);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponent<ShopItem>().id = DataCacheManager.Instance.ItemPropDataList[i].id;
            obj.GetComponent<ShopItem>().InitShopUnit();
            obj.transform.SetParent(parent);
        }
        yield return null;
    }

    public override void Open()
    {
        scrollbar.value = 1;//默认为在最上面
        transform.localPosition = new Vector3(0f, 70f, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
    }
}
