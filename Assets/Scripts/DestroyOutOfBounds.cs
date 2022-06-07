using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Limite superior
    private float upperLim = 15;
    //Lomite inferior
    private float lowerLim = -15f;

    // Update is called once per frame
    void Update()
    {
        // Bala fallida

        if (transform.position.x > upperLim)
        {
            Destroy(gameObject);
        }

        if (transform.position.z > upperLim)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < lowerLim)
        {
            Destroy(gameObject);
        }

        if (transform.position.z < lowerLim)
        {
            Destroy(gameObject);
        }
    }
}
