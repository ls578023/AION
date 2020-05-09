using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel :BasePanel {
    public Text moneyInfo;

	void Start () {
        type = PanelType.BagPanel;
        MyEventSystem.Addlistener(EventName.UpdateMoney, UpdateMoney);

	}
	
    public void UpdateMoney(object obj)
    {
        moneyInfo.text = PlayerInfo.Instance.money.ToString();
    }
    public override void Open()
    {
        UpdateMoney(null);
        transform.localPosition = new Vector3(570f,70f,0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
        BagData.Instance.SaveDataInXml();

    }
}
