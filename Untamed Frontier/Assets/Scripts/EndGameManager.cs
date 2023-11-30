using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public Button restartBtn;
    public Button quitBtn;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        // Ensure the buttons are not null to avoid NullReferenceException
        if (restartBtn != null)
        {
            restartBtn.onClick.AddListener(RestartGame);
        }
        else
        {
            Debug.LogError("Restart button not assigned in the inspector.");
        }

        if (quitBtn != null)
        {
            quitBtn.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("Quit button not assigned in the inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RestartGame()
    {
        // Assuming "Easy Level Scene" is the correct scene name and it's added to the build settings
        SceneManager.LoadScene("Easy Level Scene");
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        // Quit the game in the editor
        Debug.Log("Quit game in Editor");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the game in a built application
        Debug.Log("Quit game in Build");
        Application.Quit();
#endif
    }
}
