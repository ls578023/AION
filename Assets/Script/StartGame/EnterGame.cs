using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterGame : MonoBehaviour {

    //Dropdown dropDown;
    InputField ID;
    InputField password;    
    GameObject warning;

	void Start () {
        //账号密码存一个结构体，区服的名字作为字典的键，结构体做值，三个数值对应（需要重写大量代码，时间充足再做）
        //dropDown = GameObject.FindGameObjectWithTag("Dropdown").GetComponent<Dropdown>();
        //获取两个输入框并且为其添加监听事件
        ID = GameObject.FindGameObjectWithTag("ID").GetComponent<InputField>();
        password = GameObject.FindGameObjectWithTag("Password").GetComponent<InputField>();
        warning = GameObject.FindGameObjectWithTag("Warning");
        warning.SetActive(false);
	}

    public void Enter()
    {
        //判断账号密码是否正确
        if (GetData.Instance.CanEnterGame(ID.text, password.text))
        {
            GetData.Instance.nowID = ID.text;//记录当前账号密码
            GetData.Instance.nowPassword = password.text;
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
        else
        {
            warning.SetActive(true);
            password.text = "";
        }
    }

    public void Close()
    {
        Application.Quit();
    }

    public void CreatID()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void ColseWarning()
    {
        warning.SetActive(false);
    }
}
