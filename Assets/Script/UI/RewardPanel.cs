using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanel : MonoBehaviour
{
    public AudioSource sound;
    public GameObject rewardPrefab;
    public CanvasGroup thisCanvasGroup;
    public Transform parent;
    public List<ItemID> idList = new List<ItemID>();

	void Start () {
        thisCanvasGroup = GetComponent<CanvasGroup>();
        Destroy(gameObject,20f);
	}

    public void InitTreasure(List<ItemID> _idList)//根据容器生成掉落列表信息
    {
        idList = _idList;
        for (int i = 0; i < idList.Count; i++)//根据容器里的id生成物品
        {
            GameObject obj = GameObject.Instantiate(rewardPrefab);
            obj.GetComponent<RewardInfo>().id = idList[i];
            obj.GetComponent<RewardInfo>().InitReward();
            obj.transform.SetParent(parent);
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void Sure()//玩家点击确认后，这些物品将会被创建到玩家的背包中
    {
        //玩家点击确认后，这些物品将会被创建到玩家的背包中
        for (int i = idList.Count; i > 0; i--)
        {
            BagData.Instance.AddItem(idList[i-1]);
        }
        Close();
    }
    public void Close()
    {
        sound.Play();
        if (GameObject.FindGameObjectWithTag("Tip") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Tip"));
        }
        thisCanvasGroup.alpha = 0;
        thisCanvasGroup.blocksRaycasts = false;
        thisCanvasGroup.transform.localPosition = new Vector3(-400, 70f, 0);
        //玩家点击取消，不拾取，则将链表清空
        for (int i = idList.Count; i > 0; i--)
        {
            idList.Remove(idList[i-1]);
        }
        Destroy(gameObject);
    }
}
