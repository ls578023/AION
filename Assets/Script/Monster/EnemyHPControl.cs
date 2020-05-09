using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPControl : MonoBehaviour {

    public Image hp;
    public Text lv;
    public Text _name;
    public Text hp_num;
    public Monster monster;
    public PlayerControl control;
	void Start () {
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if (control.target_skill == null && !PlayerInfo.Instance.isBeHurt)//玩家脱离了怪物范围，血条消失
        {
            transform.localScale = Vector3.zero;
            hp.fillAmount = 1;
        }
        else if(control.target_skill != null)
        {
            transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
            if (control.target_skill!=null)
                monster = control.target_skill.GetComponent<Monster>();
            else if(control.target_normalAtk!=null)
                monster = control.target_normalAtk.GetComponent<Monster>();
            hp.fillAmount = (float)monster.hp / (float)monster.maxHP;
            if(hp.fillAmount<=0)
            {
                hp.fillAmount = 0;
            }
            lv.text = monster.lv.ToString();
            _name.text = monster._name;
            hp_num.text = monster.hp + " / " + monster.maxHP;
        }
	}
}
