using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskComplete : BagPanel 
{
    public Text info;
    public TaskID id = TaskID.KillDragon;
    bool clicked = false;
	void Start()
    {
        type = PanelType.TaskComplete;
    }

    public override void Open()
    {
        transform.localPosition = new Vector3(0, 70f, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
        UpdateTaskComplete();
    }

    void UpdateTaskComplete()
    {
        var index = (int)id - 1;
        info.text = TaskData.Instance.taskList[index].comletedInfo;
    }

    public void Sure()
    {
        sound.Play();
        var index = (int)id - 1;
        if (!clicked)//点击提交任务后进入这里
        {
            if (TaskData.Instance.taskList[index].completed == true)
            {
                info.text = TaskData.Instance.taskList[index].sure.Replace('n', '\n');
                PlayerInfo.Instance.exp += TaskData.Instance.taskList[index].taskReward_exp;//拿到奖励
                if (PlayerInfo.Instance.exp >= PlayerInfo.Instance.needExp)
                {
                    PlayerInfo.Instance.exp -= PlayerInfo.Instance.needExp;
                    PlayerInfo.Instance.LvUp();
                }
                PlayerInfo.Instance.money += TaskData.Instance.taskList[index].taskReward_money;
                MyEventSystem.SendMsg(EventName.UpdateMoney, null);
                TaskData.Instance.taskList[index].got = true;
            }
            else
            {
                info.text = TaskData.Instance.taskList[index].error;
            }
            clicked = true;
        }
        else
        {
            clicked = false;
            base.Close();
        }
    }
}
