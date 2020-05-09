using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragon : Monster {

    public GameObject effect1;
    void Start()
    {
        monsterType = MonsterType.FireDragon;
        LoadXml();
    }

    void InitEffect()
    {
        var effect = GameObject.Instantiate(effect1);
        var effectDamage = effect.GetComponent<Monster_EffectDamage>();
        effectDamage.atk = atk;
        effectDamage.beginTime = 0.2f;
        effectDamage.damageTime = 0.2f;
        effectDamage.damageCount = 3f;
        effect.transform.SetParent(transform);
        effect.transform.forward = transform.forward;
        effect.transform.localPosition = new Vector3(0, 1.2f, 2.8f);
        Destroy(effect.gameObject, 1.5f);
    }


}
