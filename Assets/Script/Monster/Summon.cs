using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public enum SummonState
{
    Idle,
    Walk,
    Atk1,
    Atk2,
    Dead
}

public class Summon : MonoBehaviour
{
    int atk;
    int def;
    int maxHP;
    public int hp;
    Animator animator;
    NavMeshAgent nav;
    SummonState state = SummonState.Idle;
    Transform target;
    PlayerControl player;
    float walkSpeed = 7f;
    float atkRadius = 5f;//攻击范围
    GameObject bloodNum;//飘血

    //Vector3 targetPos = new Vector3(1000, 1000, 1000);
    public GameObject effect1;//技能特效
    void Start()
    {
        UpdateSummon(null);//初始化属性
        hp = maxHP;
        MyEventSystem.Addlistener(EventName.UpdateSummon, UpdateSummon);
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = walkSpeed;
        nav.stoppingDistance = atkRadius;//召唤兽不会推怪物，在到达圆形攻击范围内就会开始攻击了
        bloodNum = ResourcesManager.Load<GameObject>("Prefabs\\Num");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        SummonHP.Instance.Open();
        HPLost();
	}
	
    void OnIdle()
    {
        if(player.nextState == State.Run || player.nextState==State.Run_left || player.nextState==State.Run_right || player.nextState==State.Walk_back)
        {
            nav.enabled = true;
            state = SummonState.Walk; 
        }
        if (player.target_normalAtk != null)
        {
            target = player.target_normalAtk.transform;
        }
        else if (player.target_skill != null)
        {
            target = player.target_skill.transform;
        }
        else//都为空则自动攻击
        {

        }
        if (target != null)
        {
            var transform_2d = new Vector2(transform.position.x, transform.position.z);
            var target_2d = new Vector2(target.position.x, target.position.z);
            if (Vector2.Distance(transform_2d, target_2d) <= atkRadius)//攻击距离
            {
                state = SummonState.Atk1;//普攻
            }
        }
    }

    void OnFollow()
    {
        if (nav.enabled)
            nav.destination = player.transform.position - Vector3.forward * 3f;

        if(player.nextState==State.Idle)
            state = SummonState.Idle;
        if(player.target_normalAtk!=null)
        {
            target = player.target_normalAtk.transform;
        }
        else if(player.target_skill!=null)
        {
            target = player.target_skill.transform;
        }
        else//都为空则自动攻击
        {

        }
        if (target != null)
        {
            var transform_2d = new Vector2(transform.position.x, transform.position.z);
            var target_2d = new Vector2(target.position.x, target.position.z);
            if (Vector2.Distance(transform_2d, target_2d) <= atkRadius)//攻击距离
            {
                state = SummonState.Atk1;//普攻
            }
        }
    }

    void Update () {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance >= 20f && !PlayerInfo.Instance.onTransfer)//召唤兽在打怪物的话，不跟角色移动时，离开到一定距离，会闪现到角色这里
        {
            if (nav.enabled)
                nav.destination = transform.position;//不设置为自身的话，怪物会远程攻击刚才的怪物
            nav.enabled = false;//如果传送了，召唤兽的导航不会同时离开，所以暂时关闭，到达地点之后，角色移动后就会开启，导航会同步过去
            transform.position = player.transform.position - Vector3.forward * 2;
        }
        if (target != null && nav.enabled)
            nav.destination = target.position;
        switch(state)
        {
            case SummonState.Idle:
                { OnIdle(); break; }
            case SummonState.Walk:
                { OnFollow(); break; }
            default:break;
        }
        animator.SetInteger("State", (int)state);
	}


    public void UpdateSummon(object obj)
    {
        atk = PlayerInfo.Instance.atk;
        def = PlayerInfo.Instance.def;
        maxHP = PlayerInfo.Instance.maxHP;
    }

    public void GetDamage(float attack)
    {
        if (state == SummonState.Dead)
            return;
        int damage = (int)attack - def;
        if (damage < 1)
            damage = 1;
        hp -= damage;
        HPLost();
        InitNum(damage, Color.white);
        if (hp <= 0)
        {
            hp = 0;
            SummonHP.Instance.Close();//关闭技能面板
            state = SummonState.Dead;
        }
    }

    void HPLost()//受伤的时候会调用这个函数
    {
        SummonHP.Instance.num.text = hp + "/" + maxHP;
        SummonHP.Instance.hp_bar.fillAmount = (float)hp / (float)maxHP;
        if (SummonHP.Instance.hp_bar.fillAmount <= 0)
        {
            SummonHP.Instance.hp_bar.fillAmount = 0;
        }
    }

    void InitNum(int damage, Color color)
    {
        GameObject go = GameObject.Instantiate(bloodNum);
        go.transform.position = transform.position + Vector3.up * 3f;
        go.GetComponent<Text>().text = damage.ToString();
        go.GetComponent<Text>().color = color;
        go.transform.SetAsLastSibling();
        float x = Random.Range(-2f, 2f);
        go.transform.DOLocalJump(go.transform.position + new Vector3(x, 0, 0), 2f, 1, 1.5f);
        Destroy(go.gameObject, 0.8f);
    }

    //帧事件
    public void SkillOrNormal()//切换到普攻或者技能状态
    {
        if (Random.Range(1, 4) == 1)
        {
            state = SummonState.Atk2;
        }
        else
        {
            state = SummonState.Atk1;
        }
    }

    void Atk()
    {
        if (target != null)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));//让怪物看着攻击目标
            target.GetComponent<MonsterAI>().GetDamage(atk);
        }
        else
        {
            state = SummonState.Walk;
        }
    }

    void InitEffect()//技能生成特效
    {
        var effect = GameObject.Instantiate(effect1);
        EffectDamage effectDamage = effect.GetComponent<EffectDamage>();
        effectDamage.atk = PlayerInfo.Instance.atk;
        effectDamage.beginTime = 0.5f;
        effectDamage.damageTime = 0.2f;
        effectDamage.damageCount = 2f;
        effect.transform.SetParent(transform);
        effect.transform.forward = transform.forward;
        effect.transform.localPosition = new Vector3(0, 0.5f, 0);
        Destroy(effect.gameObject, 2f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
