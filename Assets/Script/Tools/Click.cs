using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class Click : MonoBehaviour {
    LayerMask mask = ~(1<<5);//不检测第五层，这里因为召唤兽的碰撞体在第五层
    public GameObject effect;
    public Vector3 target;
    bool isMouseDown = false;
    PlayerControl playerControl;
    // Update is called once per frame
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        target = transform.position;
    }
    //按下鼠标左键则发射一条从屏幕到游戏的射线，检测到碰撞的地方是地面，就在这个点生成一个图标
    //然后人物LookAt这个目标，注意Y轴保持不变，角色使用人物控制器的SimpleMove函数往前走就可以完成鼠标点击移动
    void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            transform.GetComponent<CharacterController>().enabled = true;
            transform.GetComponent<NavMeshAgent>().enabled = false;//导航和自己点击的切换
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit, 500, mask);
            //如果碰撞到 并且碰到到的是地面 并且没有碰撞到UI
            if(isCollider && hit.collider.tag=="Ground" && !EventSystem.current.IsPointerOverGameObject())
            {
                playerControl.target_normalAtk = null;
                isMouseDown = true;
                var tmpPoint = new Vector3(hit.point.x, hit.point.y+0.2f, hit.point.z);
                GameObject.Instantiate(effect, tmpPoint, Quaternion.identity);
                LookAtTarget(hit.point);
            }
            else if (isCollider && hit.collider.tag == "Monster" && !EventSystem.current.IsPointerOverGameObject())
            {
                if (GameObject.FindGameObjectWithTag("EnemyHP").transform.localScale == Vector3.zero)//点一下显示怪物信息，再点一下前往攻击
                {
                    playerControl.target_skill = hit.collider.gameObject;
                }
                else
                {
                    if (hit.collider.gameObject != GameObject.FindGameObjectWithTag("EnemyHP").GetComponent<EnemyHPControl>().monster)
                    {
                        playerControl.target_skill = hit.collider.gameObject;//切换到目标怪物
                    }

                    playerControl.target_normalAtk = hit.collider.gameObject;//点到怪的话，普攻目标是怪
                    LookAtTarget(hit.point);
                }
            }
            else if(isCollider && hit.collider.tag == "NPC" && !EventSystem.current.IsPointerOverGameObject())
            {
                if (Vector3.Distance(transform.position, hit.point) <= 10f)//需要跟NPC在一定距离内才能打开面板
                {
                    switch (hit.collider.name)
                    {
                        case "Shoper": { UIManager.instance.ShowPanel(PanelType.ShopPanel); break; }
                        case "Fire": { UIManager.instance.ShowPanel(PanelType.StarUp); break; }
                    }
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
        if(isMouseDown)//按着不动
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);
            if (isCollider && hit.collider.tag == "Ground")
            {
                LookAtTarget(hit.point);
            }
        }
	}

    void LookAtTarget(Vector3 hit)
    {
        target = hit;
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
    }
}
