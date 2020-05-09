using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour {

    float speed = 6f;
    float gravity = 20.0f;
    public Click targetPos;
    CharacterController controller;
    NavMeshAgent navMeshAgent;
    Vector3 moveDir = Vector3.zero;
    PlayerControl control;
    Vector3 npcPos;
    GameObject trail;
    float time = 0;

    // Use this for initialization
    void Start () {
        control = GetComponent<PlayerControl>();
        targetPos = GetComponent<Click>();
        controller = transform.GetComponent<CharacterController>();
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;
        npcPos = GameObject.FindGameObjectWithTag("NPC_woman").transform.position;
        trail = GameObject.FindGameObjectWithTag("trail");
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position == new Vector3(npcPos.x, transform.position.y, npcPos.z) - Vector3.forward)//走到npc前面停下
        {
            controller.enabled = true;
            navMeshAgent.enabled = false;
            control.nextState = State.Idle;
        }
        if (controller.enabled && (control.curState != State.Skill1 || control.curState != State.Skill2 || control.curState != State.Skill3))
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal == 0 && vertical == 0 && !PlayerInfo.Instance.onTransfer)//没有用键盘，并且也不是在移动平台上
            {
                if (control.target_normalAtk != null)
                {
                    Vector3 tmpTarget = control.target_normalAtk.transform.position;
                    var transform_2d = new Vector2(transform.position.x, transform.position.z);
                    var target_2d = new Vector2(tmpTarget.x, tmpTarget.z);
                    if (Vector2.Distance(transform_2d, target_2d) > 1.5f)//继续走这里可以改成各自的攻击范围，例如法师普攻可以远程攻击
                    {
                        trail.SetActive(false);
                        speed = 7f;
                        control.nextState = State.Run;
                        transform.LookAt(new Vector3(tmpTarget.x, transform.position.y, tmpTarget.z));
                        controller.SimpleMove(transform.forward * speed);//走向目标
                    }
                    else//到达目标位置开始普攻
                    {
                        targetPos.target = transform.position;
                        control.nextState = State.NormalAtk;
                        trail.SetActive(true);
                    }
                }
                else//没有目标怪物
                {
                    var distance = Vector3.Distance(targetPos.target, transform.position);
                    if (distance >= 1f)
                    {
                        trail.SetActive(false);
                        speed = 7f;
                        control.nextState = State.Run;
                        controller.SimpleMove(transform.forward * speed);
                    }
                    else
                    {
                        control.nextState = State.Idle;
                        trail.SetActive(true);
                    }

                }            
                if(PlayerInfo.Instance.modelType==ModelType.Girl)
                {
                    switch (control.curState)
                    {
                        case State.Skill1: 
                            {
                                control.nextState = State.Skill1; 
                                time += Time.deltaTime;
                                if(time>=control.ani["Skill1"].clip.length-0.5f)
                                {
                                    time=0;
                                    control.nextState = State.Idle; 
                                }
                                break; 
                             }
                        case State.Skill2:
                            {
                                control.nextState = State.Skill2;
                                time += Time.deltaTime;
                                if (time >= control.ani["Skill2"].clip.length - 0.5f)
                                {
                                    time = 0;
                                    control.nextState = State.Idle;
                                }
                                break;
                            }
                        case State.Skill3:
                            {
                                control.nextState = State.Skill3;
                                time += Time.deltaTime;
                                if (time >= control.ani["Skill3"].clip.length - 0.5f)
                                {
                                    time = 0;
                                    control.nextState = State.Idle;
                                }
                                break;
                            }
                        case State.Skill4:
                            {
                                control.nextState = State.Skill4;
                                time += Time.deltaTime;
                                if (time >= control.ani["Skill4"].clip.length - 0.5f)
                                {
                                    time = 0;
                                    control.nextState = State.Idle;
                                }
                                break;
                            }
                        case State.NormalAtk:
                            {
                                if (control.target_normalAtk == null)
                                {
                                    control.nextState = State.Idle;
                                }
                                else
                                {
                                    control.nextState = State.NormalAtk;
                                    time += Time.deltaTime;
                                    if (time >= control.ani["Normal_Atk"].clip.length - 0.5f)
                                    {
                                        time = 0;
                                        control.nextState = State.Idle;
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            else
            {
                trail.SetActive(false);
                targetPos.target = transform.position;
                //人物前方为摄像机前方
                transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                if (vertical > 0)//往前跑
                {
                    speed = 7f;
                    control.nextState = State.Run;
                    if (horizontal < 0)//左前方跑
                    {
                        speed = 6f;
                        control.nextState = State.Run_left;
                    }
                    else if (horizontal > 0)//右前方跑
                    {                 
                        speed = 6f;
                        control.nextState = State.Run_right;                     
                    }
                }
                else if(vertical < 0)//后退
                {
                    speed = 1f;
                    control.nextState = State.Walk_back;
                }
                else if (horizontal<0)//左边跑
                {
                    speed = 6f;
                    control.nextState = State.Run_left;
                }
                else if(horizontal>0)//右边跑
                {
                    speed = 6f;
                    control.nextState = State.Run_right;
                }
                else if (horizontal == 0 && vertical==0)
                {
                    control.nextState = State.Idle;
                }

                //键盘移动      
                moveDir = new Vector3(horizontal, 0, vertical);
                ////本地坐标系 转到 世界坐标系
                moveDir = transform.TransformDirection(moveDir);
                ////非着陆状态，不能水平移动，只能垂直下落
                moveDir.y -= gravity * Time.deltaTime*40f;
                controller.Move(moveDir * speed * Time.deltaTime);
            }
        }
        
	}

    public void FindNpc()
    {
        //Click目标改成npc坐标那里，不然停下来之后，会继续往前走
        targetPos.target = new Vector3(npcPos.x, transform.position.y, npcPos.z) - Vector3.forward;
        controller.enabled = false;
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(new Vector3(npcPos.x, transform.position.y, npcPos.z) - Vector3.forward);
        control.nextState = State.Run;
    }
}
