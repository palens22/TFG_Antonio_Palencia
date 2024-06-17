using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DropdownPicker : MonoBehaviour
{   
    public TextMeshProUGUI textBox;


    // Start is called before the first frame update
    void Start()
    {
        Dropdown dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("14");
        items.Add("16");
        items.Add("18");

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData(){text = item});
        } 

        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate {DropdownItemSelected(dropdown); });
    }
    
    void DropdownItemSelected(Dropdown dropdown){
        int index = dropdown.value;
        int Font = int.Parse(dropdown.options[index].text);

        textBox.fontSize = Font;

    }

}
