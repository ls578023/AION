using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    AsyncOperation option;

    Image loadingBar;
    Text progress;


    // Use this for initialization
    void Start()
    {
        loadingBar = GameObject.FindGameObjectWithTag("LoadingBar").GetComponent<Image>();
        loadingBar.fillAmount = 0;
        progress = transform.Find("Progress").GetComponent<Text>();

        StartCoroutine(AsyncLoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        progress.text = "加载进度：" + (int)(option.progress * 100) + "%";
        loadingBar.fillAmount = option.progress;
    }

    IEnumerator AsyncLoadScene()
    {
        option = SceneManager.LoadSceneAsync(5);
        yield return option;
    }

    //void Update()
    //{
    //    loadingBar.fillAmount +=0.03f;
    //    if(loadingBar.fillAmount>=1)
    //    {
    //        loadingBar.fillAmount = 1;
    //    }
    //    progress.text = "加载进度：" + (int)(loadingBar.fillAmount * 100) + "%";
    //}

    //IEnumerator AsyncLoadScene()
    //{
    //    yield return new WaitForSeconds(0.6f);
    //    SceneManager.LoadSceneAsync(5);      
    //}
}
