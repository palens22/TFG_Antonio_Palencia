using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour
{   

    public Slider scalePost;
    public FlexibleColorPicker fcp;

    public GameObject result1, result2;

    private float transformPost;
    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScalePost();
        ColorPost();
    }

    private void ScalePost()
    {
        transformPost = scalePost.value;
        scale = new Vector3(transformPost, 0.0006f, transformPost);
        result1.transform.localScale = scale;
        result2.transform.localScale = scale;

    }
    private void ColorPost(){

        result1.GetComponent<MeshRenderer> ().material.color = fcp.color;
        result1.GetComponent<MeshRenderer> ().material.color = fcp.color;

    }
}
