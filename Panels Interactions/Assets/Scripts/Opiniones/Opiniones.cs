using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opiniones : MonoBehaviour
{
    [Header("Variables para instanciar en posicion correcta")]
    //[SerializeField] GameObject opiniones;
    [SerializeField] Transform targetLookPos;
    [SerializeField] Transform targetTransform;
    [SerializeField] GameObject prefabOpiniones;

    [Header("Variables para el prefab")]
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject leftTypingHand;
    [SerializeField] GameObject rightTypingHand;

    GameObject antOpiniones = null;
    /*public void ChangeVisibility()
    {
        opiniones.SetActive(!opiniones.activeSelf);
    }*/

    public void ChangeVisibility()
    {

        if (antOpiniones != null) Destroy(antOpiniones);
        var g = Instantiate(prefabOpiniones, targetLookPos.position, Quaternion.identity);

        g.GetComponentInChildren<TypingArea>().leftHand = leftHand;
        g.GetComponentInChildren<TypingArea>().rightHand = rightHand;
        g.GetComponentInChildren<TypingArea>().leftTypingHand = leftTypingHand;
        g.GetComponentInChildren<TypingArea>().rightTypingHand = rightTypingHand;


        g.transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y +1.7f, targetTransform.position.z);
        g.transform.forward = -targetLookPos.transform.forward;
        //g.transform.LookAt(targetLookPos);

        antOpiniones = g;


    }

    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.pKey.wasPressedThisFrame)
        {
            ChangeVisibility();
        }
    }

}
