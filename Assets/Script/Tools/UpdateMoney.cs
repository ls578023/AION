using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMoney : MonoBehaviour {
    Text money;
	// Use this for initialization
	void Start () {
		money=GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		money.text=PlayerInfo.Instance.money.ToString();
	}
}
