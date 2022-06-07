using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] colors;
    public TMP_InputField username;


    private int colorSelected;

    private void Update()
    {
        ColorSelection();
    }

    public void SaveUserOptions()
    {
        // Persistencia de datos entre escenas
        DataPersistence.sharedInstance.colorSelected = colorSelected;
        DataPersistence.sharedInstance.color = colors[colorSelected].GetComponent<Image>().color;
        
        
        DataPersistence.sharedInstance.username = username.text;
        
        // Persistencia de datos entre partidas
        DataPersistence.sharedInstance.SaveForFutureGames();
    }

    public void LoadUserOptions()
    {
        // Tal y como lo hemos configurado, si tiene esta clave, entonces tiene todas
        if (PlayerPrefs.HasKey("COLOR_SELECTED"))
        {
            colorSelected = PlayerPrefs.GetInt("COLOR_SELECTED");
            

            username.text = PlayerPrefs.GetString("USERNAME");
        }
    }


    #region Color Settings

    private void ColorSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            colorSelected++;
        }else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            colorSelected--;
        }

        
        colorSelected = (colorSelected % 4 + 4) % 4;
        ChangeColorSelection();
    }

    private void ChangeColorSelection()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i].transform.GetChild(0).gameObject.SetActive(i == colorSelected);
        }
    }

    // Para poder sesactivar la musica con el toogle
    public void tooglemusic(bool ison)
    {
        AudioManager.sharedInstance.enablemusic(ison);
    }

    // Para poder controlar la musica con el slider
    public void slidervolume(float v)
    {
        AudioManager.sharedInstance.setvolume (v);
    }
    #endregion
}
