using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        playerHealth = 100f;
    }


    void Update()
    {

    }
}
