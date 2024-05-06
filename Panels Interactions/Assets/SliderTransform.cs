using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTransform : MonoBehaviour
{   
    public Slider scaleSlider;

    private float transformSlider;
 

    // Update is called once per frame
    void Update()
    {
        transformSlider = scaleSlider.value;
        Vector3 scale = new Vector3(transformSlider, transformSlider, transformSlider);
        this.transform.localScale = scale;
    }
}
