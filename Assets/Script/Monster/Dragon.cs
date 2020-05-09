using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    public GameObject effect1;
    void Start()
    {
        monsterType = MonsterType.Dragon;
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
        effect.transform.localPosition = new Vector3(0,-1,1);
        Destroy(effect.gameObject, 1.5f);
    }
}