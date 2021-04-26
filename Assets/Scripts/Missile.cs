using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Damager
{
    public Transform target;
    private bool death_called = false;

    public float lifespan = 10f;
    protected float time_alive = 0f;

    // Start is called before the first frame update
    void Start()
    {
        time_alive = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        time_alive += Time.deltaTime;
        if ( time_alive >= lifespan )
            explode();
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
