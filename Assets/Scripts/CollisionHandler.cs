using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;
    AudioSource audioSource;
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("You have finished the level");
                StartFinishSequence();
                break;
            case "Fuel":
                Debug.Log("You have picked up fuel");
                break;
            default:
                Debug.Log("You have crashed");
                StartCrashSequence();
                break;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", levelLoadDelay);

    }
    private void StartFinishSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(finishSound);
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
