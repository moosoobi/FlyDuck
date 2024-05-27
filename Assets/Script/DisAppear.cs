using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisAppear : MonoBehaviour
{
    public float fadeDuration = 2.0f; // 사라지는 데 걸리는 시간
    public Collider2D portalCollider;
    public Renderer portalRenderer;
    private Color initialColor;

    void Start()
    {

        if (portalRenderer != null)
        {
            initialColor = portalRenderer.material.color;
        }
    }


    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOutAndDisable());
        }
    }


    private IEnumerator FadeOutAndDisable()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            if (portalRenderer != null)
            {
                Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
                portalRenderer.material.color = newColor;
            }
            yield return null;
        }

        if (portalRenderer != null)
        {
            portalRenderer.material.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        }
        portalCollider.enabled = false;
        yield return new WaitForSeconds(1.0f);
        portalCollider.enabled = true;
        portalRenderer.material.color = initialColor;
    }
}
