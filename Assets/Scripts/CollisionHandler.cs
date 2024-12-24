using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
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
                    LoadNextLevel();
                    break;
                }
            default:
                {
                    Debug.Log("Collided with something");
                    ReloadLevel();
                    break;
                }
        }
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
