using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;//file相关

public enum ModelType
{
    Boy,
    Girl
}
public class SelectRole : Singleton<SelectRole> 
{
    public Animator animator_boy;
    public Animation animation_girl;
    public Button left;
    public Button right;
    public Button enter;
    bool once = true;
    public ModelType modelType = ModelType.Boy;

	void Start () {
        //默认第一个显示男角色
        animator_boy.gameObject.SetActive(true);
        animation_girl.gameObject.SetActive(false);
        left.onClick.AddListener(PlayerBoy);
        right.onClick.AddListener(PlayerGirl);
        enter.onClick.AddListener(EnterGame);
	}
	

	void Update () {
        //开头播放一遍动画
        if (once)
        {
            animator_boy.SetTrigger("Play");
            once = false;
        }
	}

    void PlayerBoy()
    {
        modelType = ModelType.Boy;
        animator_boy.gameObject.SetActive(true);
        animation_girl.gameObject.SetActive(false);
        animator_boy.SetTrigger("Play");
    }

    void PlayerGirl()
    {
        modelType = ModelType.Girl;
        animator_boy.gameObject.SetActive(false);
        animation_girl.gameObject.SetActive(true);
        string[] str = { "Change_Atk", "Skill1", "Normal_Atk", "Skill3", "Change_Nomal", "Normal_Idle" };
        StartCoroutine(PlayAnimation(str));
    }

    void EnterGame()
    {
        LoadXml.Instance.SaveXml();
        SceneManager.LoadSceneAsync(4);   
    }

    //连续播放一段动画
    IEnumerator PlayAnimation(string[] _str)
    {
        string[] str = _str;
        for(int i=0;i<str.Length-1;i++)
        {
            //通过索引器 获得srcAnimationName的 "动画节点"的状态
            AnimationState animState = animation_girl[str[i]];
            //通过当前 "动画节点"的状态 得到正在播放的动画片段
            AnimationClip clip = animState.clip;
            float length = clip.length;
            yield return new WaitForSeconds(length);
            animation_girl.CrossFade(str[i + 1]);
        }
    }
}
