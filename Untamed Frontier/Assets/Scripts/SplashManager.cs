using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public Button startBtn;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void StartGame()
    {
        SceneManager.LoadScene("Easy Level Scene");
    }
}
