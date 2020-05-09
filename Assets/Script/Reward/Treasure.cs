using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

    Animation ani;
    public List<ItemID> idList = new List<ItemID>();
    public GameObject rewardPrefab;
    GameObject rewardPanelPrefab;
    GameObject panel;

	void Start () {
        rewardPanelPrefab=ResourcesManager.Load<GameObject>("Prefabs\\UI\\UI_RewardPanel");
        ani = GetComponent<Animation>();
        Destroy(gameObject, 22f);
	}

    public void OnMouseDown()//点开之后，会打开宝箱，播放宝箱动画和奖励列表
    {   
        panel=GameObject.Instantiate(rewardPanelPrefab);
        panel.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        panel.transform.localPosition = new Vector3(-400, 70f, 0);
        panel.GetComponent<RewardPanel>().InitTreasure(idList);
        ani.CrossFade("open");
    }
}
