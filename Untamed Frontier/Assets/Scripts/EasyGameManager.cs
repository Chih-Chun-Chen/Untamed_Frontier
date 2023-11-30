using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EasyGameManager : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public bool isEasyEliminated = false;

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

        if (isEasyEliminated)
        {
            SceneManager.LoadScene("Medium Level Scene");
        }

        if (MainManager.Instance.isGameOver)
        {
            SceneManager.LoadScene("Game Over Scene");
        }
    }
}
