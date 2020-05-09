using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;//file相关

public enum TaskType
{
    None,
    KillMonster,
    FindNpc
}

public enum TaskID
{
    None,
    KillDragon,
    FindBoss
}
public class TaskData//从xml读取任务信息
{
    public List<Task> taskList = new List<Task>();

    void LoadXml()
    {

        TextAsset xml = ResourcesManager.Load<TextAsset>(@"TaskData");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml.text);
        XmlNodeList childList = xmlDoc.SelectSingleNode("root").ChildNodes;//所有的“task”
        foreach (var task in childList)
        {
            XmlElement element = task as XmlElement;
            Task newTask = new Task();
            newTask.taskId = (TaskID)(int.Parse(element.GetAttribute("id")));
            newTask._name = element.GetAttribute("name");
            newTask.taskType = (TaskType)int.Parse(element.GetAttribute("type"));
            newTask.info = element.GetAttribute("info");
            newTask.task = element.GetAttribute("task");
            newTask.taskReward_money = int.Parse(element.GetAttribute("money"));
            newTask.taskReward_exp = int.Parse(element.GetAttribute("exp"));
            newTask.startNpc = element.GetAttribute("startNpc");
            newTask.endNpc = element.GetAttribute("endNpc");
            newTask.accepted = false;
            newTask.completed = false;
            newTask.comletedInfo = element.GetAttribute("complete");
            newTask.sure = element.GetAttribute("sure");
            newTask.error = element.GetAttribute("error");
            newTask.got = false;//任务完成
            taskList.Add(newTask);
        }
    }
    static TaskData _instance =null;
    TaskData()
    {
        LoadXml();
    }
    public static TaskData Instance
    {
        get{
            if(_instance==null)
            {
                _instance=new TaskData();
            }
            return _instance;
        }
    }
}

public class Task
{
    public TaskID taskId;
    public string _name;
    public TaskType taskType;
    public string info;//任务发布信息
    public string task;//任务具体内容
    public int taskReward_money;//任务金币奖励
    public int taskReward_exp;//任务经验奖励
    public string startNpc;//发布任务的NPC的tag
    public string endNpc;//完成任务的NPC
    public bool accepted;//这个任务是否已经被接收
    public bool completed;//任务是否完成
    public string comletedInfo;//询问任务是否完成的对话
    public string sure;//确认任务完成后的对话
    public string error;
    public bool got;
}