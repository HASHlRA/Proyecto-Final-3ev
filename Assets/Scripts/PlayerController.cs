using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    private int life;
    public GameObject[] hearts;

    // Límite de escenario
    private float xRange = 15f;
    private float zRange = 15f;

    // Para el score
    public TextMeshProUGUI pointText;
    private int score = 0;

    //Para cambio de escena 
    public GameObject[] nextScene;

    //Para la musica
    private AudioSource playerAudioSource;
    private AudioSource cameraAudioSource;
    public AudioClip moneyAudio;
    public AudioClip healAudio;
    public AudioClip shot;
    public AudioClip gameoverAudio;

    //Para la posicion inicial del player
    private Vector3[] startposition = new Vector3[]
    {
        new Vector3(0,0.5f,4),
        new Vector3 (4f,0.5f,0),
        new Vector3(0,0.5f,-4f),
        new Vector3(-4f,0.5f,0)
    };

    //Para el Game Over
    public GameObject gameOverPanel;
    public GameObject restartPanel;
    public GameObject exitPanel;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        // Persistencia de datos
            // La vida
        life = DataPersistence.sharedInstance.lives;
        UpdateLife (0);
            //El score
        score = DataPersistence.sharedInstance.money;
        pointText.text = $" {score}";
            // para la musica
        playerAudioSource = GetComponent<AudioSource>();
            // Para la puerta en la q sale y entra
        Debug.Log(DataPersistence.sharedInstance.doorindex);
        if (DataPersistence.sharedInstance.doorindex == -1)
        {
            transform.position = new Vector3(0, 0.5f, 7);
        }
        else
        {
            transform.position = startposition[(DataPersistence.sharedInstance.doorindex + 2) % 4];
        }

        //Para el panel de Game Over
        gameOverPanel.SetActive(false);
        restartPanel.SetActive(false);
        exitPanel.SetActive(false);
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
            
            score++;
            DataPersistence.sharedInstance.money = score;
            pointText.text = $" {score}";

            playerAudioSource.PlayOneShot(moneyAudio);

            Destroy(otherCollision.gameObject);            
        }

        if (otherCollision.gameObject.CompareTag("Heal"))
        {
            UpdateLife(1);
            playerAudioSource.PlayOneShot(healAudio);
            Destroy(otherCollision.gameObject);
            Debug.Log("a");
        }
    }

    private void GameOver()
    {
        {
            // Indica que el juego ha finalizado
            gameOver = true;
            gameOverPanel.SetActive(true);
            restartPanel.SetActive(true);
            exitPanel.SetActive(true);

            // Sonido de fame over
            playerAudioSource.PlayOneShot(gameoverAudio);
            Time.timeScale = 0f;
        }
    }
    
    // Para el contador de vidas del player
    public void UpdateLife(int value)
    {
        life += value;
        DataPersistence.sharedInstance.lives = life;
        Debug.Log(life);
        if (life <= 0)
        {
            hearts[0].gameObject.SetActive(false);
            hearts[1].gameObject.SetActive(false);
            hearts[2].gameObject.SetActive(false);
            GameOver();
            Debug.Log("Game Over");
        }
        else if (life == 1)
        {
            hearts[0].gameObject.SetActive(true);
            hearts[1].gameObject.SetActive(false);
            hearts[2].gameObject.SetActive(false);
        }
        else if (life == 2)
        {
            hearts[0].gameObject.SetActive(true);
            hearts[1].gameObject.SetActive(true);
            hearts[2].gameObject.SetActive(false);
        }
        else if (life == 3)
        {
            hearts[0].gameObject.SetActive(true);
            hearts[1].gameObject.SetActive(true);
            hearts[2].gameObject.SetActive(true);
        }
    }
}
