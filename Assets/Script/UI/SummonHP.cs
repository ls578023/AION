using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonHP : Singleton<SummonHP>
{

    public Image hp_bar;
    public Text num;
    public CanvasGroup thisCanvasGroup;

    public void Open()
    {
        transform.localPosition = new Vector3(-730, 444, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        thisCanvasGroup.alpha = 0;
        thisCanvasGroup.blocksRaycasts = false;
    }
}
