using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvasController : MonoBehaviour
{
    public static WorldCanvasController instance;

    public GameObject worldCanvas;
    public GameObject floatingTextPrefab;
    ObjectPooler objectPooler;

    private void Awake()
    {
        instance = this;
        objectPooler = ObjectPooler.instance;
    }

    private void Start()
    {
        worldCanvas.transform.LookAt(Camera.main.transform);
    }

    public void AddDamageText(Vector3 position, string v, Color color)
    {
        //GameObject go = Instantiate(floatingTextPrefab);
        if (objectPooler.poolDic["Dame_Text"].Count > 0)
        {
            GameObject go = objectPooler.SpawnFormPool("Dame_Text", position, Quaternion.identity);
            go.transform.SetParent(worldCanvas.transform);
            go.GetComponent<FloatingText>().Init(position, v, color);
        }

        
    }


}
