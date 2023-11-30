using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioClip bombEffect;
    public GameObject bombParticle;

    public int bombTimer;

    private Player player;

    private bool isEliminated = false;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        bombTimer = 90;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (bombTimer > 0)
        {
            yield return new WaitForSeconds(1);
            bombTimer--;
            Debug.Log(bombTimer);
        }

        if (!isEliminated)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.PlayOneShot(bombEffect);
            Destroy(newAudioSource, bombEffect.length);
            if (bombParticle != null)
            {
                Instantiate(bombParticle, transform.position, Quaternion.identity);
            }
            MainManager.Instance.playerHealth -= 200f;

            yield return new WaitForSeconds(bombEffect.length);

            if (MainManager.Instance.playerHealth <= 0f)
            {
                MainManager.Instance.isGameOver = true;
            }
        }
       
        // Delay the destruction of the gameObject until after the sound has finished playing.
        Destroy(gameObject, bombEffect.length);
    }
}
