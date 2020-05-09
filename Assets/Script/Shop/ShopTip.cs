using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ShopItem shopItem;
    GameObject tip;
    GameObject tmpTip;
    Transform canvas;
    Rect[] rects = new Rect[4];

    void Start()
    {
        tip = Resources.Load<GameObject>("Prefabs\\Tip");
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        //左下
        rects[0] = new Rect(0, 0, Screen.width - tip.GetComponent<RectTransform>().sizeDelta.x,
            tip.GetComponent<RectTransform>().sizeDelta.y);
        //左上
        rects[1] = new Rect(0, tip.GetComponent<RectTransform>().sizeDelta.y,
            Screen.width - tip.GetComponent<RectTransform>().sizeDelta.x,
            Screen.height - tip.GetComponent<RectTransform>().sizeDelta.y);
        //右上
        rects[2] = new Rect(Screen.width - tip.GetComponent<RectTransform>().sizeDelta.x,
            tip.GetComponent<RectTransform>().sizeDelta.y,
            tip.GetComponent<RectTransform>().sizeDelta.x,
            Screen.height - tip.GetComponent<RectTransform>().sizeDelta.y);

        //右下
        rects[3] = new Rect(Screen.width - tip.GetComponent<RectTransform>().sizeDelta.x, 0,
          tip.GetComponent<RectTransform>().sizeDelta.x,
          tip.GetComponent<RectTransform>().sizeDelta.y);
    }

    void Update()
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(transform.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Destroy(tmpTip);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ShowTip());
    }

    IEnumerator ShowTip()
    {
        yield return new WaitForSeconds(1f);
        if (tmpTip == null)
        {
            tmpTip = GameObject.Instantiate(tip, canvas);
            Item item = ItemFactory.CreateItem(shopItem.id);
            var itemSprite = ResourcesManager.Load<Sprite>(item.path);
            tmpTip.transform.Find("Icon").GetComponent<Image>().sprite = itemSprite;
            tmpTip.transform.Find("Name").GetComponent<Text>().text = item._name;
            tmpTip.transform.Find("Info").GetComponent<Text>().text = item.info;
            tmpTip.transform.Find("Attr").GetComponent<Text>().text = item.attr.Replace('n', '\n');//改换行符
            tmpTip.transform.Find("LvInfo").GetComponent<Text>().text = item.lvInfo;
            for (int i = 0; i < 4; i++)
            {
                if (rects[i].Contains(Input.mousePosition))//如果鼠标在 左下角区域，将tip自身锚点设为左下角
                {
                    switch (i)
                    {
                        case 0:
                            tmpTip.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
                            break;
                        case 1:
                            tmpTip.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                            break;
                        case 2:
                            tmpTip.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
                            break;
                        case 3:
                            tmpTip.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
                            break;
                    }
                }
            }
            tmpTip.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(Input.mousePosition.x, Input.mousePosition.y - Screen.height);
            tmpTip.transform.SetParent(canvas);
        }

    }


    public void OnPointerExit(PointerEventData eventData)
    {
    }

}
