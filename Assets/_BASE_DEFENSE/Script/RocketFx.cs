using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFx : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DestroyFx());
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyControler>().lives -= 100;
            WorldCanvasController.instance.AddDamageText(collision.transform.position + new Vector3(0, 2f, 0), "-100", Color.green);
        }
    }

    IEnumerator DestroyFx()
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPooler.instance.EnQueueObject("RocketFX", gameObject);
    }

}
