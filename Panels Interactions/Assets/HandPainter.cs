using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections;
using UnityEngine.UI;

public class HandPainter : MonoBehaviour
{   
    public Transform _tip; 

    [SerializeField] GameObject HandMarker;

    [SerializeField] GameObject Marker;
    
    [HideInInspector]
    public Renderer _renderer;
    public LayerMask whiteboardMask;
    public Slider scaleSlider;
    public FlexibleColorPicker fcp;
    //private TouchColor other;
    private float transformSlider;
    private Color[] _colors;
    private float _tipHeight;

    private int tip_bulk;

    private Vector3 scale;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos,_lastTouchPos;
    private bool _touchedLastFrame, activated;
    private Quaternion _lastTouchRot;

    // Start is called before the first frame update
    void Start()
    {                   
        activated = false;
        _renderer = _tip.GetComponent<Renderer>();
        tip_bulk = (int)(scale.x * 15);
        _colors = Enumerable.Repeat(_renderer.material.color, tip_bulk * tip_bulk).ToArray();
        _tipHeight = _tip.localScale.y;


        
    }

    // Update is called once per frame
    void Update()
    {
        Draft();
        ColorChangeHP();
        SliderChangeHP();
        
    }
    private void Draft()
    {
        if (Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.up), out _touch, _tipHeight, whiteboardMask))
        {
            Pintar();
        }
        else if (Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.forward), out _touch, _tipHeight, whiteboardMask))
        {
            Pintar();
        }
        else if (Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.right), out _touch, _tipHeight, whiteboardMask))
        {   
            Pintar();
        }
        else if (Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.left), out _touch, _tipHeight, whiteboardMask))
        {   
            Pintar();
        }

        else if (Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.back), out _touch, _tipHeight, whiteboardMask))
        {   
            Pintar();
        }
        else if (Physics.Raycast(_tip.position, transform.TransformDirection(Vector3.down), out _touch, _tipHeight, whiteboardMask))
        {   
            Pintar();
        }
        else 
        {
        _whiteboard = null;
        _touchedLastFrame = false;
        }

        
        Debug.DrawRay(_tip.position, transform.TransformDirection(Vector3.up) * 100, Color.red);
        Debug.DrawRay(_tip.position, transform.TransformDirection(Vector3.forward) * 100, Color.blue);
        Debug.DrawRay(_tip.position, transform.TransformDirection(Vector3.right) * 100, Color.green);
        Debug.DrawRay(_tip.position, transform.TransformDirection(Vector3.left) * 100, Color.white);
        Debug.DrawRay(_tip.position, transform.TransformDirection(Vector3.back) * 100, Color.black);
        Debug.DrawRay(_tip.position, transform.TransformDirection(Vector3.down) * 100, Color.yellow);

       

    }
    public void HandActivator()
    {
        if (!activated)
        {

            HandMarker.SetActive(true);
            Marker.SetActive(false);
            activated = true;
        }
        else
        {
            HandMarker.SetActive(false);
            Marker.SetActive(true);
            activated = false;
        }
    }

    private void SliderChangeHP()
    {
        transformSlider = scaleSlider.value;
        scale = new Vector3(transformSlider, transformSlider, transformSlider);
        this.transform.localScale = scale;
        tip_bulk = (int)(scale.x * 15);
        _colors = Enumerable.Repeat(_renderer.material.color, tip_bulk * tip_bulk).ToArray();
        _tipHeight = _tip.localScale.y;

    }
    private void ColorChangeHP(){
        _renderer.material.color = fcp.color;
        _colors = Enumerable.Repeat(_renderer.material.color, tip_bulk * tip_bulk).ToArray();

    }
    private void Pintar()
    {
        if (_touch.transform.CompareTag("Whiteboard"))
        {
            _colors = Enumerable.Repeat(_renderer.material.color, tip_bulk * tip_bulk).ToArray();
            _tipHeight = _tip.localScale.y;
            if (_whiteboard == null)
            {
                _whiteboard = _touch.transform.GetComponent<Whiteboard>();
            }
            _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

            var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (tip_bulk / 2));
            var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (tip_bulk / 2));

            if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;
               
            if (_touchedLastFrame)
            {
                
               
                _whiteboard.texture.SetPixels(x, y, tip_bulk, tip_bulk, _colors);

                for (float f = 0.01f; f < 1.00f; f += 0.01f)
                {
                    var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                    var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                    _whiteboard.texture.SetPixels(lerpX, lerpY, tip_bulk, tip_bulk, _colors);
                }

                transform.rotation = _lastTouchRot;

                _whiteboard.texture.Apply();
            }

            _lastTouchPos = new Vector2(x, y);
            _lastTouchRot = transform.rotation;
            _touchedLastFrame = true;

            return;


        }
         _whiteboard = null;
        _touchedLastFrame = false;
    }
}
