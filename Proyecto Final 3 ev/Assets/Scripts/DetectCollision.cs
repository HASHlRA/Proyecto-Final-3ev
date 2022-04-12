using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private PlayerController PlayerControllerScript;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Player"))
        {
            PlayerControllerScript.UpdateLife(-1);
            
            // Destruyo el proyectil
            Destroy(gameObject);
        }

    }
}
