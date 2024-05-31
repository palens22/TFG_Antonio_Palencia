using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResultDelete : MonoBehaviour
{
    
    public void ResultHandled(GameObject g)
    {
        g.transform.parent = null;
    }
    public void Destroy()
    {
        var t = GetComponentInChildren<TypingArea>().leftTypingHand;
        t.SetActive(false);
        t = GetComponentInChildren<TypingArea>().rightTypingHand;
        t.SetActive(false);

        Destroy(this.gameObject);
    }
}
