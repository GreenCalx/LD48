using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Damager
{
    public Transform target;
    private bool death_called = false;

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
        explode();
    }

    public void OnCollisionStay2D(Collision2D iCol)
    {   
        explode();
    }

    public void explode()
    {
        if (death_called)
            return;
        death_called = true;

        Destroy(this.gameObject);
    }
}
