using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour {

    Transform movePos;
    GameObject effectPrefab;
    public virtual void Start () {
        effectPrefab = Resources.Load<GameObject>("backtocity");
        movePos = GameObject.FindGameObjectWithTag("Pos3").transform;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var effect = GameObject.Instantiate(effectPrefab);
            effect.transform.SetParent(transform);
            effect.transform.localPosition = new Vector3(0, 0, 3f);
            StartCoroutine(wait(other.transform));
            Destroy(effect.gameObject, 4.9f);
        }
    }

    IEnumerator wait(Transform player)
    {
        yield return new WaitForSeconds(4.9f);
        player.position = movePos.position;
        player.GetComponent<PlayerMove>().targetPos.target = player.position;
    }


}
