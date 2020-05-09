using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TabType
{
    None,
    All,
    Equiment,
    Drug,
    Task,
    Others
}
public class InitBagUnit : MonoBehaviour 
{

    int row = 6;
    int col = 5;
    Dictionary<Vector2,BagUnit> unitInfo=new Dictionary<Vector2,BagUnit>();
    GameObject bagUnitPrefab;
    public TabType curType = TabType.None;

    void Awake()
    {
        bagUnitPrefab = Resources.Load<GameObject>("Prefabs\\BagUnit");
        StartCoroutine(InitBagTab());
    }

    IEnumerator InitBagTab()
    {
        for(int i=0;i<row;i++)
        {
            for(int j=0;j<col;j++)
            {
                var obj = Instantiate(bagUnitPrefab);
                obj.GetComponent<BagUnit>().row = i;
                obj.GetComponent<BagUnit>().col = j;
                obj.name = "BagUnit_" + i.ToString() + "_" + j.ToString();
                obj.transform.SetParent(transform);
                obj.tag = "BagUnit";
                unitInfo.Add(new Vector2(i, j), obj.GetComponent<BagUnit>());
            }
        }
        yield return null;
    }
}
