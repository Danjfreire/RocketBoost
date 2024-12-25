using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip successAudioClip;

    private AudioSource audioSource;
    private bool canColllide = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!canColllide) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                {
                    Debug.Log("This is fine");
                    break;
                }
            case "Finish":
                {
                    canColllide = false;
                    StartSuccessSequence();
                    break;
                }
            default:
                {
                    canColllide = false;
                    StartCrashSequence();
                    break;
                }
        }
    }

    private void StartCrashSequence()
    {
        // TODO: add particles
        audioSource.Stop();
        audioSource.PlayOneShot(explosionAudioClip, 0.5f);
        // Disable player movement
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        // TODO: add particles
        audioSource.Stop();
        audioSource.PlayOneShot(successAudioClip, 0.5f);
        // Disable player movement
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int nextLevelIndex = (SceneManager.GetActiveScene().buildIndex + 1) % sceneCount;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
