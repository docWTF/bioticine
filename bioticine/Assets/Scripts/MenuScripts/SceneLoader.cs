using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    public Animator transitionAnimator; // Assign this in the inspector
    public float transitionTime = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive across scenes.
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // If another instance exists, destroy it.
        }
    }

    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    IEnumerator LoadSceneRoutine(string sceneName)
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(transitionTime);
        }
        SceneManager.LoadScene(sceneName);
    }
}