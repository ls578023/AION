using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        //该脚本组件对象的引用
        _instance = this as T;

        //防止切换新场景后, 该游戏对象被销毁  
        //DontDestroyOnLoad(this.gameObject);
    }

}
