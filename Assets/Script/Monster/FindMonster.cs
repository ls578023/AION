using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMonster : Singleton<FindMonster> {

    public List<Monster> monsterList = new List<Monster>();

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Monster")
        {
            var monster = other.GetComponent<Monster>();
            int index = monsterList.IndexOf(monster);
            if (index == -1)
                monsterList.Add(other.GetComponent<Monster>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Monster")
        {
            //monsterList.Remove(other.GetComponent<Monster>());
        }
    }
}
