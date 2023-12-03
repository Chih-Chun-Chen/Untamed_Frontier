using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public Button startBtn;
    public Button settingBtn;
    public Button quitBtn;
    public AudioClip backgroundMusic; // Background music clip
    private AudioSource audioSource;  // Audio source component

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        startBtn.onClick.AddListener(StartGame);
        settingBtn.onClick.AddListener(GoToSetting);
        quitBtn.onClick.AddListener(QuitGame);

        // Add AudioSource component and play background music
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // Loop the music
        audioSource.volume = MainManager.Instance.volumeSliderValue;
        if (MainManager.Instance.backgroundMusicToggleValue == 1)
        {
            audioSource.Play();
        }
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

    void GoToSetting()
    {
        SceneManager.LoadScene("Settings Scene");
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        // Quit the game in the editor
        Debug.Log("Quit game in Editor");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the game in a built application
        Application.Quit();
#endif
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
