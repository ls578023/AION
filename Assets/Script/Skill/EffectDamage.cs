using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamage : MonoBehaviour {
    public float atk;//伤害量
    public float beginTime;
    public float damageTime;//攻击间隔
    public float damageCount;//攻击次数
    private void Awake()
    {
        GetComponent<AudioSource>().Play();
    }
    public List<Monster> monsterList = new List<Monster>();
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Monster")
        {
            Monster monster = other.GetComponent<Monster>();
            int index = monsterList.IndexOf(monster);
            if(index==-1)//不在当前列表
            {
                MonsterAI monsterAI = other.GetComponent<MonsterAI>();
                StartCoroutine(MutiDamage(monsterAI));
                monsterList.Add(monster);
            }
        }
    }

    IEnumerator MutiDamage(MonsterAI monsterAI)
    {
        for (int i = 0; i < damageCount; i++)
        {
            monsterAI.GetDamage(atk);//让这个怪物获取伤害
            yield return new WaitForSeconds(damageTime);
        }
    }
}
