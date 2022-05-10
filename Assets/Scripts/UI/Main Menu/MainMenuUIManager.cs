using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        // Cargamos la escena que tenga por nombre sceneName
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        // Si estamos haciendo pruebas en el editor
        #if UNITY_EDITOR
        // Salimos del editor
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
        // Salimos de la aplicación (solo funcionará en la Build)
        Application.Quit();
    }
}
