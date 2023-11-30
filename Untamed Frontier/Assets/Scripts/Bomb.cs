using System.Collections;
using UnityEngine;
using TMPro;

public class Bomb : MonoBehaviour
{
    public AudioClip bombEffect;
    public GameObject bombParticle;
    public TextMeshProUGUI messageText;

    public int bombTimer;
    private float disarmDistance = 0.5f; // Distance within which the player can disarm the bomb

    private Player player;
    private EasyGameManager emanager;
    private bool isPlayerInRange = false;
    private bool isDisarming = false;
    private float disarmTimer = 0f;
    private float totalDisarmTime = 5f;
    private bool isEasyEliminated = false;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        emanager = GameObject.FindObjectOfType<EasyGameManager>();
        bombTimer = 90;
        messageText.text = "";
        StartCoroutine(Countdown());
    }

    void Update()
    {
        CheckPlayerDistance();

        if (isPlayerInRange)
        {
            HandleDisarming();
        }
    }

    void CheckPlayerDistance()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log(distance);
            isPlayerInRange = distance < disarmDistance;

            if (isPlayerInRange)
            {
                messageText.text = "Press F to disarm bomb";
            }
            else
            {
                messageText.text = "";
                isDisarming = false; // Reset disarming if the player moves out of range
            }
        }
    }

    void HandleDisarming()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isDisarming = true;
            disarmTimer = 0f;
        }

        if (Input.GetKey(KeyCode.F) && isDisarming)
        {
            disarmTimer += Time.deltaTime;
            messageText.text = "Disarming bomb: " + (totalDisarmTime - disarmTimer).ToString("F2") + " seconds left";

            if (disarmTimer >= totalDisarmTime)
            {
                DisarmBomb();
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            isDisarming = false;
            if (isPlayerInRange)
            {
                messageText.text = "Press F to disarm bomb";
            }
        }
    }

    IEnumerator Countdown()
    {
        while (bombTimer > 0 && !emanager.isEasyEliminated)
        {
            yield return new WaitForSeconds(1);
            bombTimer--;
            Debug.Log(bombTimer);
        }

        if (!emanager.isEasyEliminated)
        {
            Explode();
        }

        // Delay the destruction of the gameObject until after the sound has finished playing.
        Destroy(gameObject, bombEffect.length);
    }

    void DisarmBomb()
    {
        messageText.text = "Bomb disarmed!";
        emanager.isEasyEliminated = true;
        isPlayerInRange = false;
        isDisarming = false;
    }

    void Explode()
    {
        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
        newAudioSource.PlayOneShot(bombEffect);
        Destroy(newAudioSource, bombEffect.length);
        if (bombParticle != null)
        {
            Instantiate(bombParticle, transform.position, Quaternion.identity);
        }
        MainManager.Instance.playerHealth -= 200f;

        if (MainManager.Instance.playerHealth <= 0f)
        {
            MainManager.Instance.isGameOver = true;
        }

        // Delay the destruction of the gameObject until after the sound has finished playing.
        Destroy(gameObject, bombEffect.length);
    }
}
