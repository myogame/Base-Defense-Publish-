using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLine : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            WorldCanvasController.instance.AddDamageText(other.transform.position + new Vector3(0, 2f, 0), "-100", Color.red);
            other.gameObject.GetComponent<PlayerControler>().hearth_Player -= 100;
        }

        if (other.gameObject.tag == "Ally_Gun")
        {
            WorldCanvasController.instance.AddDamageText(other.transform.position + new Vector3(0, 2f, 0), "-100", Color.red);
            other.gameObject.GetComponent<Ally_Gun_Controller>().lives -= 100;
        }

        if (other.gameObject.tag == "Enemy")
        {
            WorldCanvasController.instance.AddDamageText(other.transform.position + new Vector3(0, 2f, 0), "-100", Color.green);
            other.gameObject.GetComponent<EnemyControler>().lives -= 100;
        }

    }
}
