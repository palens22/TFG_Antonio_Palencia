using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections;
using UnityEngine.UI;

public class WhiteboardMarker : MonoBehaviour
{   
    public Transform _tip; 
    //public int _penSize;
    public Slider scaleSlider;
    public FlexibleColorPicker fcp;

    private float transformSlider;
    private Renderer _renderer;
    //private TouchColor other;
    private Color[] _colors;
    private float _tipHeight;

    private int tip_bulk;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector3 scale;
    private Vector2 _touchPos,_lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        tip_bulk = (int)(scale.x * 15);
        _colors = Enumerable.Repeat(_renderer.material.color, tip_bulk * tip_bulk).ToArray();
        _tipHeight = _tip.localScale.y;


        
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
        if(Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.up), out _touch, _tipHeight))
        {
            RaycastPixels();
        }
        else if (Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.forward), out _touch, _tipHeight))
        {
            RaycastPixels();

        }
        else if (Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.right), out _touch, _tipHeight))
        {   
            RaycastPixels();
        }
        else if (Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.left), out _touch, _tipHeight))
        {   
            
            RaycastPixels();

        }

        else if (Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.back), out _touch, _tipHeight))
        {   
            RaycastPixels();
        }
        else 
        {
        _whiteboard = null;
        _touchedLastFrame = false;
        }
        


    }
    private void OnCollisionEnter(Collision collision){
          if(collision.gameObject.tag == "ColorCube"){
                var new_color = collision.transform.gameObject.GetComponent<Renderer>().material.color;
                _renderer.material.color = new_color;
                _colors = Enumerable.Repeat(_renderer.material.color, tip_bulk * tip_bulk).ToArray();
                
            }
    }
    private void SliderChange()
    {
        transformSlider = scaleSlider.value;
        scale = new Vector3(transformSlider, transformSlider, transformSlider);
        this.transform.localScale = scale;
        tip_bulk = (int)(scale.x * 15);
        _colors = Enumerable.Repeat(_renderer.material.color, tip_bulk * tip_bulk).ToArray();
        _tipHeight = _tip.localScale.y;

    }
    private void ColorChange(){
        _renderer.material.color = fcp.color;
        _colors = Enumerable.Repeat(_renderer.material.color, tip_bulk * tip_bulk).ToArray();

    }
    private void RaycastPixels()
    {

         if(_touch.transform.CompareTag("Whiteboard"))
            {
                if(_whiteboard ==null )
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (tip_bulk/2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (tip_bulk/2));

                if(y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;

                if(_touchedLastFrame){

                    _whiteboard.texture.SetPixels(x,y,tip_bulk,tip_bulk, _colors);

                    for (float f = 0.01f; f < 1.00f; f+= 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x,x,f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y,y,f);
                        _whiteboard.texture.SetPixels(lerpX,lerpY,tip_bulk,tip_bulk,_colors);
                    }

                    transform.rotation = _lastTouchRot;
                    
                    _whiteboard.texture.Apply();
                }
                _lastTouchPos = new Vector2(x,y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
            
        
        _whiteboard = null;
        _touchedLastFrame = false;
            
    }
}
