using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangmuMove : MonoBehaviour {
    Transform end;
    public bool beginMove = false;
    Transform player;
    Transform pos4;
	// Use this for initialization
	void Start () {
        end = GameObject.FindGameObjectWithTag("BossBorn").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pos4 = GameObject.FindGameObjectWithTag("Pos4").transform;
    }
	
	// Update is called once per frame
	void Update () {
		if(player.position== pos4.position)
        {
            beginMove = true;
        }
        if(beginMove)
        {
            transform.position = Vector3.MoveTowards(transform.position,end.position, 5f);
        }
        if(transform.position== end.position)
        {

        }
	}
}
