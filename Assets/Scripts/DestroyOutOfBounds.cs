using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float upperLim = 8.75f;
    private float lowerLim = -8.75f;

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
