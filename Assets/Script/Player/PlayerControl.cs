using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.AI;
public enum State
{
    None,
    Idle,
    Run,
    Jump,
    Dead,
    Wound,
    Skill1,
    Skill2,
    Skill3,
    Skill4,
    Run_left,
    Run_right,
    Walk_back,
    NormalAtk,
    Fly
}

public class PlayerControl : MonoBehaviour 
{

    public Animator animator;
    public Animation ani;
    GameObject trail;
    public State curState = State.Idle;
    public State nextState = State.None;
    public GameObject target_skill;
    public GameObject target_normalAtk;
    public GameObject bloodNum;
    float skillAtkRange;
    Transform shortCut;
    public List<GameObject> skillUnit=new List<GameObject>();
    Click targetPos;
    GameObject summon;

    void Start () {
        if(PlayerInfo.Instance.modelType==ModelType.Boy)//男的用animator 女的用animation
        {
            animator = transform.GetComponent<Animator>();
            skillAtkRange = 3f;
        }
        else
        {
            ani = transform.GetComponent<Animation>();
            skillAtkRange = 20f;
        }         
        trail = GameObject.FindGameObjectWithTag("trail");
        shortCut = GameObject.FindGameObjectWithTag("ShortCut").transform;//快捷栏
        for (int i=0;i< shortCut.childCount; i++)
        {
            skillUnit.Add(shortCut.GetChild(i).gameObject);//查找所有的技能栏
        }
        bloodNum = ResourcesManager.Load<GameObject>("Prefabs\\Num");
        summon = ResourcesManager.Load<GameObject>("Prefabs\\Summon");
        targetPos = GetComponent<Click>();
    }
	// Update is called once per frame
	void Update () {


        //动画控制
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            trail.SetActive(true);
            target_normalAtk = null;
            targetPos.target = transform.position;
            if (target_skill != null)
            {
                transform.LookAt(new Vector3(target_skill.transform.position.x, transform.position.y, target_skill.transform.position.z));
                UseShortCut(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            trail.SetActive(true);
            target_normalAtk = null;//停止普攻
            targetPos.target = transform.position;
            if (target_skill != null)//如果目标怪物死亡，没有切换目标，就报错
            {
                transform.LookAt(new Vector3(target_skill.transform.position.x, transform.position.y, target_skill.transform.position.z));
                UseShortCut(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            trail.SetActive(true);
            target_normalAtk = null;
            targetPos.target = transform.position;
            if (target_skill != null)
            {
                transform.LookAt(new Vector3(target_skill.transform.position.x, transform.position.y, target_skill.transform.position.z));
                UseShortCut(3);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            trail.SetActive(true);
            target_normalAtk = null;
            targetPos.target = transform.position;
            if (target_skill != null)
            {
                transform.LookAt(new Vector3(target_skill.transform.position.x, transform.position.y, target_skill.transform.position.z));
                UseShortCut(4);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            trail.SetActive(true);
            target_normalAtk = null;
            targetPos.target = transform.position;
            if (target_skill != null)
            {
                transform.LookAt(new Vector3(target_skill.transform.position.x, transform.position.y, target_skill.transform.position.z));
                UseShortCut(5);
            }
        }

        if (PlayerInfo.Instance.modelType == ModelType.Boy && curState != nextState)//男主的移动方式
        {
            animator.SetBool("Run", false);
            animator.SetBool("NormalAtk", false);
            animator.SetBool("Skill1", false);
            switch (nextState)
            {
                case State.Run: { animator.SetTrigger("ToRun"); break; }
                case State.Run_left: { animator.SetTrigger("Run_left"); break; }
                case State.Run_right: { animator.SetTrigger("Run_right"); break; }
                case State.Walk_back: { animator.SetTrigger("Walk_back"); break; }
                case State.Skill1: { animator.SetBool("Skill1",true); break; }
                case State.Skill2: { animator.SetTrigger("Skill2"); break; }
                case State.Skill3: { animator.SetTrigger("Skill3"); break; }
                case State.Skill4: { animator.SetTrigger("Skill4"); break; }
                case State.NormalAtk: { animator.SetBool("NormalAtk",true); break; }
                case State.Idle:
                    {
                        if (curState == State.Run || curState == State.Run_left
                            || curState == State.Run_right || curState == State.Walk_back)
                        {
                            animator.SetBool("Run", true);
                        }
                        break;
                    }
            }
            curState = nextState;
            
        }
        else if (PlayerInfo.Instance.modelType == ModelType.Girl)
        {
            switch (nextState)
            {
                case State.Run: { ani.CrossFade("Run"); break; }
                case State.Run_left: { ani.CrossFade("Run_left"); break; }
                case State.Run_right: { ani.CrossFade("Run_right"); break; }
                case State.Walk_back: { ani.CrossFade("Walk_back"); break; }
                case State.Skill1: { ani.CrossFade("Skill1");break; }
                case State.Skill2: { ani.CrossFade("Skill2"); break; }
                case State.Skill3: { ani.CrossFade("Skill3"); break; }
                case State.Skill4: { ani.CrossFade("Skill4"); break; }
                case State.NormalAtk: { ani.CrossFade("Normal_Atk"); break; }
                case State.Idle: {ani.CrossFade("Idle"); break; }
            }
            curState = nextState;
        }



        //UI控制
        if (Input.GetKeyDown(KeyCode.P))//角色面板
        {
            UIManager.instance.ShowPanel(PanelType.EquimentPanel);
        }
        if(Input.GetKeyDown(KeyCode.B))//背包面板
        {
            UIManager.instance.ShowPanel(PanelType.BagPanel);
        }
        if (Input.GetKeyDown(KeyCode.K))//技能面板
        {
            UIManager.instance.ShowPanel(PanelType.SkillPanel);
        }
        if (Input.GetKeyDown(KeyCode.T))//任务面板
        {
            UIManager.instance.ShowPanel(PanelType.TaskPanelSystem);
        }
        if (Input.GetKeyDown(KeyCode.H))//任务面板
        {
            UIManager.instance.ShowPanel(PanelType.Info);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.instance.ShowPanel(PanelType.QuitGame);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            PlayerInfo.Instance.money += 1000;

        }
	}

    public void GetDamage(float attack)
    {
        PlayerInfo.Instance.isBeHurt=true;
        if (curState == State.Dead)
            return;
        int damage = (int)attack - PlayerInfo.Instance.def;
        if (damage < 1)
            damage = 1;
        PlayerInfo.Instance.hp -= damage;
        InitNum(damage, Color.white);
        MyEventSystem.SendMsg(EventName.HPLost);
        MyEventSystem.SendMsg(EventName.UpdateAttr);
        if (PlayerInfo.Instance.hp <= 0)
        {
            PlayerInfo.Instance.hp = 0;
            PlayerInfo.Instance.isBeHurt = false;
            curState = State.Dead;
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

    void Atk()
    {
        if (target_normalAtk != null)
        {
            PlayerInfo.Instance.isBeHurt = true;
            target_normalAtk.GetComponent<MonsterAI>().GetDamage(PlayerInfo.Instance.atk);
        }
    }

    void SkillAtk()//战士在帧使用，用于在特效未生成时的劈砍动作造成伤害
    {
        if (target_skill != null)
        {
            string skillName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;//通过当前动画的名字来判定进入的什么技能
            if ((skillName == "Skill1" || skillName == "Skill1_1"))
            {
                target_skill.GetComponent<MonsterAI>().GetDamage((PlayerInfo.Instance.atk));
            }
            else if ((skillName == "Skill2" || skillName == "Skill2_1"))
            {
                target_skill.GetComponent<MonsterAI>().GetDamage((PlayerInfo.Instance.atk));
            }
            else if ((skillName == "Skill3"))
            {
                target_skill.GetComponent<MonsterAI>().GetDamage((PlayerInfo.Instance.atk));
            }
        }     
    }

    void SkillToNormalAtk()//技能放完后，切换到普攻状态
    {
        target_normalAtk = target_skill;
    }

    void UseShortCut(int num)
    {
        int i = num - 1;//num是对应1—12数字组合，i代表对应的快捷栏下标
        if (skillUnit[i].transform.childCount == 1)//快捷栏没有物品
            return;
        Transform obj = skillUnit[i].transform.GetChild(0);//技能图标，这个是父结点，也就是灰色的那个
        if (obj.tag == "Skill" && obj.GetChild(0).GetComponent<Image>().fillAmount==1 
            && PlayerInfo.Instance.mp>= SkillData.Instance.skillDic[obj.GetComponent<DragSkill>().id].cd)//如果是技能，并且CD转好了,对当前蓝量满足
        {
            int id = obj.GetComponent<DragSkill>().id;
            switch(id)
            {
                case 1:
                case 4:{ nextState = State.Skill1;break;}
                case 2:
                case 5: { nextState = State.Skill2; break; }
                case 3:
                case 6: { nextState = State.Skill3; break; }
                case 7: { nextState = State.Skill4; break; }
            }
            PlayerInfo.Instance.mp -= SkillData.Instance.skillDic[id].mp;//扣蓝
            MyEventSystem.SendMsg(EventName.MPLost);
            StartCoroutine(SkillCDing(obj.GetChild(0).GetComponent<Image>(), SkillData.Instance.skillDic[id].cd));
        }
        else //药品
        {

        }
    }  

    IEnumerator SkillCDing(Image img,float CD_time)
    {
        img.fillAmount = 0;
        while (img.fillAmount<=1)
        {
            img.fillAmount += Time.deltaTime / CD_time;
            if (img.fillAmount >= 1)
            {
                img.fillAmount = 1;
                break;
            }
            else
            {
                yield return null;
            }
        }
    }

    void CreateSummon()//生成召唤兽
    {
        if (GameObject.FindGameObjectWithTag("Summon") == null)//只能召唤一次
        {
            var sum = GameObject.Instantiate(summon);
            sum.transform.position = transform.position + Vector3.forward * 3f;
        }
    }

    IEnumerator PlayAnimation(string cur,string next)
    {
        AnimationState animState = ani[cur];
        AnimationClip clip = animState.clip;
        float length = clip.length;
        yield return new WaitForSeconds(length);
        ani.CrossFade(next);       
    }


    public void OnDrawGizmos()
    {

        //---能攻击范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, skillAtkRange);


    }
}
