using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Slider and Dropdown

public class MainManager : MonoBehaviour
{
    public static MainManager Instance; // Singleton instance

    public float playerHealth;
    public bool isGameOver = false;

    public float fpsSliderValue;
    public int musicDropdownValue;
    public int backgroundMusicToggleValue;
    public float volumeSliderValue;
    public int crosshairColorDropdownValue;

    void Awake()
    {
        // Set the instance to this and make it persistent
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerHealth = 100f; // Default health
    }

    void Update()
    {
        Debug.Log("FPS" + fpsSliderValue);
        Debug.Log("Music" + musicDropdownValue);
        Debug.Log("Background Music" + backgroundMusicToggleValue);
        Debug.Log("Volume" + volumeSliderValue);
        Debug.Log("Crosshair Color" + crosshairColorDropdownValue);
    }
}
