using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public enum MonsterStatus
{
    MS_None,
    MS_Idle,     //待机  == 1
    MS_Patroll,  //巡逻  ==2
    MS_Attack,   //攻击   == 3
    MS_Dead,     //死亡   == 4
    MS_Chase,    //追击
    MS_Skill    
}

public class MonsterAI : MonoBehaviour 
{
    Animator animator;
    NavMeshAgent agent; //导航代理
    Monster monsterInfo;
    float deadZone = 1f;      //距离
    public float waitTimeAtOnce = 3.0f; //每到一个路点，停留的时间
    float timer = 0;

    public MonsterStatus status;
    public Vector3 destPos = new Vector3(1000, 1000, 1000);

    //巡逻
    public float PatrollRadius = 20.0f;                //巡逻的半径 //也是追击半径
    Vector3 originPos = new Vector3(1000, 1000, 1000); //巡逻原点 -- 最终被赋值为怪物出生点
    public float PatrollSpeed = 3.5f;                  //巡逻时的移动速度 -- 给导航设置的

    //攻击和追击
    public float CanAttackRadius = 7f;     //攻击范围
    public float ChaseRadius = 20.0f;        //追击范围  视野范围
    //-- 从原点/当前发现玩家所处的位置开始计算的圆形范围 或者 玩家脱离的距离到达这个数值

    public Transform player;//玩家

    public float HalfSight = 60.0f;       //半视角范围
    public float ChaseSpeed = 5.0f;       //追击时的移动速度

    //追击返回时的判断 -- 当前是否返回的途中
    bool isBack = false;
    public GameObject bloodNum;//飘血
    public GameObject treasure;//宝箱

	void Start () 
	{
        monsterInfo = GetComponent<Monster>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //设置巡逻原点
        originPos = transform.position;

        status = MonsterStatus.MS_Patroll; //默认状态 巡逻

        destPos = CreateRandomPos();
        agent.destination = destPos;  //设置导航目标 开始巡逻
        bloodNum = ResourcesManager.Load<GameObject>("Prefabs\\Num");
        treasure = ResourcesManager.Load<GameObject>("Prefabs\\Treasure");
	}

    //---------------------------------------------------
    //检查玩家是否处于 视角范围内
    void OnSeeCheck()
    {
        if (status == MonsterStatus.MS_Dead)
            return;
        //玩家进入警戒圈
        if (Vector3.Distance(transform.position, player.position) < ChaseRadius)
        {
            status = MonsterStatus.MS_Chase; //追击
        }          
    }

	//追击
    void OnChase()
    {
        if (status == MonsterStatus.MS_Dead)
            return;
        //追击速度改变
        agent.speed = ChaseSpeed;
        //设置追击目标
        agent.destination = player.position;

        //怪物出了巡逻的圈，应该返回到出生点的位置
        if (Vector3.Distance(transform.position, originPos) > PatrollRadius) //直接使用巡逻半径作为追击半径
        {
            PlayerInfo.Instance.isBeHurt = false;
            isBack = true;//设置回到原定
            monsterInfo.hp = monsterInfo.maxHP;//回血
            destPos = originPos; //改变目标点
            agent.destination = destPos; //改变导航目标

            status = MonsterStatus.MS_Patroll; //设置状态为巡逻
        }
        //玩家进入怪物的攻击范围
        if (Vector3.Distance(player.position, transform.position) <= CanAttackRadius)//追到玩家
        {
            agent.isStopped = true;//停止怪物的导航组件
            status = MonsterStatus.MS_Attack;//设置状态为 攻击
        }
    }

    //攻击行为
    void OnAttack()
    {
        if (status == MonsterStatus.MS_Dead)
            return;
        PlayerInfo.Instance.isBeHurt = true;
        //怪物和玩家之间超过了攻击范围
        if (Vector3.Distance(transform.position, player.position) >= CanAttackRadius)
        {
            //那么怪物要继续去追玩家
            agent.isStopped = false;//恢复导航组件            
            status = MonsterStatus.MS_Chase;
        }
    }

    //巡逻间隙 是 站立状态
    void OnIdle()
    {
        if (status == MonsterStatus.MS_Dead)
            return;
        timer += Time.deltaTime;
        if (timer >= waitTimeAtOnce)
        {
            timer = 0;

            destPos = CreateRandomPos();
            agent.destination = destPos;

            status = MonsterStatus.MS_Patroll;
        }
    }

    void OnPatroll()
    {
        if (status == MonsterStatus.MS_Dead)
            return;
        //设置巡逻速度
        agent.speed = PatrollSpeed;
        //计算当前位置 和 最新目标路点 的距离
        if (Vector3.Distance(transform.position, destPos) <= deadZone)
        {
            if (isBack)
            {
                isBack = false;
            }
            status = MonsterStatus.MS_Idle;  //==1
        }
    }

