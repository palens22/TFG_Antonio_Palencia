using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections;
using UnityEngine.UI;

public class WhiteboardMarker : MonoBehaviour
{   
    public Transform point; 
    public Slider scaleSlider;
    public FlexibleColorPicker fcp;
    private float transformSlider, pointHeight;
    private Renderer render;
    private Color[] colors;
    private int tip_bulk;
    private RaycastHit touch;
    private Whiteboard whiteboard;
    private Vector3 scale;
    private Vector2 Postouch, LastPosTouched;
    private bool LastFrameTouched;
    private Quaternion LastRotTouched;

    // Start is called before the first frame update
    void Start()
    {
        render = point.GetComponent<Renderer>();
        tip_bulk = (int)(scale.y * 20);
        colors = Enumerable.Repeat(render.material.color, tip_bulk * tip_bulk).ToArray();
        pointHeight = point.localScale.y;


        
    }

    // Update is called once per frame
    void Update()
    {     
        ColorChange();
        SliderChange();
        Draw();
        
    }
    private void Draw()
    {
        if(Physics.Raycast(point.position, transform.TransformDirection(Vector3.up), out touch, pointHeight))
        {
            RaycastPixels();
        }
        else if (Physics.Raycast(point.position, transform.TransformDirection(Vector3.forward), out touch, pointHeight))
        {
            RaycastPixels();

        }
        else if (Physics.Raycast(point.position, transform.TransformDirection(Vector3.right), out touch, pointHeight))
        {   
            RaycastPixels();
        }
        else if (Physics.Raycast(point.position, transform.TransformDirection(Vector3.left), out touch, pointHeight))
        {   
            RaycastPixels();

        }

        else if (Physics.Raycast(point.position, transform.TransformDirection(Vector3.back), out touch, pointHeight))
        {   
            RaycastPixels();
        }
        else 
        {
        whiteboard = null;
        LastFrameTouched = false;
        }
        


    }
    private void OnCollisionEnter(Collision collision){
          if(collision.gameObject.tag == "ColorCube"){
                var new_color = collision.transform.gameObject.GetComponent<Renderer>().material.color;
                render.material.color = new_color;
                colors = Enumerable.Repeat(render.material.color, tip_bulk * tip_bulk).ToArray();
                
            }
    }
    private void SliderChange()
    {
        transformSlider = scaleSlider.value;
        scale = new Vector3(transformSlider, transformSlider, transformSlider);
        this.transform.localScale = scale;
        tip_bulk = (int)(scale.x * 20);
        colors = Enumerable.Repeat(render.material.color, tip_bulk * tip_bulk).ToArray();
        pointHeight = point.localScale.y;

    }
    private void ColorChange(){
        render.material.color = fcp.color;
        colors = Enumerable.Repeat(render.material.color, tip_bulk * tip_bulk).ToArray();
    }
    private void RaycastPixels()
    {

         if(touch.transform.CompareTag("Whiteboard"))
            {
                if(whiteboard == null )
                {
                    whiteboard = touch.transform.GetComponent<Whiteboard>();
                }
                Postouch = new Vector2(touch.textureCoord.x, touch.textureCoord.y);

                var x = (int)(Postouch.x * whiteboard.textureSize.x - (tip_bulk/2));
                var y = (int)(Postouch.y * whiteboard.textureSize.y - (tip_bulk/2));

                if(y < 0 || y > whiteboard.textureSize.y || x < 0 || x > whiteboard.textureSize.x) return;

                if(LastFrameTouched){

                    whiteboard.texture.SetPixels(x,y,tip_bulk,tip_bulk, colors);

                    for (float f = 0.01f; f < 1.00f; f+= 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(LastPosTouched.x,x,f);
                        var lerpY = (int)Mathf.Lerp(LastPosTouched.y,y,f);
                        whiteboard.texture.SetPixels(lerpX,lerpY,tip_bulk,tip_bulk,colors);
                    }

                    transform.rotation = LastRotTouched;
                    
                    whiteboard.texture.Apply();
                }
                LastPosTouched = new Vector2(x,y);
                LastRotTouched = transform.rotation;
                LastFrameTouched = true;
                return;
            }
            
        
        whiteboard = null;
        LastFrameTouched = false;
            
    }
}
