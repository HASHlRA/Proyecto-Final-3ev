using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextscene : MonoBehaviour
{
    // Para el cambio de scena a traves de las puertas 
    public int _doorindex;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int randomindex = Random.Range(1, 6);
            string scenename = $"Escena {randomindex}";
            DataPersistence.sharedInstance.doorindex=_doorindex;
            SceneManager.LoadScene(scenename);
        }
    }
}
