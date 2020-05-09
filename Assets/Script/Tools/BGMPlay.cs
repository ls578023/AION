using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlay : MonoBehaviour {
    public GameObject BGM;
    GameObject myBGM;
	// Use this for initialization
	void Start () {
        myBGM = GameObject.FindGameObjectWithTag("BGM");
        if(myBGM == null)
        {
            myBGM = GameObject.Instantiate(BGM);
        }

    }

}
