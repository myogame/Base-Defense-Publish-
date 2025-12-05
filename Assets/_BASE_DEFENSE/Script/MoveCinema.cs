using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveCinema : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOMoveZ(-17, 60).OnComplete(() =>
        {
            StartCoroutine(CameraMove());
        });
    }

    private void OnDisable()
    {
        transform.DOKill();
        transform.position = new Vector3(-7.19f, 1.95f, 25);
    }

    IEnumerator CameraMove()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 15);
        yield return new WaitForSeconds(10);
        transform.position = new Vector3(-7, -2, 4.3f);
        yield return new WaitForSeconds(10);
        transform.position = new Vector3(-7.33f, 2.49f, 1.8f);
        yield return new WaitForSeconds(10);
        transform.position = new Vector3(-7.26f, 1.88f, 14.82f);
        transform.DORotate(new Vector3(0, 0, 360), 60);
    }
}
