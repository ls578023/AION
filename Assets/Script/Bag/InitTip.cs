using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InitTip : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler
{
    GameObject tip;
    public GameObject tmpTip;
    Transform canvas;
    Rect[] rects = new Rect[4];
    Item item;
    public bool isKeep = false;
 
    void Start()
    {
        item = transform.GetComponent<ItemInfo>().item;
        tip = Resources.Load<GameObject>("Prefabs\\Tip");
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;

        //左下
        rects[0] = new Rect(0, 0,Screen.width - tip.GetComponent<RectTransform>().sizeDelta.x,
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
            isKeep = false;
        }      
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isKeep = true;
        StartCoroutine(ShowTip());
    }

    IEnumerator ShowTip()
    {
        yield return new WaitForSeconds(1f);
        //此处创建完成 tips 之后 需要给其 物品属性
        if (tmpTip == null && isKeep == true)
        {
            tmpTip = GameObject.Instantiate(tip, canvas);
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
        isKeep = false;
    }

   
}
