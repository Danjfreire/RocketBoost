using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip successAudioClip;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem successParticles;

    private AudioSource audioSource;
    private bool canCollide = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!canCollide) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                {
                    Debug.Log("This is fine");
                    break;
                }
            case "Finish":
                {
                    canCollide = false;
                    StartSuccessSequence();
                    break;
                }
            default:
                {
                    canCollide = false;
                    StartCrashSequence();
                    break;
                }
        }
    }

    private void StartCrashSequence()
    {
        explosionParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(explosionAudioClip, 0.5f);
        // Disable player movement
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        successParticles.Play();
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
