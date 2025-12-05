using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlashScene : MonoBehaviour
{
    public Image fillbar;
    int sceneNumber;

    private void Start()
    {
        OnLoadScene();
    }

   
    void OnLoadScene()
    {
        fillbar.gameObject.SetActive(true);

        if (PlayerPrefs.GetInt(StringManager.LEVEL_CURRENT) <= 1)
            sceneNumber = 1;
        else
            sceneNumber = 2;


        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneNumber);
        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            fillbar.fillAmount = progress;
            yield return null;
        }

    }
}
