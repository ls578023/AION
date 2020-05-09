using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour {
    public int id = 0;
    public Image icon;
    public Text skillName;
    public Text info;

    public void InitSkillInfo()
    {
        skillName.text = SkillData.Instance.skillDic[id]._name;
        info.text = SkillData.Instance.skillDic[id].info;
        icon.sprite = ResourcesManager.Load<Sprite>(SkillData.Instance.skillDic[id].path);//图片加载
        icon.transform.GetComponent<DragSkill>().id = id;//标记图片信息，便于查找是哪种技能       
        icon.color = Color.gray;
    }
}
