using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    public AudioSource sound;
    public PanelType type;
	void Start () {
        transform.GetComponent<Button>().onClick.AddListener(Open_Close);
        sound = GetComponent<AudioSource>();

    }

    public void Open_Close()
    {
        UIManager.instance.ShowPanel(type);
        sound.Play();
    }

    static ButtonManager _instance = null;
    ButtonManager()
    {
    }

    public static ButtonManager instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new ButtonManager();
            }
            return _instance;
        }
    }
}