    public void GetDamage(float attack)
    {
        if (status == MonsterStatus.MS_Dead)
            return;
        PlayerInfo.Instance.isBeHurt = true;
        int damage = (int)attack - monsterInfo.def;
        var num = Random.Range(0, 100);
        if (num < PlayerInfo.Instance.crit)
        {
            damage = (int)(damage * 1.5f);//暴击1.5倍伤害
            if (damage < 1)
                damage = 1;
            InitNum(damage, Color.yellow);
        }
        else
        {
            if (damage < 1)
                damage = 1;
            InitNum(damage, Color.red);
        }
        monsterInfo.hp -= damage;
        if (monsterInfo.hp <= 0)
        {
            monsterInfo.hp = 0;
            status = MonsterStatus.MS_Dead;
            if (player.GetComponent<PlayerControl>().target_normalAtk == gameObject || player.GetComponent<PlayerControl>().target_skill == gameObject)
            {
                player.GetComponent<PlayerControl>().target_normalAtk = null;
                player.GetComponent<PlayerControl>().target_skill = null;
                PlayerInfo.Instance.isBeHurt = false;//消除血条
                player.GetComponent<PlayerControl>().nextState = State.Idle;//目标怪物被打死之后，角色就站立不动
            }
            //int index = FindMonster.Instance.monsterList.IndexOf(monsterInfo);
            //if (index != -1)//在当前列表
            //{
            //    FindMonster.Instance.monsterList.RemoveAt(index);
            //}
            if(transform.name=="Death")
            {
                TipPanel.Instance.info.text = "使用键盘按键移动至传送台";
                TipPanel.Instance.Open();
                GetComponent<Boss>().tansfer.GetComponent<Transfer>().enabled = true;//平台启动
            }
            CreatTreasure();//是否要生成一个宝箱
            PlayerInfo.Instance.exp += monsterInfo.exp;//打怪提升经验和金币
            MyEventSystem.SendMsg(EventName.UpdateAttr, null);//刷新面板属性
            if(PlayerInfo.Instance.exp>= PlayerInfo.Instance.needExp)
            {
                PlayerInfo.Instance.exp -= PlayerInfo.Instance.needExp;
                PlayerInfo.Instance.LvUp();
            }
            PlayerInfo.Instance.money += monsterInfo.money;
            MyEventSystem.SendMsg(EventName.UpdateMoney, null);
            if (TaskData.Instance.taskList[0].accepted && !TaskData.Instance.taskList[0].completed)//杀龙任务已经被接取并且未完成
            {
                IssueTask issueTask=GameObject.FindGameObjectWithTag(TaskData.Instance.taskList[0].startNpc).GetComponent<IssueTask>();
                if (transform.GetComponent<Monster>().monsterType == MonsterType.Dragon)
                {
                   issueTask.iceDragon += 1;
                   if (issueTask.iceDragon > 5)
                       issueTask.iceDragon = 5;
                }
                else if(transform.GetComponent<Monster>().monsterType == MonsterType.FireDragon)
                {
                   issueTask.fireDragon += 1;
                   if (issueTask.fireDragon > 5)
                       issueTask.fireDragon = 5;
                }
                if (issueTask.iceDragon >= 5 && issueTask.fireDragon >= 5)
                {
                    TaskData.Instance.taskList[0].completed = true;//任务已经完成
                }
            }
        }
    }

    void InitNum(int damage,Color color)
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
	void Update () 
	{
        //--------------------------------------怪物状态判断和更新-------------------------
        //检查是否追击
        if (status != MonsterStatus.MS_Chase && !isBack && status != MonsterStatus.MS_Attack)
        {
            OnSeeCheck();
            //onListenCheck();
        }
        //AI玩法
        switch(status)
        {
            case MonsterStatus.MS_Chase:
                {
                    OnChase();
                    break;
                }
            case MonsterStatus.MS_Attack:
                {
                    OnAttack();
                    break;
                }
            case MonsterStatus.MS_Idle:
                {
                    OnIdle();
                    break;
                }
            case MonsterStatus.MS_Patroll:
                {
                    OnPatroll();
                   break;
                }
            case MonsterStatus.MS_Dead:
                {
                    PlayerInfo.Instance.isBeHurt = false;
                    break;
                }
        }

        //------------------------设置Animator控制参数-----------------------------------------------
        animator.SetInteger("Status", (int)status);
	}
    //---------------------------------------------------
   
    //巡逻随机点
    Vector3 CreateRandomPos()
    {
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            0,                                //Y轴须为0
            Random.Range(-1f, 1f)
            ).normalized;

        float length = Random.Range(0.1f, PatrollRadius);
        Vector3 randomDstPos = originPos + randomDirection * length;

