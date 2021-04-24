using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Damager
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnCollisionEnter2D(Collision2D iCol)
    {

    }

    public void OnCollisionStay2D(Collision2D iCol)
    {   
   
    }

    public void explode()
    {
        DestroyImmediate(this);
    }
}
