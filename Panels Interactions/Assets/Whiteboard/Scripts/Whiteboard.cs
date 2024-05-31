using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.InputSystem;

public class Whiteboard : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D texture;
    public static Texture2D whiteTexture;

    public WhiteBoardDrafter Drafter;
    public Vector2 textureSize = new Vector2(2048,6144); 
    
    //private Color[] start_color = new Color[1];
    //start_color[0] = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int) textureSize.x, (int) textureSize.y);
        //start_color = Enumerable.Repeat(texture.material.color,(int) textureSize.x * (int) textureSize.y).ToArray();
        //texture.SetPixels(0,0,(int)textureSize.x,(int) textureSize.y, start_color[0]);
        texture.SetPixels(0,0,2048,6144, Enumerable.Repeat(Drafter._renderer.material.color, 2048 * 6144).ToArray());
        texture.Apply();
        r.material.mainTexture = texture;
    }

    public void CleanScene()
    {   
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int) textureSize.x, (int) textureSize.y);
        texture.SetPixels(0,0,2048,6144, Enumerable.Repeat(Drafter._renderer.material.color, 2048 * 6144).ToArray());
        texture.Apply();
        r.material.mainTexture = texture;
    }
    public void SaveWhiteBoard()
    {
        Texture2D whiteBoard;
        whiteBoard = new Texture2D((int)textureSize.x, (int)textureSize.y);
        whiteBoard.SetPixels(0, 0, 2048, 6144, texture.GetPixels());
        whiteBoard.Apply();

        byte[] bytes = whiteBoard.EncodeToPNG();
        var dirPath = Application.dataPath + "/../SaveImages/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        Debug.Log("Attempting to save in: "+dirPath);
        File.WriteAllBytes(dirPath + "WhiteBoard" + ".png", bytes);

    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.sKey.wasPressedThisFrame)
        {
            SaveWhiteBoard();
        }
    }

}
