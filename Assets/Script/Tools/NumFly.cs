using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NumFly : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.forward = Camera.main.transform.forward;
	}
}
