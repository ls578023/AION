using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUnitControl : MonoBehaviour {

    GameObject[] skillUnits;
	void Start () {
        skillUnits = GameObject.FindGameObjectsWithTag("SkillUnit");//全部快捷栏
	}

    public RectTransform GetRect()
    {
        for (int i = 0; i < skillUnits.Length; i++)
        {
            //鼠标是否在单元格范围内
            if (RectTransformUtility.RectangleContainsScreenPoint(skillUnits[i].GetComponent<RectTransform>(), Input.mousePosition))
            {
                return skillUnits[i].GetComponent<RectTransform>();
            }
        }
        return null;
    }
}
