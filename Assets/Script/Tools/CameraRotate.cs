using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {
    Transform player;//玩家后背上的点
    Vector3 offset;
    float distance = 0;
    float scrollSpeed = 10f;//滚动滚轮的速度
    float rotateSpeed = 5f;
    bool rotate = false;

    void Start()
    {
        if(PlayerInfo.Instance.modelType==ModelType.Boy)
        {
            player = GameObject.FindGameObjectWithTag("LookPos_boy").transform;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("LookPos_girl").transform;
        }
        transform.LookAt(player.position);
        offset = transform.position - player.position;//玩家到摄像头的偏移量
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = offset + player.position;//摄像机始终与玩家保持一致
        RotateView();
        FarOrNear();
    }
    //设置镜头远近
    void FarOrNear()
    {
        distance = offset.magnitude;//镜头到玩家的距离
        distance -= Input.GetAxis("Mouse ScrollWheel")* scrollSpeed;//获得滑轮滑动的值[-1,1]
        distance = Mathf.Clamp(distance,2f,18f);//限制distance的值
        offset = offset.normalized * distance;//改变远近视角
    }
    //摄像机旋转
    void RotateView()
    {
        if(Input.GetMouseButtonDown(1))
        {
            rotate = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            rotate = false;
        }
        if(rotate)
        {
            //左右旋转
            transform.RotateAround(player.position,player.up, rotateSpeed * Input.GetAxis("Mouse X"));
            //上下旋转
            Vector3 tmpPos = transform.position;
            Quaternion rotation = transform.rotation;
            transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
            //限制上下的旋转
            if(transform.rotation.eulerAngles.x < 3 || transform.rotation.eulerAngles.x > 80)
            {
                transform.position = tmpPos;
                transform.rotation = rotation;
            }
        }
        offset = transform.position - player.position;//更新偏移量 玩家对摄像机
    }
}
