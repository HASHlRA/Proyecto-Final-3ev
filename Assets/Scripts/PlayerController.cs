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
    private float xRange = 8.75f;
    private float zRange = 8.75f;

    // Contador de monedas
   // private int moneda = 0;
   // public TextMeshProUGUI monedaText;
  //  public int counter = 10;

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

    private Vector3[] startposition = new Vector3[]
    {
        new Vector3(0,0.5f,4),
        new Vector3 (4f,0.5f,0),
        new Vector3(0,0.5f,-4f),
        new Vector3(-4f,0.5f,0)
    };




    // Start is called before the first frame update
    void Start()
    {
        life = hearts.Length;
        pointText.text = $" {score}";
        playerAudioSource = GetComponent<AudioSource>();
        Debug.Log(Datapersistence.sharedInstance.doorindex);
        if (Datapersistence.sharedInstance.doorindex == -1)
        {
            transform.position = new Vector3(0, 0.5f, 7);
        }
        else
        {
            transform.position = startposition[(Datapersistence.sharedInstance.doorindex + 2) % 4];
        }
        /*
        else
        {
            Datapersistence.sharedInstance.pointText = pointText.text;
        }
        */
      


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
        //playerAudioSource.PlayOneShot(shot);
    }

    private void OnTriggerEnter(Collider otherCollision)
    {
        

        if (otherCollision.gameObject.CompareTag("Moneda"))
        {
            
            score++;
            pointText.text = $" {score}";

            playerAudioSource.PlayOneShot(moneyAudio);

            Destroy(otherCollision.gameObject);

          
            /*
            score++;
            Debug.Log(score);
            */
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

            playerAudioSource.PlayOneShot(gameoverAudio);

            //cameraAudioSource.volume = 0.01f;

            // Muestra en consola tu resultado
            Debug.Log($"GAME OVER.");
        }
    }
    
    public void UpdateLife(int value)
    {
        life += value;
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
