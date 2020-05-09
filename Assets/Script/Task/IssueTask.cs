using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueTask : MonoBehaviour //点击NPC发布任务
{
    TaskID id = TaskID.KillDragon;
    public int iceDragon=0;
    public int fireDragon=0;
    public void OnMouseDown()
    {
        if (!TaskData.Instance.taskList[0].got)//任务1未完成，进入这个
        {
            //任务既没有接取，也没有完成，就打开任务发布面板
            if (TaskData.Instance.taskList[0].accepted == false && TaskData.Instance.taskList[0].completed == false)
            {
                GameObject.FindGameObjectWithTag("UI_TaskPanel").GetComponent<TaskPanel>().id = id;
                UIManager.instance.ShowPanel(PanelType.TaskPanel);//点击之后打开面板
            }
            //任务被接取
            else if (TaskData.Instance.taskList[0].accepted == true)
            {
                GameObject.FindGameObjectWithTag("UI_TaskComplete").GetComponent<TaskComplete>().id = id;
                UIManager.instance.ShowPanel(PanelType.TaskComplete);//点击之后打开面板

            }
        }
        else//任务1已经完成，开始任务2
        {
            if (TaskData.Instance.taskList[1].accepted == false && TaskData.Instance.taskList[1].completed == false)
            {
                GameObject.FindGameObjectWithTag("UI_TaskPanel").GetComponent<TaskPanel>().id = TaskID.FindBoss;//找盟主任务
                UIManager.instance.ShowPanel(PanelType.TaskPanel);//点击之后打开面板
            }
            //任务被接取，但是没有完成
            else if (TaskData.Instance.taskList[0].accepted == true)
            {
                GameObject.FindGameObjectWithTag("UI_TaskComplete").GetComponent<TaskComplete>().id = TaskID.FindBoss;
                UIManager.instance.ShowPanel(PanelType.TaskComplete);//点击之后打开面板
            }
        }

    }

}
