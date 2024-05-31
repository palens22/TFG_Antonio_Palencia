using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ResaultSpawn : MonoBehaviour
{
    Vector3 handScale=new Vector3(0.01988079f, 0.01967861f, 0.001531802f);
    Vector3 grabScale = new Vector3(0.1097102f*2, 0.1085945f * 2, 0.0084531f * 2);
  
    private Vector3 initialPos;
    private Quaternion initialRot;
    private Transform parent;

    [SerializeField] GameObject prefabOpiniones;

    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject leftTypingHand;
    [SerializeField] GameObject rightTypingHand;

    private void Start()
    {
        initialPos = transform.localPosition;
        initialRot = transform.localRotation;

        parent = this.gameObject.transform.parent;

    }

    public void unselectedHand()
    {

        var g=Instantiate(prefabOpiniones, transform.position, Quaternion.identity);

        g.transform.position = new Vector3 (g.transform.position.x,g.transform.position.y,g.transform.position.z+.5f);



        g.GetComponentInChildren<TypingArea>().leftHand = leftHand;
        g.GetComponentInChildren<TypingArea>().rightHand = rightHand;
        g.GetComponentInChildren<TypingArea>().leftTypingHand = leftTypingHand;
        g.GetComponentInChildren<TypingArea>().rightTypingHand = rightTypingHand;

        transform.localScale = handScale;
        transform.localPosition = initialPos;
        transform.localRotation = initialRot;
        
    }

}
