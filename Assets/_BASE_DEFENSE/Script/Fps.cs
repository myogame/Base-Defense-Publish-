using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fps : MonoBehaviour
{

    public TextMeshProUGUI fpsText;
    public float deltaTime;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;
    }
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }
}
