using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUIrotation : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRotation(float angle)
    {
        foreach (Transform child in transform)
        {
            child.eulerAngles = new Vector3(0f, 0f, angle + 90);
        }
    }
}
