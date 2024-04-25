using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGManager : MonoBehaviour
{
    [System.Serializable]
    public struct CharacterGraphic
    {
        public string name;
        public Image image;
    }

    public List<CharacterGraphic> characterGraphics;
    public float fadeDuration = 1f; // Duration of the fade in seconds

    private void Start()
    {
        // Hide all CGs initially
        foreach (var cg in characterGraphics)
        {
            SetAlpha(cg.image, 0);
        }
    }

    public void FadeInCG(string characterName)
    {
        var cg = characterGraphics.Find(c => c.name == characterName);
        if (cg.image != null)
        {
            Debug.Log($"Fading in {characterName}");
            StartCoroutine(FadeTo(cg.image, 1f)); // Fade in
        }
        else
        {
            Debug.LogWarning($"Image not found for {characterName}");
        }
    }

    public void FadeOutCG(string characterName)
    {
        var cg = characterGraphics.Find(c => c.name == characterName);
        if (cg.image != null)
        {
            StartCoroutine(FadeTo(cg.image, 0f)); // Fade out
        }
    }

    // Make sure to set blocksRaycasts to false after fading out.
    private IEnumerator FadeTo(Image image, float targetAlpha)
    {
        CanvasGroup cg = image.GetComponent<CanvasGroup>();
        if (!cg) cg = image.gameObject.AddComponent<CanvasGroup>();

        float startAlpha = cg.alpha;
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }

        cg.alpha = targetAlpha; // Ensure target alpha is set exactly
        cg.blocksRaycasts = targetAlpha > 0.01;
    }


    private void SetAlpha(Image image, float alpha)
    {
        CanvasGroup cg = image.GetComponent<CanvasGroup>();
        if (!cg) cg = image.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = alpha;
    }
}