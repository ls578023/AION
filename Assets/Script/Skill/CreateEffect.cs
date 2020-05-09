using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEffect : MonoBehaviour {

    public GameObject boyEffect1;
    public GameObject boyEffect2;
    public GameObject boyEffect3;
    public GameObject girlEffect1;
    public GameObject girlEffect2;
    public GameObject girlEffect3;
    GameObject lv_up;


    void Start()
    {
        lv_up = Resources.Load<GameObject>("lv_up");
    }
    public void Skill1_Effect()
    {
        var effect = GameObject.Instantiate(boyEffect1);
        EffectDamage effectDamage = effect.GetComponent<EffectDamage>();
        effectDamage.atk = PlayerInfo.Instance.atk * 0.8f;
        effectDamage.beginTime = 0.5f;
        effectDamage.damageTime = 0.2f;
        effectDamage.damageCount = 4f;
        effect.transform.rotation = transform.rotation;
        effect.transform.position = transform.position;
        Destroy(effect.gameObject, 1.5f);
    }

    public void Skill2_Effect()
    {
        var effect = GameObject.Instantiate(boyEffect2);
        EffectDamage effectDamage = effect.GetComponent<EffectDamage>();
        effectDamage.atk = PlayerInfo.Instance.atk * 1f;
        effectDamage.beginTime = 0.5f;
        effectDamage.damageTime = 0.2f;
        effectDamage.damageCount = 5f;
        effect.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y-180f, 0);
        effect.transform.GetChild(0).rotation= transform.rotation;
        effect.transform.position = transform.position;
        Destroy(effect.gameObject, 2f);
    }

    public void Skill3_Effect()
    {
        var effect = GameObject.Instantiate(boyEffect3);
        EffectDamage effectDamage = effect.GetComponent<EffectDamage>();
        effectDamage.atk = PlayerInfo.Instance.atk * 1.2f;
        effectDamage.beginTime = 0.5f;
        effectDamage.damageTime = 0.2f;
        effectDamage.damageCount = 6f;
        effect.transform.position = new Vector3(transform.position.x, transform.position.y-1f, transform.position.z);
        Destroy(effect.gameObject, 2f);
    }

    public void Girl1_Effect()
    {
        var effect = GameObject.Instantiate(girlEffect1);
        EffectDamage effectDamage = effect.GetComponent<EffectDamage>();
        effectDamage.atk = PlayerInfo.Instance.atk * 0.8f;
        effectDamage.beginTime = 0.5f;
        effectDamage.damageTime = 0.2f;
        effectDamage.damageCount = 5f;
        Vector3 m_pos = GetComponent<PlayerControl>().target_skill.transform.position;
        effect.transform.position = new Vector3(m_pos.x, transform.position.y - 0.3f, m_pos.z);
        Destroy(effect.gameObject, 3f);
    }

    public void Girl2_Effect()
    {
        var effect = GameObject.Instantiate(girlEffect2);
        EffectDamage effectDamage = effect.GetComponent<EffectDamage>();
        effectDamage.atk = PlayerInfo.Instance.atk * 1f;
        effectDamage.beginTime = 0.5f;
        effectDamage.damageTime = 0.2f;
        effectDamage.damageCount = 6f;
        Vector3 m_pos = GetComponent<PlayerControl>().target_skill.transform.position;
        effect.transform.position = new Vector3(m_pos.x,transform.position.y-0.3f,m_pos.z);
        Destroy(effect.gameObject, 4f);
    }

    public void Girl3_Effect()
    {
        var effect = GameObject.Instantiate(girlEffect3);

        EffectDamage effectDamage = effect.GetComponent<EffectDamage>();
        effectDamage.atk = PlayerInfo.Instance.atk * 1.2f;
        effectDamage.beginTime = 0.5f;
        effectDamage.damageTime = 0.2f;
        effectDamage.damageCount = 6f;

        effect.transform.forward = transform.forward;
        effect.transform.SetParent(transform);
        effect.transform.localPosition = Vector3.zero;
        Destroy(effect.gameObject, 2f);
    }

    public void LvUp()
    {
        var effect = GameObject.Instantiate(lv_up);
        effect.transform.SetParent(transform);
        effect.transform.localPosition = new Vector3(0,1.5f,0);
        Destroy(effect.gameObject, 3f);
    }

}
