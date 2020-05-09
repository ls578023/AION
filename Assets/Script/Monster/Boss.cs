using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster {

    public GameObject effect1;
    public GameObject tansfer;
	// Use this for initialization
	void Start () {
        monsterType = MonsterType.Boss;
        LoadXml();
	}
	
	// Update is called once per frame
	void InitEffect()
    {
        Vector3 player=GetComponent<MonsterAI>().player.position;
        transform.LookAt(new Vector3(player.x, transform.position.y, player.z));//看着玩家
        var effect = GameObject.Instantiate(effect1);
        var effectDamage = effect.GetComponent<Monster_EffectDamage>();
        effectDamage.atk = atk;
        effectDamage.beginTime = 0.2f;
        effectDamage.damageTime = 0.2f;
        effectDamage.damageCount = 4f;
        effect.transform.forward = transform.forward;
        effect.transform.position = new Vector3(player.x, player.y, player.z);
        Destroy(effect.gameObject, 3f);
    }
}
