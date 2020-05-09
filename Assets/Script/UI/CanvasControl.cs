using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIManager.instance.GetPanel(PanelType.BagPanel).Close(); 
        UIManager.instance.GetPanel(PanelType.EquimentPanel).Close();
        UIManager.instance.GetPanel(PanelType.SkillPanel).Close(); //开局直接先加载面板
        UIManager.instance.GetPanel(PanelType.TaskPanel).Close();
        UIManager.instance.GetPanel(PanelType.TaskPanelSystem).Close();
        UIManager.instance.GetPanel(PanelType.TaskComplete).Close();
        UIManager.instance.GetPanel(PanelType.ShopPanel).Close();
        UIManager.instance.GetPanel(PanelType.StarUp).Close();
        UIManager.instance.GetPanel(PanelType.QuitGame).Close();
        UIManager.instance.GetPanel(PanelType.Info).Close();
        UIManager.instance.GetPanel(PanelType.Info).Open();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
