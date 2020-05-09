using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float time = 0;
    void LateUpdate()
    {
        cameraShake();
    }

    void cameraShake()
    {
        if(time>0)
        {
            time -= Time.deltaTime;
            Camera.main.rect = new Rect(0.04f * (-1f + 2f * Random.value) * Mathf.Pow(time, 2),
                0.04f * (-1f + 2f * Random.value) * Mathf.Pow(time, 2), 1f, 1f);
        }
        else
        {
            time = 0;
        }
    }
}
