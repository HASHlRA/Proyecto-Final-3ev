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
    private float cooldown = 1f;
    private float lastshoot;

    // Guarda el GameOver
    public bool gameOver;

    //Guarda total de vidas
    private int lives = 3;

    // Límite de escenario
    private float xRange = 8.75f;
    private float zRange = 8.75f;

    // Contador de monedas
    private int totalmoneda = 0;


    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento del player
        verticalInput = Input.GetAxis("VerticalMovement");
        transform.Translate( Vector3.forward * speed * Time.deltaTime * verticalInput, Space.World);

        horizontalInput = Input.GetAxis("HorizontalMovement");
        transform.Translate( Vector3.right * speed * Time.deltaTime * horizontalInput, Space.World);

        // Disparo
        verticalInputS = Input.GetAxis("VerticalShoot");
        horizontalInputS = Input.GetAxis("HorizontalShoot");
        if (verticalInputS != 0 && shooting())
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Sign(verticalInputS) > 0 ? 0 : 180, 0);
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            
        }

        else if (horizontalInputS != 0 && shooting())
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Sign(horizontalInputS) * 90, 0);
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            
        }

        

        // Limite de pantalla
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        // Limite de pantalla izquierdo
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > zRange)
        {
            transform.position = new Vector3(zRange, transform.position.y, transform.position.z);
        }

        // Limite de pantalla izquierdo
        if (transform.position.x < -zRange)
        {
            transform.position = new Vector3(-zRange, transform.position.y, transform.position.z);
        }



    }
    bool shooting()
    {
        if (Time.time - lastshoot < cooldown)
        {
            return false;
        }

        lastshoot = Time.time;
        return true;
    }

    private void OnTriggerEnter(Collider otherCollision)
    {
        

        if (otherCollision.gameObject.CompareTag("Moneda"))
        {
            Destroy(otherCollision.gameObject);
            totalmoneda++;
            Debug.Log(totalmoneda);
        }

        if (otherCollision.gameObject.CompareTag("Heal"))
        {
            UpdateLife(1);
            Destroy(otherCollision.gameObject);
            Debug.Log("a");
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

    public void UpdateLife(int value)
    {
        lives += value;
        Debug.Log(lives);
        if (lives <= 0)
        {
            GameOver();
            Debug.Log("Game Over");
        }
    }


}
