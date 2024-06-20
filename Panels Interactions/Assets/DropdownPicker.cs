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
        TMP_Dropdown dropdown = transform.GetComponent<TMP_Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("14");
        items.Add("16");
        items.Add("18");

        
        // foreach (var item in items)
        // {
        //     dropdown.options.Add(new Dropdown.OptionData(){text = item});
        // } 
        dropdown.AddOptions(items);
        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate {DropdownItemSelected(dropdown); });
    }
    
    void DropdownItemSelected(TMP_Dropdown dropdown){
        int index = dropdown.value;
        int Font = int.Parse(dropdown.options[index].text);

        textBox.fontSize = Font;

    }

}
