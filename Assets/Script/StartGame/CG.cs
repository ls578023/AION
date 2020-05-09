using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CG : MonoBehaviour 
{
    RawImage rawImage;
    AudioSource audioSource;

    MovieTexture movieTexture;

	// Use this for initialization
	void Start () 
	{
        rawImage = this.GetComponent<RawImage>();
        audioSource = this.GetComponent<AudioSource>();

        //重要
        movieTexture = rawImage.texture as MovieTexture;

        movieTexture.Play();
        audioSource.Play();
        StartCoroutine(PlayFinished());
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (movieTexture.isPlaying)
            {
                movieTexture.Pause();
            }
            else
            {
                movieTexture.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            movieTexture.Stop();
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }

    IEnumerator PlayFinished()
    {
        yield return new WaitForSeconds(22.5f);
        movieTexture.Stop();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
