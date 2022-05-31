using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datapersistence : MonoBehaviour
{
    
    // Instancia compartida
    public static Datapersistence sharedInstance;

    // Variables cuyo valor queremos conservar entre escenas:
    public int doorindex;




   
    // Nos aseguramos de que la instancia sea única
    private void Awake()
    {
        // Si la instancia no existe
        if (sharedInstance == null)
        {
            // Configuramos la instancia
            sharedInstance = this;
            // Nos aseguramos de que no sea destruida con el cambio de escena
            DontDestroyOnLoad(sharedInstance);
        }
        else
        {
            // Como ya existe una instancia, destruimos la copia
            Destroy(this);
        }
    }

  
    
}
