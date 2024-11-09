using System.Collections;
using UnityEngine;

public class SimpleFadeOut : MonoBehaviour
{
    [SerializeField]
    private float minFadeDuration = 2f;  // Minimum duration of the fade-out effect

    [SerializeField]
    private float maxFadeDuration = 5f;  // Maximum duration of the fade-out effect

    private float fadeDuration;  // Randomized fade-out duration

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Randomize fade duration between the min and max values
        fadeDuration = Random.Range(minFadeDuration, maxFadeDuration);

        // Get the SpriteRenderer component on the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start the fade-out coroutine
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        // Gradually fade out over the fadeDuration
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Destroy the parent GameObject after the fade-out is complete
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            // If there is no parent, just destroy this GameObject
            Destroy(gameObject);
        }
    }
}
