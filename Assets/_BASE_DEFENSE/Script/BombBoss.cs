using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBoss : MonoBehaviour
{
    public string tags;
    bool isGrounded;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !isGrounded)
        {
            if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
                audioSource.Play();

            WorldCanvasController.instance.AddDamageText(collision.transform.position + new Vector3(0, 2f, 0), "-100", Color.red);
            collision.gameObject.GetComponent<PlayerControler>().hearth_Player -= 100;
        }

        if (collision.gameObject.tag == "Ally_Gun" && !isGrounded)
        {
            if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
                audioSource.Play();

            WorldCanvasController.instance.AddDamageText(collision.transform.position + new Vector3(0, 2f, 0), "-100", Color.red);
            collision.gameObject.GetComponent<Ally_Gun_Controller>().lives -= 100;
        }

        if (collision.gameObject.tag == "Enemy" && !isGrounded)
        {
            if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
                audioSource.Play();

            WorldCanvasController.instance.AddDamageText(collision.transform.position + new Vector3(0, 2f, 0), "-100", Color.green);
            collision.gameObject.GetComponent<EnemyControler>().lives -= 100;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BossZone")
        {
            if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
                audioSource.Play();

            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.2f);
        isGrounded = true;
        yield return new WaitForSeconds(1);
        ObjectPooler.instance.EnQueueObject(tags, gameObject);
    }
}
