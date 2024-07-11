using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections;

public class WhiteBoardDrafter : MonoBehaviour
{   
    public Transform point; 
    private int PenSize = 130;
    
    [HideInInspector]
    public Renderer render;
    //private TouchColor other;
    private Color[] colors;
    private float pointHeight;

    private int tip_bulk;

    private RaycastHit touch;
    private Whiteboard whiteboard;
    private Vector2 Postouch,LastPosTouched;
    private bool LastFrameTouched;
    private Quaternion LastRotTouched;

    // Start is called before the first frame update
    void Awake()
    {
        render = point.GetComponent<Renderer>();
        colors = Enumerable.Repeat(render.material.color, PenSize * PenSize).ToArray();
        pointHeight = point.localScale.y;


        
    }

    // Update is called once per frame
    void Update()
    {
        Draft();
    }
    private void Draft()
    {
        if(Physics.Raycast(point.position, transform.up, out touch, pointHeight))
        {
            if(touch.transform.CompareTag("Whiteboard"))
            {
                if(whiteboard ==null )
                {
                    whiteboard = touch.transform.GetComponent<Whiteboard>();
                }
                Postouch = new Vector2(touch.textureCoord.x, touch.textureCoord.y);

                var x = (int)(Postouch.x * whiteboard.textureSize.x - (PenSize/2));
                var y = (int)(Postouch.y * whiteboard.textureSize.y - (PenSize/2));

                if(y < 0 || y > whiteboard.textureSize.y || x < 0 || x > whiteboard.textureSize.x) return;

                if(LastFrameTouched){

                    whiteboard.texture.SetPixels(x,y,PenSize,PenSize, colors);

                    for (float f = 0.01f; f < 1.00f; f+= 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(LastPosTouched.x,x,f);
                        var lerpY = (int)Mathf.Lerp(LastPosTouched.y,y,f);
                        whiteboard.texture.SetPixels(lerpX,lerpY,PenSize,PenSize,colors);
                    }

                    transform.rotation = LastRotTouched;
                    
                    whiteboard.texture.Apply();
                }
                LastPosTouched = new Vector2(x,y);
                LastRotTouched = transform.rotation;
                LastFrameTouched = true;
                return;
            }
            

            

        }

        whiteboard = null;
        LastFrameTouched = false;

    }
}
