using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BulletController : MonoBehaviour
{
    private Scene currentScene;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(sceneName);
            if (sceneName == "Easy Level Scene")
            {
                MainManager.Instance.playerHealth -= 10;
            }
            else if (sceneName == "Medium Level Scene")
            {
                MainManager.Instance.playerHealth -= 20;
                Debug.Log("20");
            }
            else if (sceneName == "Hard Level Scene")
            {
                MainManager.Instance.playerHealth -= 30;
                Debug.Log("30");
            }
        }
        Destroy(gameObject); // Destroy the bullet itself
    }
}
