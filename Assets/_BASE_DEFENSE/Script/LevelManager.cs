using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    TextMeshPro baseName;
    TextMeshPro baseNext;

    private void Awake()
    {
        baseName = transform.Find("/Base/Base_Name").GetComponent<TextMeshPro>();
        baseNext = transform.Find("/BaseNext/Base_Name").GetComponent<TextMeshPro>();

    }

    private void Start()
    {
        baseName.text = "BASE " + PlayerPrefs.GetInt(StringManager.LEVEL_CURRENT).ToString();
        baseNext.text = "BASE " + (PlayerPrefs.GetInt(StringManager.LEVEL_CURRENT) + 1).ToString();
    }
}
