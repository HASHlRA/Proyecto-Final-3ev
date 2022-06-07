using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    // Cambio de scena
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Cuando Game Over y le demos a restart, q los valores vuelvan a los iniciales
    public void restart ()
    {
        DataPersistence.sharedInstance.lives = 3;
        DataPersistence.sharedInstance.money = 0;
        SceneManager.LoadScene("Escena 1");
    }
}
