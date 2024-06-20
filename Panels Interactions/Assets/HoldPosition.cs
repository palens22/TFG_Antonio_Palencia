using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPosition : MonoBehaviour
{       
    public Transform controller;
    public float minDistance = 2.0f;
    public Transform followTransform;
    Vector3 relativePosition;
    bool onBoard = false;
    float limitX;
    Quaternion relativeRotation;
    // Start is called before the first frame update
    void Start()
    {   
        relativePosition = transform.localPosition;
        relativeRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {     
        if(!onBoard){
            transform.position = followTransform.position;
            transform.localRotation = relativeRotation;  
        }
        else if(onBoard){
            transform.localRotation = relativeRotation; 
            transform.position = new Vector3(limitX,followTransform.position.y,followTransform.position.z);
          

            }
            else 
            {
                transform.position = new Vector3(limitX,transform.position.y,transform.position.z);
            }
            

        
        if(Mathf.Abs(transform.position.x - controller.position.x)>minDistance)
        {
            onBoard = false;
        }

    }
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Whiteboard"){
            onBoard = true;
            Vector3 closest = collider.ClosestPoint(transform.position);
            limitX = closest.x;
        }

    }
}
