using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        respondToDebugKeys();

    }

    private void respondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) { return;}
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

    private void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);

    }
    private void StartFinishSequence()
    {
        isControllable = false;     
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
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
