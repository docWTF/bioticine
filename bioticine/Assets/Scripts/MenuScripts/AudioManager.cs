using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public static AudioManager Instance { get; private set; }
    public AudioSource soundEffectsSource;
    public AudioSource musicSource;
    public AudioClip[] soundEffects;
    public AudioClip[] musicClips;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Music fade out
    public void FadeOutMusic(float fadeOutTime)
    {
        StartCoroutine(FadeMusicVolume(fadeOutTime, 0));
    }

    // Music fade in
    public void FadeInMusic(float fadeInTime, AudioClip newClip)
    {
        if (musicSource.isPlaying)
        {
            FadeOutMusic(1); // Optionally fade out current music quickly before fading in new music
        }
        musicSource.clip = newClip;
        musicSource.Play();
        StartCoroutine(FadeMusicVolume(fadeInTime, 1));
    }

    private IEnumerator FadeMusicVolume(float time, float targetVolume)
    {
        float startVolume = musicSource.volume;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / time);
            yield return null;
        }

        musicSource.volume = targetVolume;
        if (targetVolume == 0)
        {
            musicSource.Stop();
        }
    }

    void Start()
    {
        var dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<string>("PlaySound", PlaySoundEffect);
        dialogueRunner.AddCommandHandler<string>("PlayMusic", PlayMusicWithFade);
        dialogueRunner.AddCommandHandler("StopMusic", () => FadeOutMusic(1)); // Default fade out over 1 second
        dialogueRunner.AddCommandHandler("StopSound", StopSoundEffects);
    }

    private void StopSoundEffects()
    {
        soundEffectsSource.Stop();
    }

    private void PlaySoundEffect(string soundName)
    {
        AudioClip clip = soundEffects.FirstOrDefault(s => s.name == soundName);
        if (clip != null)
        {
            soundEffectsSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound effect not found: " + soundName);
        }
    }

    private void PlayMusicWithFade(string musicName)
    {
        AudioClip clip = musicClips.FirstOrDefault(m => m.name == musicName);
        if (clip != null)
        {
            FadeInMusic(1, clip); // Default fade in over 1 second
        }
        else
        {
            Debug.LogWarning("Music clip not found: " + musicName);
        }
    }
}