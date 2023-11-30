using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class HardGameManager : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public TextMeshProUGUI health;
    public bool isEliminated = false;

    private Bomb bomb;

    // Start is called before the first frame update
    void Start()
    {
        bomb = GameObject.FindObjectOfType<Bomb>();
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = "Timer: " + bomb.bombTimer.ToString();
        health.text = "Health: " + MainManager.Instance.playerHealth.ToString();

        if (bomb.isEliminated)
        {
            bomb.isEliminated = false;
            SceneManager.LoadScene("End Game");
        }

        if (MainManager.Instance.isGameOver)
        {
            SceneManager.LoadScene("Game Over Scene");
        }

        if (MainManager.Instance.playerHealth <= 0)
        {
            SceneManager.LoadScene("Game Over Scene");
        }
    }
}
