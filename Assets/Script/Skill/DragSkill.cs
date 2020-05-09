using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSkill : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform canvas;
    Vector3 anchored;//当前物品的锚点
    Vector3 offset;
    RectTransform curSkillUnit;
    public int id;
    PlayerControl player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.GetComponent<Image>().color == Color.gray)
        {
            return;//灰色图标时，不能拖
        }
        if (transform.parent.tag != "SkillUnit")//在技能面板拖动时，生成新的，快捷栏拖动时，不生成新的
        {
            GameObject obj = GameObject.Instantiate(this.gameObject);
            obj.transform.SetParent(this.transform.parent);//拖拽的时候复制一个物品留在原地
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            obj.transform.SetAsFirstSibling();
        }
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
        anchored = transform.GetComponent<RectTransform>().anchoredPosition;
        offset = anchored - Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.GetComponent<Image>().color == Color.gray)
        {
            return;//灰色图标时，不能拖
        }
        anchored = Input.mousePosition + offset;
        transform.GetComponent<RectTransform>().anchoredPosition = anchored;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.GetComponent<Image>().color == Color.gray)
        {
            return;//灰色图标时，不能拖
        }       
        curSkillUnit = GameObject.FindGameObjectWithTag("ShortCut").GetComponent<SkillUnitControl>().GetRect();
        if(curSkillUnit!=null)//如果拖到了快捷栏
        {
            for(int i=0;i<player.skillUnit.Count;i++)
            {
                var obj = player.skillUnit[i].transform.GetChild(0);
                if (obj.tag==transform.tag)//遍历有技能的快捷栏
                {
                    if(obj.GetComponent<DragSkill>().id== id)
                    {
                        Destroy(gameObject);//如果快捷栏有这个技能，就不能加进去了
                    }
                }
            }
            if (curSkillUnit.childCount == 1)//空快捷栏，因为有字体
            {
                GameObject obj = GameObject.Instantiate(this.gameObject);
                obj.transform.SetParent(curSkillUnit);//灰色，放在后面
                obj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                obj.transform.GetComponent<Image>().color = Color.gray;
                obj.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
                obj.GetComponent<RectTransform>().sizeDelta = curSkillUnit.sizeDelta - new Vector2(2f, 2f);
                obj.transform.SetAsFirstSibling();
                transform.SetParent(obj.transform);//白色 放在前面
                transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                transform.transform.GetComponent<Image>().color = Color.white ;
                transform.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
                transform.GetComponent<RectTransform>().sizeDelta = curSkillUnit.sizeDelta - new Vector2(2f, 2f);
            }
            else if (curSkillUnit.childCount == 2)
            {
                if(curSkillUnit.GetChild(0).tag=="Skill")
                {
                    Destroy(curSkillUnit.GetChild(0).gameObject);//直接删掉当前技能
                    GameObject obj = GameObject.Instantiate(this.gameObject);
                    obj.transform.SetParent(curSkillUnit);//灰色，放在后面
                    obj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    obj.transform.GetComponent<Image>().color = Color.gray;
                    obj.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    obj.GetComponent<RectTransform>().sizeDelta = curSkillUnit.sizeDelta - new Vector2(2f, 2f);
                    obj.transform.SetAsFirstSibling();
                    transform.SetParent(obj.transform);//白色 放在前面
                    transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    transform.transform.GetComponent<Image>().color = Color.white;
                    transform.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    transform.GetComponent<RectTransform>().sizeDelta = curSkillUnit.sizeDelta - new Vector2(2f, 2f);
                }
                else//如果是物品，不能交换，把技能删除
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
          Destroy(this.gameObject);
        }
    }
}

