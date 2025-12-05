using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Floating text display damage numbers champions take
/// </summary>
public class FloatingText : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Vector3 moveDirection;
    private float timer = 0;
    public float speed = 3;
    public float fadeOutTime = 1f;

    private void OnEnable()
    {
        transform.LookAt(Camera.main.transform);
        fadeOutTime = 1f;
        timer = 0;
    }
    void Update()
    {
        this.transform.position = this.transform.position + moveDirection * speed * Time.deltaTime;
        timer += Time.deltaTime;
        float fade = (fadeOutTime - timer) / fadeOutTime;
        canvasGroup.alpha = fade;

        if (fade <= 0)
            ObjectPooler.instance.EnQueueObject("Dame_Text", gameObject);
    }

    public void Init(Vector3 startPosition, string v, Color color)
    {
        this.transform.position = startPosition;
        canvasGroup = this.GetComponent<CanvasGroup>();
        this.GetComponent<TextMeshProUGUI>().text = v;
        this.GetComponent<TextMeshProUGUI>().color = color;
        moveDirection = new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f)).normalized;
        
    }
}
