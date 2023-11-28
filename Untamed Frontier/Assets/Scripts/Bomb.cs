using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioClip bombEffect;
    private int bombTimer;

    void Start()
    {
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

        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
        newAudioSource.PlayOneShot(bombEffect);
        Destroy(newAudioSource, bombEffect.length);

        // Delay the destruction of the gameObject until after the sound has finished playing.
        Destroy(gameObject, bombEffect.length);
    }
}
