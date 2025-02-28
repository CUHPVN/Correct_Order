using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] float magnitude = 0.1f;
    public float DestroyTime = 2f;
    public Vector3 RandomizeIntensity = new Vector3(1, 0, 0);
    private Color localColor = Color.white;
    private TMP_Text popupText;

    private void Start()
    {
    }
    void OnEnable()
    {
        popupText = GetComponent<TMP_Text>();
        transform.localPosition += new Vector3(
            Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
            Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z)
        );
        StartCoroutine(FadeOut());
        StartCoroutine(Shake(DestroyTime/4,magnitude));
        Invoke(nameof(Despawn), DestroyTime);
    }
    IEnumerator FadeOut()
    {
        popupText.color = localColor;
        float startAlpha = popupText.color.a;
        Color fadeColor = popupText.color;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / DestroyTime)
        {
            fadeColor.a = Mathf.Lerp(startAlpha, 0, t);
            popupText.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 0;
        popupText.color = fadeColor;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            originalPos = transform.position;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        //transform.position = originalPos;
    }
    void Despawn()
    {
        SpawnManager.Instance.Despawn(this.gameObject.transform);
    }
}
