using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliceMovement : MonoBehaviour
{
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value <= -175 )
        {
            slider.value = 172;

        }
        else if (slider.value >= 175)
        {
            slider.value = -173;


        }
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = slider.value;
        transform.rotation = Quaternion.Euler(rotationVector);
        
    }
}
