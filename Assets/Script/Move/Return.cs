using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = GameObject.FindGameObjectWithTag("Pos3").transform.position;
    }


}
