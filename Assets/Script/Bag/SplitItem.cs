using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitItem : MonoBehaviour {

    Text num;
    Button add1;
    Button add10;
    Button del1;
    Button del10;
    Button close;
    Button enter;
    int spliteNum = 1;
    int maxNum = 1;
    public AudioSource sound;
    void Awake()
    {
        num = GameObject.FindGameObjectWithTag("SplitNum").GetComponent<Text>();
        add1 = transform.Find("Add1").GetComponent<Button>();
        add10 = transform.Find("Add10").GetComponent<Button>();
        del1 = transform.Find("Del1").GetComponent<Button>();
        del10 = transform.Find("Del10").GetComponent<Button>();
        close = transform.Find("Close").GetComponent<Button>();
        enter = transform.Find("Enter").GetComponent<Button>();
        add1.onClick.AddListener(Add1);
        add10.onClick.AddListener(Add10);                                                                                                                                                          
        del1.onClick.AddListener(Del1);
        del10.onClick.AddListener(Del10);
        close.onClick.AddListener(Close);
        enter.onClick.AddListener(Enter);       
    }

    void Update()
    {
        num.text = spliteNum.ToString();
        if(BagData.Instance.splitItem!=null)
        {
            maxNum = BagData.Instance.splitItem.count;
        }
        else
        {
            maxNum = 1;
        }
    }

    public void Add1()
    {
        sound.Play();
        if (spliteNum+1<maxNum)
        {
            spliteNum++;
            num.text = spliteNum.ToString();
        }
    }

    public void Add10()
    {
        sound.Play();
        if (spliteNum + 10 < maxNum)
        {
            spliteNum+=10;
            num.text = spliteNum.ToString();
        }
    }

    public void Del1()
    {
        sound.Play();
        if (spliteNum>1)
        {
            spliteNum--;
            num.text = spliteNum.ToString();
        }
    }

    public void Del10()
    {
        sound.Play();
        if (spliteNum > 10)
        {
            spliteNum-=10;
            num.text = spliteNum.ToString();
        }
    }

    public void Close()
    {
        sound.Play();
        transform.localScale = Vector3.zero;
        spliteNum = 1; //恢复为1
    }

    public void Enter()
    {
        sound.Play();
        if (BagData.Instance.splitItem.count == 1 && spliteNum == 1)
            return;
        BagData.Instance.SplitItenFunc(spliteNum);//拆分
        transform.localScale = Vector3.zero;
        spliteNum = 1; //恢复为1
    }
}
