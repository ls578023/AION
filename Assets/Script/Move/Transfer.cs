using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transfer : MonoBehaviour {
    Vector3 end;
    void OnEnable()
    {
        end = new Vector3(-100f, 103f, 124f);
        transform.DOLocalMove(end, 30f).SetEase(Ease.InOutQuint).SetDelay(10f).SetLoops(-1, LoopType.Yoyo);//传送台移动方式
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.SetParent(transform);
            PlayerInfo.Instance.onTransfer = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.SetParent(null);
            PlayerInfo.Instance.onTransfer = false;
        }
    }
}
