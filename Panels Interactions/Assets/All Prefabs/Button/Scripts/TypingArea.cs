using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingArea : MonoBehaviour
{
     [SerializeField] public GameObject leftHand;
     [SerializeField] public GameObject rightHand;
     [SerializeField] public GameObject leftTypingHand;
     [SerializeField] public GameObject rightTypingHand;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject==leftHand)
            leftTypingHand.SetActive(true);
        if (other.gameObject == rightHand)
            rightTypingHand.SetActive(true);
        /*
        GameObject hand;
        if (other.GetComponentInParent<Hand>() != null)
            hand = other.GetComponentInParent<Hand>().gameObject;
        else
            hand = null;

        //GameObject hand = other.GetComponentInParent<Hand>().gameObject ?? null;
        if (hand == null) return;
        if (hand == leftHand)
        {
            leftTypingHand.SetActive(true);
        }
        else if (hand = rightHand)
        {
            rightTypingHand.SetActive(true);
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == leftHand)
            leftTypingHand.SetActive(false);
        if (other.gameObject == rightHand)
            rightTypingHand.SetActive(false);
        /*
        GameObject hand;
        if (other.GetComponentInParent<Hand>() != null)
            hand = other.GetComponentInParent<Hand>().gameObject;
        else
            hand = null;
        if (hand == null) return;
        if (hand == leftHand)
        {
            leftTypingHand.SetActive(false);
        }
        else if (hand = rightHand)
        {
            rightTypingHand.SetActive(false);
        }
        */
    }

}
