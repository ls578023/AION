using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PanelType
{
    None,
    BagPanel, //背包界面
    EquimentPanel,  //角色面板 
    AttributePanel,  //属性面板
    SkillPanel,  //技能面板
    TaskPanel, //任务发布面板
    TaskPanelSystem,//任务系统面板
    TaskComplete,//任务完成面板
    ShopPanel,
    TipPanel,//提示面板
    QuitGame,
    RewardPanel,//奖励拾取面板
    StarUp,//升星面板
    Info//游戏说明面板
}

public class BasePanel : MonoBehaviour {

    public PanelType type = PanelType.None;
    public AudioSource sound;
    public CanvasGroup _canvasGroup;

    public CanvasGroup thisCanvasGroup
    {
        get
        {
            //如果有，直接等于
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)//如果没有这个组件，就添加一个
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                _canvasGroup.alpha = 0;
            }
            return _canvasGroup;
        }
    }
	
    public virtual void Open()
    {
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
    }

    public virtual void Close()
    {
        if (GameObject.FindGameObjectWithTag("Tip")!=null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Tip"));
        }
        thisCanvasGroup.alpha = 0;
        thisCanvasGroup.blocksRaycasts = false;
        thisCanvasGroup.transform.localPosition = Vector3.zero;//都放在中间
    }

}
