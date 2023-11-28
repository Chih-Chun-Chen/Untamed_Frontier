using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioClip bombEffect;
    public GameObject bombParticle;

    private Player player;

    private bool isEliminated = false;

    private int bombTimer;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        bombTimer = 5;
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
        }
       
        // Delay the destruction of the gameObject until after the sound has finished playing.
        Destroy(gameObject, bombEffect.length);
    }
}
