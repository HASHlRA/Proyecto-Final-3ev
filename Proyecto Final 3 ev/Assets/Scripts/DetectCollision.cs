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

    private void OnTriggerEnter(Collider otherCollider)
    {
        // Destruyo el tanque contra el que colisiona
        Destroy(otherCollider.gameObject);

        // Destruyo el proyectil
        Destroy(gameObject);

    }
}
