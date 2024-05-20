using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;
using static System.TimeZoneInfo;

public class OriginalPosition : MonoBehaviour
{
    public Vector2 Position;

    void Awake()
    {
        Position = GetComponent<RectTransform>().anchoredPosition;
    }
}

public class YarnCommands : MonoBehaviour
{
    public Image doctorImage;
    public Image bibiImage;
    public Image nurseImage;
    public Image ayannaImage;
    public Animator transitionAnimator;
    public float transitionTime = 1f;
    public DialogueRunner dialogueRunner;
    public CGManager cgManager; // Assign this in the inspector
    public float fadeDuration = 1f; // Duration of the fade'

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        float slideOffset = 100f; // Horizontal offset for the slide; adjust as necessary
        SetAlpha(doctorImage, 0);
        SetAlpha(bibiImage, 0);
        SetAlpha(ayannaImage, 0);
        SetAlpha(nurseImage, 0);

        dialogueRunner.AddCommandHandler("ShowDoctor", () =>
        {
            StartCoroutine(FadeImage(doctorImage, 1, slideOffset, true));
            StartCoroutine(FadeImage(bibiImage, 0, slideOffset, false));
            StartCoroutine(FadeImage(nurseImage, 0, slideOffset, false));
            StartCoroutine(FadeImage(ayannaImage, 0, slideOffset, false));
        });
        dialogueRunner.AddCommandHandler("ShowBibi", () =>
        {
            StartCoroutine(FadeImage(doctorImage, 0, slideOffset, false));
            StartCoroutine(FadeImage(bibiImage, 1, slideOffset, true));
            StartCoroutine(FadeImage(nurseImage, 0, slideOffset, false));
            StartCoroutine(FadeImage(ayannaImage, 0, slideOffset, false));
        });
        dialogueRunner.AddCommandHandler("ShowNurse", () =>
        {
            StartCoroutine(FadeImage(doctorImage, 0, slideOffset, false));
            StartCoroutine(FadeImage(bibiImage, 0, slideOffset, false));
            StartCoroutine(FadeImage(nurseImage, 1, slideOffset, true));
            StartCoroutine(FadeImage(ayannaImage, 0, slideOffset, false));
        });
        dialogueRunner.AddCommandHandler("ShowAyanna", () =>
        {
            StartCoroutine(FadeImage(doctorImage, 0, slideOffset, false));
            StartCoroutine(FadeImage(bibiImage, 0, slideOffset, false));
            StartCoroutine(FadeImage(nurseImage, 0, slideOffset, false));
            StartCoroutine(FadeImage(ayannaImage, 1, slideOffset, true));
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
        SceneManager.LoadScene("EnemyAI"); // Use the correct name for your gameplay scene
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
        float slideOffset = 100f; // Horizontal offset for the slide; adjust as necessary
        StartCoroutine(FadeImage(doctorImage, 0, slideOffset, false));
        StartCoroutine(FadeImage(bibiImage, 0, slideOffset, false));
        StartCoroutine(FadeImage(nurseImage, 0, slideOffset, false));
        StartCoroutine(FadeImage(ayannaImage, 0, slideOffset, false));
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
    private IEnumerator FadeImage(Image image, float targetAlpha, float offset, bool isEntering)
    {
        CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = image.gameObject.AddComponent<CanvasGroup>();
        }

        RectTransform rectTransform = image.GetComponent<RectTransform>();
        float startAlpha = canvasGroup.alpha;
        Vector2 startPosition;
        Vector2 endPosition;

        // Ensure the OriginalPosition component is attached and use its stored position
        OriginalPosition originalPos = image.GetComponent<OriginalPosition>();
        if (originalPos == null)
        {
            originalPos = image.gameObject.AddComponent<OriginalPosition>();
        }

        if (isEntering)
        {
            startPosition = new Vector2(originalPos.Position.x - offset, originalPos.Position.y);
            endPosition = originalPos.Position;
        }
        else
        {
            startPosition = originalPos.Position;
            endPosition = new Vector2(originalPos.Position.x - offset, originalPos.Position.y);
        }

        rectTransform.anchoredPosition = startPosition;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        image.gameObject.SetActive(true);

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        rectTransform.anchoredPosition = endPosition;

        if (targetAlpha == 0)
        {
            image.gameObject.SetActive(false);
        }
        else
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}