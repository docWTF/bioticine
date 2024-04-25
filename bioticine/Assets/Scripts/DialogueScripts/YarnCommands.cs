using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;
using static System.TimeZoneInfo;

public class YarnCommands : MonoBehaviour
{
    public Image doctorImage;
    public Image bibiImage;
    public Animator transitionAnimator;
    public float transitionTime = 1f;
    public Image CharBoxBG;
    public Image FrameBox;
    public DialogueRunner dialogueRunner;
    public CGManager cgManager; // Assign this in the inspector
    public float fadeDuration = 1f; // Duration of the fade'

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();

        SetAlpha(doctorImage, 0);
        SetAlpha(bibiImage, 0);
        SetAlpha(CharBoxBG, 0);
        SetAlpha(FrameBox, 0);

        dialogueRunner.AddCommandHandler("ShowFrame", () =>
        {
            StartCoroutine(FadeImage(CharBoxBG, 1));
            StartCoroutine(FadeImage(FrameBox, 1));
        });
        dialogueRunner.AddCommandHandler("ShowDoctor", () =>
        {
            StartCoroutine(FadeImage(doctorImage, 1));
            StartCoroutine(FadeImage(bibiImage, 0));
        });
        dialogueRunner.AddCommandHandler("ShowBibi", () =>
        {
            StartCoroutine(FadeImage(doctorImage, 0));
            StartCoroutine(FadeImage(bibiImage, 1));
        });
        dialogueRunner.AddCommandHandler("FadeOutAll", FadeOutAllCharacters);
        dialogueRunner.AddCommandHandler<string>("FadeInCG", cgManager.FadeInCG);
        dialogueRunner.AddCommandHandler<string>("FadeOutCG", cgManager.FadeOutCG);
        dialogueRunner.AddCommandHandler("SwitchToGameplay",
            () => StartCoroutine(SwitchToGameplay()));
        dialogueRunner.AddCommandHandler("SwitchToMenu",
           () => StartCoroutine(SwitchToMenu()));
    }
    private IEnumerator SwitchToGameplay()
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(transitionTime);
        }
        // After the fade-out, load the gameplay scene
        SceneManager.LoadScene("Enemy"); // Use the correct name for your gameplay scene
    }

    private IEnumerator SwitchToMenu()
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(transitionTime);
        }
        // After the fade-out, load the gameplay scene
        SceneManager.LoadScene("MainMenu"); // Use the correct name for your gameplay scene
    }

    private void FadeOutAllCharacters()
    {
        StartCoroutine(FadeImage(doctorImage, 0));
        StartCoroutine(FadeImage(bibiImage, 0));
        StartCoroutine(FadeImage(CharBoxBG, 0));
        StartCoroutine(FadeImage(FrameBox, 0));
    }

    // Set the initial alpha of an image
    private void SetAlpha(Image image, float alpha)
    {
        CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = image.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = alpha;
    }

    // Coroutine to fade an image to a target alpha
    private IEnumerator FadeImage(Image image, float targetAlpha)
    {
        // Ensure the CanvasGroup component exists
        CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = image.gameObject.AddComponent<CanvasGroup>();
        }

        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        // Make the image non-interactable and non-blocking of raycasts
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // If we're fading in, make sure the object is active
        if (targetAlpha > startAlpha)
        {
            image.gameObject.SetActive(true);
            Debug.Log($"Fading in {image.gameObject.name}");
        }

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha; // Ensure the final alpha is set

        // If we've faded out, deactivate the object to prevent it from blocking UI
        if (targetAlpha == 0)
        {
            image.gameObject.SetActive(false);
            Debug.Log($"Faded out and deactivated {image.gameObject.name}");
        }
        else
        {
            // Make the image interactable and blocking of raycasts after it has faded in
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}