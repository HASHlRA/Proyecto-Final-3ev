using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextscene : MonoBehaviour
{
    public int _doorindex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int randomindex = Random.Range(1, 6);
            string scenename = $"Escena {randomindex}";
            Datapersistence.sharedInstance.doorindex=_doorindex;
            SceneManager.LoadScene(scenename);
        }
    }
}
