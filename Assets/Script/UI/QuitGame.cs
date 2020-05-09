using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame :BasePanel
{
    private void Start()
    {
        type = PanelType.QuitGame;
    }

    public override void Open()
    {
        transform.localPosition = new Vector3(0, 70f, 0);
        thisCanvasGroup.alpha = 1;
        thisCanvasGroup.blocksRaycasts = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