        return randomDstPos;
    }

    void CreatTreasure()//判断是否有掉落，如果有，就把生成的id给宝箱容器
    {
        var obj = GameObject.Instantiate(treasure);
        obj.transform.position = transform.position;
        obj.transform.forward = transform.forward;
        var idList = obj.GetComponent<Treasure>().idList;
        if (idList.Count > 0)
        {
            for (int i = idList.Count; i > 0; i--)
            {
                idList.RemoveAt(i);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            var prob = Random.Range(1, 10);
            if (prob == 1)//掉落概率
            {
                int type = Random.Range(1, 7);//决定了物品的类型
                if (PlayerInfo.Instance.modelType == ModelType.Boy)//根据角色职业爆相应内容
                {
                    switch (type)
                    {
                        case 1: { idList.Add(ItemID.Weapon1); break; }
                        case 2: { idList.Add(ItemID.Helmet1); break; }
                        case 3: { idList.Add(ItemID.Glove1); break; }
                        case 4: { idList.Add(ItemID.Armor1); break; }
                        case 5: { idList.Add(ItemID.Shoes1); break; }
                        case 6: { idList.Add(ItemID.Pant1); break; }
                    }
                }
                else
                {
                    switch (type)
                    {
                        case 1: { idList.Add(ItemID.Weapon6); break; }
                        case 2: { idList.Add(ItemID.Helmet6); break; }
                        case 3: { idList.Add(ItemID.Glove6); break; }
                        case 4: { idList.Add(ItemID.Armor6); break; }
                        case 5: { idList.Add(ItemID.Shoes6); break; }
                        case 6: { idList.Add(ItemID.Pant6); break; }
                    }
                }
            }
        }
        if (idList.Count == 0)//没有随机到物品
        {
            Destroy(obj);
        }
    }
    //帧事件
    void SkillBegin()//放在动画里面
    {
        animator.SetBool("Skill", true);
    }
    void SkillOver()
    {
        animator.SetBool("Skill", false);
    }

    void Atk()//怪物的普攻伤害
    {
        player.GetComponent<PlayerControl>().GetDamage(monsterInfo.atk);
        PlayerInfo.Instance.isBeHurt = true;
    }

    public void Destroy()
    {
        if (monsterInfo.hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void BaseOffset()//处理BOSS死亡后的位移
    {
        agent.baseOffset = 0.5f;
    }

    //public void OnDrawGizmos()
    //{
    //    //---巡逻范围
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(originPos, PatrollRadius);

    //    //---能攻击范围
    //    //Gizmos.color = Color.yellow;
    //    //Gizmos.DrawWireSphere(transform.position, CanAttackRadius);

    //    //---追击范围
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, ChaseRadius);

    //    ////---观察范围
    //    //Vector3 leftDir = Vector3.Slerp(-transform.right, transform.forward, 1.0f / 3).normalized;
    //    //Vector3 rightDir = Vector3.Slerp(transform.right, transform.forward, 1.0f / 3).normalized;

    //    //Vector3 leftPos = transform.position + leftDir * ChaseRadius;
    //    //Vector3 rightPos = transform.position + rightDir * ChaseRadius;

    //    //Gizmos.color = Color.blue;
    //    //Gizmos.DrawLine(transform.position, leftPos);
    //    //Gizmos.DrawLine(transform.position, rightPos);

    //    //---能攻击范围
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, CanAttackRadius);

    //}

    ////侦听
    //Animator playerAnimator;
    //public float ListenRadius = 15.0f; //检查玩家声音的半径
    ////这个半径 实际是 警戒半径--- 处于半径内 怪会发起追击
    ////玩家是否在 监听范围内 发出了声音
    //void onListenCheck()
    //{
    //    if (status == MonsterStatus.MS_Dead)
    //        return;
    //    //判断玩家正在播放有声音的动画
    //    //if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
    //    if (true)
    //    {
    //        //让声音进行导航到怪物，判断整个导航的距离小于怪物感知范围
    //        float length = GetNavMeshPathLength(player.position);

    //        if (length <= ListenRadius)
    //        {
    //            //代表怪物听到了玩家
    //            status = MonsterStatus.MS_Chase;  //追击
    //        }
    //    }
    //}
    //float GetNavMeshPathLength(Vector3 end)
    //{
    //    //得到NavMeshPath 的对象，用于存放 系统导航 计算的路径
    //    NavMeshPath path = new UnityEngine.AI.NavMeshPath();

    //    //CalculatePath 给定目标点位置之后开始计算整个导航的路径，把计算的结果赋值给path
    //    agent.CalculatePath(end, path);

    //    //创建路点数组，分别存放 起点/终点  -- 系统的路径点 -- 
    //    // 导航的自身需求  系统自己算的
    //    Vector3[] pathNodeArray= new Vector3[path.corners.Length + 2];

    //    //--
    //    pathNodeArray[0] = transform.position; //起始点为怪物
    //    pathNodeArray[pathNodeArray.Length - 1] = end; //结束点为玩家

    //    //--
    //    for (int i = 0; i < path.corners.Length; i++)
    //    {
    //        pathNodeArray[i + 1] = path.corners[i];
    //    }

    //    //----------------------------
    //    //计算系统导航 的路径 长度
    //    float length = 0;
    //    for (int i = 0; i < pathNodeArray.Length - 1; i++)
    //    {
    //        length += Vector3.Distance(pathNodeArray[i], pathNodeArray[i + 1]);
    //    }

    //    return length;
    //}
}
