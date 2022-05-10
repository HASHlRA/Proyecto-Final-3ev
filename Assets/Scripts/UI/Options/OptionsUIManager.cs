using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsUIManager : MonoBehaviour
{
    public GameObject[] colorSelection;
    public TMP_InputField[] RGB;
    public TMP_InputField Hex;

    public Image preview;

    public void GoToMainScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RGBOrHex(int selection)
    {
        for (int i = 0; i < colorSelection.Length; i++)
        {
            if (i == selection)
            {
                colorSelection[i].SetActive(true);
            }
            else
            {
                colorSelection[i].SetActive(false);
            }
        }
    }

    public bool IsValidRGB()
    {
        foreach (var t in RGB)
        {
            Debug.Log(t.text.GetType());
            int.TryParse(t.text, out int val);
            if (!ValueBetween(val, 0, 255))
            {
                return false;
            }
        }
        return true;
    }
    
    public bool IsValidHex()
    {
        Regex hexColorRegex = new Regex("^[a-fA-F0-9]+$");

        if (hexColorRegex.IsMatch(Hex.text))
        {
            return true;
        }
        return false;
    }
    
    public void ApplyRGBColor()
    {
        if (IsValidRGB())
        {
            preview.color = new Color(NormalizeValue(RGB[0].text, 255f),
                NormalizeValue(RGB[1].text, 255f),
                NormalizeValue(RGB[2].text, 255f));
        }
    }

    public void ApplyHexColor()
    {
        if (IsValidHex())
        {
            ColorUtility.TryParseHtmlString(string.Concat("#", Hex.text), out Color hexColor);
            preview.color = hexColor;
        }
    }
    
    private float NormalizeValue(string s, float max)
    {
        int val = int.Parse(s);
        return val / max;
    }
    
    private bool ValueBetween(int val, int min, int max)
    {
        if (val >= min && val <= max)
        {
            return true;
        }

        return false;

    }
}
