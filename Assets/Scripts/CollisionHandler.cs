using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip finishSFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;
    AudioSource audioSource;

    bool isControllable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable) return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartLandSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartLandSequence()
    {
        isControllable = false;
        finishParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(finishSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", 1f);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 2f);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(currentScene + 1);
    }
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
