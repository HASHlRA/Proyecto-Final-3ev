using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject[] doorstriggers;
    private int enemiesLeft;

    public static GameManager sharedInstance;
    public MeshRenderer player;
    public TextMeshProUGUI username;
    // Start is called before the first frame update
    void Start()
    {
        // Aplicamos los cambios guardados en la escena Menu
        ApplyUserOptions();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesLeft = FindObjectsOfType<Enemy>().Length;
        if (enemiesLeft <= 0)
        {
            GameObject[] doors=GameObject.FindGameObjectsWithTag("Door");
            foreach(GameObject d in doors)
            {
                d.SetActive(false);
            }
            
            foreach (GameObject d in doorstriggers)
            {
                d.SetActive(true);
            }
        }
    }
   

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public void ApplyUserOptions()
    {
        player.material.color = DataPersistence.sharedInstance.color;
        username.text = DataPersistence.sharedInstance.username;
    }
}
