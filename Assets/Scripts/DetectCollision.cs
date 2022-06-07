using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private PlayerController PlayerControllerScript;

    //Sonidos
    private AudioSource EnemyAudioSource;
    public AudioClip damageAudio;

    //Particulas
    public ParticleSystem danyo;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = FindObjectOfType<PlayerController>();
        EnemyAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            PlayerControllerScript.UpdateLife(-1);

            Destroy(gameObject);

            Debug.Log(damageAudio);

            EnemyAudioSource.PlayOneShot(damageAudio);

            ParticleSystem explosionEscena = Instantiate(danyo, transform.position,
        danyo.transform.rotation);
            explosionEscena.Play();


        }

    }
}
