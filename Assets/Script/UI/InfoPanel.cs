using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : BasePanel {

    public Scrollbar scrollbar;
	void Start () {
        type = PanelType.Info;
	}

    public override void Open()
    {
        scrollbar.value = 1;
        transform.localPosition = new Vector3(0, 70f, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
    }
}
