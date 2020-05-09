using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour {

    public Monster monsterPrefab;
    int maxNum = 7;
    int curNum = 0;
    float time = 0;
    float bornTime = 3.1f;
    public Transform born;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (born.childCount < maxNum)//当没有怪之后 才重新生成
        {
            time += Time.deltaTime;
            if(time>=bornTime)
            {
                time = 0;
                Vector3 pos = new Vector3(born.position.x + Random.Range(-13f, 13f), born.position.y, born.position.z + Random.Range(-13f, 13f));
                var monster = Instantiate<Monster>(monsterPrefab, pos,Quaternion.identity);
                monster.transform.SetParent(born);
                curNum++;
            }
        }
	}
}
