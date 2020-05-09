using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskSystemPanel:BasePanel//角色的任务系统面板 用来查看已经接收到的任务进程
{
    public List<Task> taskList = new List<Task>();
    public Transform infoPanel;
    public GameObject textPrefab;

    void Start()
    {
        type = PanelType.TaskPanelSystem;      
    }

    public override void Open()
    {
        transform.localPosition = new Vector3(0, 70f, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
        UpdateTask();
    }
    void UpdateTask()
    {
        if(taskList.Count==0)
        {
            infoPanel.GetChild(0).GetComponent<Text>().text = "目前没有接收到任务";
        }
        else
        {
            for(int i=0;i<taskList.Count;i++)//遍历角色的任务容器
            {
                if (taskList.Count>infoPanel.childCount)
                {
                    var newText = GameObject.Instantiate(textPrefab);//有几个任务就添加几个选项
                    newText.transform.SetParent(infoPanel);
                }
                if (taskList[i].taskId == TaskID.KillDragon)
                {
                    IssueTask npc=GameObject.FindGameObjectWithTag(taskList[i].startNpc).GetComponent<IssueTask>();
                    infoPanel.GetChild(i).GetComponent<Text>().text = "<color=red>[主线]</color>" + taskList[i]._name + "\n\n"
                        + taskList[i].task + "\n\n冰海骨龙：" + npc.iceDragon.ToString() + " / 5" + "\n地狱炎龙：" + npc.fireDragon.ToString() + " / 5";
                    if(taskList[i].completed==true)
                    {
                        infoPanel.GetChild(i).GetComponent<Text>().text = "<color=red>[主线]</color>" + taskList[i]._name + "\n\n" + "任务已完成";
                    }
                }
                else
                {
                    infoPanel.GetChild(i).GetComponent<Text>().text = "<color=red>[主线]</color>" + taskList[i]._name + "\n\n"
                        + taskList[i].task;
                }
            }
        }
    }
}
