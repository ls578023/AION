using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_EffectDamage : MonoBehaviour {

    public float atk;//伤害量
    public float beginTime;
    public float damageTime;//攻击间隔
    public float damageCount;//攻击次数


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(MutiDamage(other.GetComponent<PlayerControl>()));
        }
        if(other.tag == "Summon")
        {
            StartCoroutine(SummonDamage(other.GetComponent<Summon>()));
        }
    }

    IEnumerator MutiDamage(PlayerControl player)
    {
        for (int i = 0; i < damageCount; i++)
        {
            player.GetDamage(atk);//让角色获取伤害
            yield return new WaitForSeconds(damageTime);
        }
    }

    IEnumerator SummonDamage(Summon summon)
    {
        for (int i = 0; i < damageCount; i++)
        {
            summon.GetDamage(atk);//让召唤兽怪物获取伤害
            yield return new WaitForSeconds(damageTime);
        }
    }
}
