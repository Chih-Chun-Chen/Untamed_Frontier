using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public Button startBtn;
    public AudioClip backgroundMusic; // Background music clip
    private AudioSource audioSource;  // Audio source component

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        startBtn.onClick.AddListener(StartGame);

        // Add AudioSource component and play background music
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // Loop the music
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        StopMusic(); // Stop the music before changing the scene
        SceneManager.LoadScene("Easy Level Scene");
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
