using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.OpenXR.Input;

public class HandPainter : MonoBehaviour
{   
    public Transform _point; 

    [SerializeField] GameObject HandMarker;

    [SerializeField] GameObject Marker;
    
    [HideInInspector]
    public Renderer render;
    public LayerMask whiteboardMask;
    public Slider scaleSlider;
    public FlexibleColorPicker fcp;
    //private touchColor other;
    private float transformSlider, _pointHeight;
    private Color[] colors;
    private int tip_bulk;

    private Vector3 scale;

    private RaycastHit touch, hit;
    private Whiteboard whiteboard;
    private Vector2 Postouch,LastPostouched, LastPosHitted, Posthits;
    private bool LastFrametouched, activated, hitLastFrame;
    private Quaternion LastRottouched, LastRotHitted;
    
    private InputAction drawAction;

    public float rayLength = 5f; // Length of the ray

    private LineRenderer _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {                   
        activated = false;
        render = _point.GetComponent<Renderer>();
        tip_bulk = 15;
        colors = Enumerable.Repeat(render.material.color, tip_bulk * tip_bulk).ToArray();
        _pointHeight = _point.localScale.y;

        _lineRenderer = GetComponent<LineRenderer>();
           // Initialize the input action for the trigger button
        drawAction = new InputAction(type: InputActionType.Button, binding: "<XRController>{RightHand}/primaryButton");
        drawAction.Enable();
        
    }

    // Update is called once per frame
    void Update()
    {
        Draft();
        Draw();
        ColorChangeHP();
       

    }
    private void Draft()
    {
        if (Physics.Raycast(_point.position, transform.TransformDirection(Vector3.up), out touch, _pointHeight, whiteboardMask))
        {   
            Pintar();
        }
        else if (Physics.Raycast(_point.position, transform.TransformDirection(Vector3.forward), out touch, _pointHeight, whiteboardMask))
        {   
            Pintar();
        }
        else if (Physics.Raycast(_point.position, transform.TransformDirection(Vector3.right), out touch, _pointHeight, whiteboardMask))
        {   
            Pintar();
        }
        else if (Physics.Raycast(_point.position, transform.TransformDirection(Vector3.left), out touch, _pointHeight, whiteboardMask))
        {   
            Pintar();
        }

        else if (Physics.Raycast(_point.position, transform.TransformDirection(Vector3.back), out touch, _pointHeight, whiteboardMask))
        {   
            Pintar();
        }
        else if (Physics.Raycast(_point.position, transform.TransformDirection(Vector3.down), out touch, _pointHeight, whiteboardMask))
        {   
            Pintar();
        }
        else 
        {
        whiteboard = null;
        LastFrametouched = false;
        }

        
        Debug.DrawRay(_point.position, transform.TransformDirection(Vector3.up) * 100, Color.red);
        Debug.DrawRay(_point.position, transform.TransformDirection(Vector3.forward) * 100, Color.blue);
        Debug.DrawRay(_point.position, transform.TransformDirection(Vector3.right) * 100, Color.green);
        Debug.DrawRay(_point.position, transform.TransformDirection(Vector3.left) * 100, Color.white);
        Debug.DrawRay(_point.position, transform.TransformDirection(Vector3.back) * 100, Color.black);
        Debug.DrawRay(_point.position, transform.TransformDirection(Vector3.down) * 100, Color.yellow);

       

    }
    public void HandActivator()
    {
        if (!activated)
        {

            HandMarker.SetActive(true);
            Marker.SetActive(false);
            scaleSlider.gameObject.SetActive(false);
            activated = true;
        }
        else
        {
            HandMarker.SetActive(false);
            Marker.SetActive(true);
            scaleSlider.gameObject.SetActive(true);
            activated = false;
        }
    }
    private void ColorChangeHP(){
        render.material.color = fcp.color;
        colors = Enumerable.Repeat(render.material.color, tip_bulk * tip_bulk).ToArray();

    }
    private void UpdateLineRenderer(Vector3 start, Vector3 end)
    {
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);
    }
    private void Draw()
    {
        // Check if the draw action is pressed
        if (Physics.Raycast(_point.position, transform.TransformDirection(Vector3.up), out hit, rayLength, whiteboardMask))
            {
                UpdateLineRenderer(_point.position, hit.point);
                if (drawAction.ReadValue<float>() > 0.1f)
                     {

                        PintarDistancia();
                     }
                else
                {
                    whiteboard = null;
                    hitLastFrame = false;
                }
            }
        
    }
    private void Pintar()
    {
        if (touch.transform.CompareTag("Whiteboard"))
        {
            if (whiteboard == null)
            {
                whiteboard = touch.transform.GetComponent<Whiteboard>();
            }
            Postouch = new Vector2(touch.textureCoord.x, touch.textureCoord.y);

            var x = (int)(Postouch.x * whiteboard.textureSize.x - (tip_bulk / 2));
            var y = (int)(Postouch.y * whiteboard.textureSize.y - (tip_bulk / 2));

            if (y < 0 || y > whiteboard.textureSize.y || x < 0 || x > whiteboard.textureSize.x) return;
               
            if (LastFrametouched)
            {
                whiteboard.texture.SetPixels(x, y, tip_bulk, tip_bulk, colors);

                for (float f = 0.01f; f < 1.00f; f += 0.01f)
                {
                    var lerpX = (int)Mathf.Lerp(LastPostouched.x, x, f);
                    var lerpY = (int)Mathf.Lerp(LastPostouched.y, y, f);
                    whiteboard.texture.SetPixels(lerpX, lerpY, tip_bulk, tip_bulk, colors);
                }

                transform.rotation = LastRottouched;

                whiteboard.texture.Apply();
            }

            LastPostouched = new Vector2(x, y);
            LastRottouched = transform.rotation;
            LastFrametouched = true;

            return;


        }
        whiteboard = null;
        LastFrametouched = false;
    }
    private void PintarDistancia()
    {
        if (hit.transform.CompareTag("Whiteboard"))
        {
            colors = Enumerable.Repeat(render.material.color, tip_bulk * tip_bulk).ToArray();
            if (whiteboard == null)
            {
                whiteboard = hit.transform.GetComponent<Whiteboard>();
            }
            Posthits = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            Debug.Log(hit.textureCoord.x);
            Debug.Log(hit.textureCoord.y);
            var x = (int)(Posthits.x * whiteboard.textureSize.x - (tip_bulk / 2));
            var y = (int)(Posthits.y * whiteboard.textureSize.y - (tip_bulk / 2));

            if (y < 0 || y > whiteboard.textureSize.y || x < 0 || x > whiteboard.textureSize.x) return;
               
            if (hitLastFrame)
            {
                
                whiteboard.texture.SetPixels(x, y, tip_bulk, tip_bulk, colors);

                for (float f = 0.01f; f < 1.00f; f += 0.01f)
                {
                    var lerpX = (int)Mathf.Lerp(LastPosHitted.x, x, f);
                    var lerpY = (int)Mathf.Lerp(LastPosHitted.y, y, f);
                    whiteboard.texture.SetPixels(lerpX, lerpY, tip_bulk, tip_bulk, colors);
                }

                transform.rotation = LastRotHitted;

                whiteboard.texture.Apply();
            }

            LastPosHitted = new Vector2(x, y);
            LastRotHitted = transform.rotation;
            hitLastFrame = true;

            return;


        }
        whiteboard = null;
        hitLastFrame = false;
    }
}
