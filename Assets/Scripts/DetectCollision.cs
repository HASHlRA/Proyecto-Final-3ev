using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private PlayerController PlayerControllerScript;


    private AudioSource EnemyAudioSource;
    public AudioClip damageAudio;

    //Particulas
    public ParticleSystem daño;

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

            Debug.Log(damageAudio);

            EnemyAudioSource.PlayOneShot(damageAudio);

            ParticleSystem explosionEscena = Instantiate(daño, transform.position,
        daño.transform.rotation);
            explosionEscena.Play();



            // Destruyo el proyectil
            Destroy(gameObject);
        }

    }
}
