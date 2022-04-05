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

    // Guarda los ejes de verticales y horizontales del disparo
    private float verticalInputS;
    private float horizontalInputS;

    // Guarda el cooldown del disparo
    private float cooldown = 50f;
    private float lastshoot;

    // Guarda el GameOver
    public bool gameOver;



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
        verticalInputS = Input.GetAxis("VerticalShoot");
        horizontalInputS = Input.GetAxis("HorizontalShoot");
        if (verticalInputS != 0)
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Sign(verticalInputS) > 0 ? 0 : 180, 0);
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            shooting();
        }

        else if (horizontalInputS != 0)
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Sign(horizontalInputS) * 90, 0);
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            shooting();
        }

        void shooting()
        {
            if (Time.time - lastshoot < cooldown)
            {
                return;
            }

            lastshoot = Time.time;

        }


    }

    private void OnTriggerEnter(Collider otherCollision)
    {
        if (otherCollision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(otherCollision.gameObject);
            GameOver();
        }
    }

    private void GameOver()
    {
        {
            // Indica que el juego ha finalizado
            gameOver = true;

            // Muestra en consola tu resultado
            Debug.Log($"GAME OVER.");
        }
    }
}
