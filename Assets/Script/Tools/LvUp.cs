using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvUp : MonoBehaviour {
    Text lv;
	// Use this for initialization
	void Start () {
        lv = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        lv.text = PlayerInfo.Instance.lv.ToString();

    }
}
