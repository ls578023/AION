using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel : BasePanel //任务发布面板，用来展示当前任务
{
    public TaskID id;
    public Text _name;
    public Text info;
    public Text task;
    public Text reward;
    TaskSystemPanel taskSystem;

	void Start () {
        type = PanelType.TaskPanel;
        id = TaskID.KillDragon;//本NPC颁布的是杀龙任务
        MyEventSystem.Addlistener(EventName.UpdateTaskPanel, UpdateTaskPanel);
        taskSystem = GameObject.FindGameObjectWithTag("UI_TaskSystem").GetComponent<TaskSystemPanel>();
	}

    public void AcceptTask()
    {
        sound.Play();
        var index = (int)id - 1;
        TaskData.Instance.taskList[index].accepted = true;//任务已经被接收
        taskSystem.taskList.Add(TaskData.Instance.taskList[index]);//把这个任务加入到角色的任务系统
        base.Close();
    }

    public override void Open()
    {
        transform.localPosition = new Vector3(0, 70f, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
        UpdateTaskPanel(null);
    }

    void UpdateTaskPanel(object obj)//根据ID，改变面板的内容
    {
        var index = (int)id - 1;
        _name.text = TaskData.Instance.taskList[index]._name;
        info.text = TaskData.Instance.taskList[index].info.Replace('n', '\n');
        task.text = TaskData.Instance.taskList[index].task;
        reward.text = "金币+" + TaskData.Instance.taskList[index].taskReward_money.ToString() + "，"
            + "经验+" + TaskData.Instance.taskList[index].taskReward_exp.ToString();
    }
	

}
