using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager 
{
    //如果说当前资源之前没有加载过的话，那么加载一遍，并且将该资源记录下来
    //如果当前资源之前 加载过的话，那么直接使用之前 加载过的资源
    static Dictionary<string, GameObject> resDic = new Dictionary<string, GameObject>();

    //非泛型用于加载预置体对象
    public static GameObject Load(string path)
    {
        if (resDic.ContainsKey(path))
        {
            return resDic[path];
        }
        GameObject go = Resources.Load(path) as GameObject;
        resDic[path] = go;

        return go;
    }

    static Hashtable resTable = new Hashtable();

    public static T Load<T>(string path) where T : UnityEngine.Object
    {
        if (resTable.ContainsKey(path))
        {
            return resTable[path] as T;
        }

        T t = Resources.Load<T>(path);
        resTable[path] = t;

        return t;
    }
}
