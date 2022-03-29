using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Velocidad del proyectil
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Movimiento hacia delante del proyectil
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
