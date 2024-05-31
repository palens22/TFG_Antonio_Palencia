using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Keyboard : MonoBehaviour
{

    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject normalButtons;
    [SerializeField] GameObject capsButtons;

    [SerializeField] TextMeshProUGUI textPostit;
    [SerializeField] TextMeshProUGUI datePostit;
    [SerializeField] TextMeshProUGUI userPostit;



    private bool caps;

    // Start is called before the first frame update
    void Start()
    {
        caps = false;
    }

    public void InsertChar(string c)
    {
        inputField.text += c;
    }

    public void DeleteChar()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void InsertSpace()
    {
        inputField.text += " ";
    }

    public void Submit()
    {
        textPostit.text = inputField.text;

        DateTime dt = DateTime.Now;

        datePostit.text = dt.ToString("dd-MM-yyyy");
        userPostit.text = "User: Prueba" /*+ PruebaJSON.recoverUserName("0")*/;
    }

    public void CapsPressed()
    {
        if (!caps)
        {
            normalButtons.SetActive(false);
            capsButtons.SetActive(true);
            caps = true;
        }
        else
        {
            normalButtons.SetActive(true);
            capsButtons.SetActive(false);
            caps = false;
        }
    }

}
