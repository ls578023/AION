using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : Singleton<TipPanel>
{
    public AudioSource sound;
    public Text info;
    public CanvasGroup thisCanvasGroup;

    public void Open()
    {
        transform.SetAsLastSibling();
        transform.localPosition = new Vector3(0, 70f, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
    }
    public void Close()
    {
        sound.Play();
        if (GameObject.FindGameObjectWithTag("Tip") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Tip"));
        }
        thisCanvasGroup.alpha = 0;
        thisCanvasGroup.blocksRaycasts = false;
        thisCanvasGroup.transform.localPosition = Vector3.zero;//都放在中间
    }
}
