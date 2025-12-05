using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BombDeadZone : MonoBehaviour
{
    public List<EnemyControler> enmyList;
    public float time;
    public ParticleSystem mineFx;
    public Color zoneImgae;
    public Image imageMine;
    AudioSource nukeEx;

   

    void Awake()
    {
        imageMine = GetComponent<Image>();
        nukeEx = transform.parent.parent.parent.Find("NukeExplosionRed").GetComponent<AudioSource>();
    }

    void onEnable()
    {

        zoneImgae.a = 0;
        imageMine.color = zoneImgae;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyControler enemy = other.gameObject.GetComponent<EnemyControler>();
            if (enemy.mute)
                enmyList.Add(enemy);
        }
    }

    private void Update()
    {
        if (time <= 0)
        {
            if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
                nukeEx.Play();
            mineFx.Play();
            DestroyAllEnemy();
        }
        else
        { 
            time -= Time.deltaTime; 
            zoneImgae.a += 0.1f * Time.deltaTime;
            imageMine.color = zoneImgae;
        }

        

    }

    void DestroyAllEnemy()
    {
        for (int i = 0; i < enmyList.Count; i++)
        {
            WorldCanvasController.instance.AddDamageText(enmyList[i].gameObject.transform.position + new Vector3(0, 2f, 0), "-" + enmyList[i].lives.ToString(), Color.green);
            enmyList[i].lives = 0;
        }
        time = 10;
        findAllObject();
        gameObject.SetActive(false);
        enmyList.Clear();

    }

    void findAllObject()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject potentialTarget in enemies)
        {
            potentialTarget.GetComponent<EnemyControler>().mute = false;
        }

    }

}
