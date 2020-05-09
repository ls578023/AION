using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : BasePanel {
    SkillItem[] skills;

	void Start () {
        type = PanelType.SkillPanel;
        MyEventSystem.Addlistener(EventName.ShowSkill, ShowImage);
        skills = GetComponentsInChildren<SkillItem>();
	}

    public override void Open()
    {
        transform.localPosition = new Vector3(0, 70f, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
    }

    void ShowImage(object obj)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            int id = skills[i].id;
            if (PlayerInfo.Instance.lv >= SkillData.Instance.skillDic[id].lv)
            {
                skills[i].icon.color = Color.white;
            }
            else
            {
                skills[i].icon.color = Color.gray;
            }

            if (id == 7)//召唤技能
            {
                if (!PlayerInfo.Instance.hasBook)
                {
                    skills[i].icon.color = Color.gray;
                }
                else
                {
                    skills[i].icon.color = Color.white;
                }
            }
        }
    }
}
