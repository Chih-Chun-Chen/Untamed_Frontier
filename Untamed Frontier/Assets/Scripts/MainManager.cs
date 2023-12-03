using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Slider and Dropdown

public class MainManager : MonoBehaviour
{
    public static MainManager Instance; // Singleton instance

    public float playerHealth;
    public bool isGameOver = false;

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
        // Optionally, save settings when they are changed by hooking into their respective event listeners
        // This can be done in the UI setup rather than in Update for better performance
    }
}
