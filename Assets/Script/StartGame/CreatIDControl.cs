using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreatIDControl : MonoBehaviour {

    InputField ID;
    InputField password;
    InputField password2;
    GameObject warning;
    Text warningText;

	// Use this for initialization
	void Start () {
        ID = GameObject.FindGameObjectWithTag("InputID").GetComponent<InputField>();
        password = GameObject.FindGameObjectWithTag("InputPassWord").GetComponent<InputField>();
        password2 = GameObject.FindGameObjectWithTag("InputPassWord2").GetComponent<InputField>();
        warningText = GameObject.FindGameObjectWithTag("WarningText").GetComponent<Text>();
        warning = GameObject.FindGameObjectWithTag("Warning");
        warning.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Check(string context)
    {

    }

    public void Back()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Creat()
    {
        if (ID.text != "" && password.text != "")
        {
            if (password.text == password2.text)
            {
                if (GetData.Instance.IfCreatIDSuccess(ID.text, password.text))
                {
                    SceneManager.LoadScene(1, LoadSceneMode.Single);
                }
                else
                {
                    warning.SetActive(true);
                    warningText.text = "账号已被注册";
                    ID.text = "";
                    password.text = "";
                    password2.text = "";
                }
            }
            else
            {
                warning.SetActive(true);
                warningText.text = "两次密码不一致";
                password.text = "";
                password2.text = "";
            }
        }
        else
        {
            warning.SetActive(true);
            warningText.text = "请输入有效信息";
            ID.text = "";
            password.text = "";
            password2.text = "";
        }
    }

    public void ColseWarning()
    {
        warning.SetActive(false);
    }
}
