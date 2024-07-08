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
    [SerializeField] GameObject styleButtons;
    [SerializeField] GameObject emoteButtons;

    [SerializeField] GameObject Bold_On;
    [SerializeField] GameObject Italic_On;
    [SerializeField] GameObject Under_On;

    
    [SerializeField] TextMeshProUGUI textPostit;
    [SerializeField] TextMeshProUGUI datePostit;
    [SerializeField] TextMeshProUGUI userPostit;


    private string textSaved, aux_text, styles;
    private bool caps, bold, italic, underline, bangers, roboto, anton, liberation, emote;

    // Start is called before the first frame update
    void Start()
    {
        emote = false;
        caps = false;
        bold = false;
        italic = false;
        underline = false;
        liberation = true;
        bangers = false;
        roboto = false;
        anton = false;
    }

    public void InsertChar(string c)
    {
        textSaved += c;
        inputField.text += c;
    }

    public void DeleteChar()
    {
        if (textSaved.Length > 0)
        {
            textSaved = textSaved.Substring(0, textSaved.Length - 1);
        }
    }

    public void InsertSpace()
    {
        textSaved += " ";
    }
    public void InsertSmile()
    {
        textSaved += "\U0001F600";
    }
    public void InsertLaugh()
    {
        textSaved += "\U0001f602";
    }
    public void InsertUpset()
    {
        textSaved += "\U0001f606";
    }
    public void InsertRandom()
    {
        textSaved += "\U0001F605";
    }

    public void Submit()
    {
        textPostit.text = inputField.text;

        DateTime dt = DateTime.Now;

        datePostit.text = dt.ToString("dd-MM-yyyy");
        userPostit.text = "User: Tester";
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
        public void EmotePressed()
    {
        if (!emote)
        {
            styleButtons.SetActive(false);
            emoteButtons.SetActive(true);
            emote = true;
        }
        else
        {
            styleButtons.SetActive(true);
            emoteButtons.SetActive(false);
            emote = false;
        }
    }
    public void ChangeBold()
    {
      if(!bold){
        Bold_On.SetActive(true);
        bold = true;
      }
      else{
        Bold_On.SetActive(false);
        bold = false;
      }

    }
    public void ChangeItalic()
    {
       if(!italic){
        Italic_On.SetActive(true);
        italic = true;
      }
      else{
        Italic_On.SetActive(false);
        italic = false;
      }
    }
    public void ChangeUnderline()
    {   
      if(!underline){
        Under_On.SetActive(true);
        underline = true;
      }
      else{
        Under_On.SetActive(false);
        underline = false;
      }
       }
    public void ChangeBangers()
    {   
      if(!bangers){
        bangers = true;
        anton = false;
        liberation = false;   
        roboto = false;
      }
      
      }
      public void ChangeAnton()
     {   
      if(!anton){
        bangers = false;
        anton = true;
        liberation = false;   
        roboto = false;
      }
      
      }
      public void ChangeRoboto()
    {   
      if(!roboto){
        bangers = false;
        anton = false;
        liberation = false;   
        roboto = true;
      }
      
      }
      public void Changeliberation()
    {   
      if(!liberation){
        bangers = false;
        anton = false;
        liberation = true;   
        roboto = false;
      }
      
      }
    
    
    void Update()
    {
        if(bold==true && italic==false && underline==false)
        {   
            aux_text = "<b>" + textSaved + "</b>";
            inputField.text =  aux_text;
            
        }
        else if(bold==false && italic==true && underline==false){
            aux_text = "<i>" + textSaved + "</i>";
            inputField.text =  aux_text;
            
        }
        else if(bold==false && italic==false && underline==true){
            aux_text = "<u>" + textSaved + "</u>";
            inputField.text =  aux_text;
            
        }
        else if(bold==true && italic == true && underline == false)
        {   
            aux_text = "<b><i>" + textSaved + "</i></b>";
            inputField.text =  aux_text;
            
        }
        else if(bold==false && italic==true && underline==true){
            aux_text = "<i><u>" + textSaved + "</u></i>";
            inputField.text =  aux_text;
           
        }
        else if(underline==true && bold==true && italic==false){
            aux_text = "<u><b>" + textSaved + "</b></u>";
            inputField.text =  aux_text;
        }
        else if(underline==true && bold==true && italic==true)
        {
            aux_text = "<u><b><i>" + textSaved + "</i></b></u>";
            inputField.text =  aux_text;
            
        }
        else if(underline==false && bold==false && italic==false)
        { 
            aux_text = textSaved;
            inputField.text =  aux_text;
        }
        

        if(anton==true)
        {
            inputField.text ="<font=Anton SDF>" + aux_text + "</font>";
            
        }
        else if(roboto==true)
        {
            inputField.text ="<font=Roboto-Bold SDF>" + aux_text + "</font>";
            
        }
        else if(liberation==true)
        {
            inputField.text ="<font=LiberationSans SDF>" + aux_text + "</font>";
           
        }
        else if(bangers==true)
        {
            inputField.text ="<font=Bangers SDF>" + aux_text + "</font>";
            
        }
    
   }
}


 

