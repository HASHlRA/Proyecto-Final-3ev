using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Determina la velocidad del player
    public float speed = 10f;

    // Guarda el proyectil
    public GameObject projectilePrefab;

    // Guarda los ejes verticales y horizontales
    private float verticalInput;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento del player
        verticalInput = Input.GetAxis("VerticalMovement");
        transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);

        horizontalInput = Input.GetAxis("HorizontalMovement");
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);

        // Disparo
        if (Input.GetKeyDown(KeyCode.VerticalShoot))
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }

    }
}
