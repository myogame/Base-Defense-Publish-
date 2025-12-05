using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{

    Button okBtn;

    private void Awake()
    {
        okBtn = transform.Find("BG/Button_Claim").GetComponent<Button>();
        okBtn.onClick.AddListener(() => LoadScreen());
    }

    void LoadScreen()
    {
        SoundManager.ins.PlaySound(0);
        StringManager.ResetLevel();

        PlayerPrefs.SetInt(StringManager.LEVEL_CURRENT, PlayerPrefs.GetInt(StringManager.LEVEL_CURRENT) + 1);

        if (PlayerPrefs.GetInt(StringManager.LEVEL_CURRENT) <= 2)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
