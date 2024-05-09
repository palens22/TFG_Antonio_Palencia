using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D texture;
    public static Texture2D whiteTexture;
    public Vector2 textureSize = new Vector2(2048,6144); 
    public WhiteBoardDrafter Drafter;
    
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
