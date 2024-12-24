using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                {
                    Debug.Log("This is fine");
                    break;
                }
            case "Finish":
                {
                    StartSuccessSequence();
                    break;
                }
            default:
                {
                    StartCrashSequence();
                    break;
                }
        }
    }

    private void StartCrashSequence()
    {
        // TODO: add sfx and particles
        // Disable player movement
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        // TODO: add sfx and particles
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
