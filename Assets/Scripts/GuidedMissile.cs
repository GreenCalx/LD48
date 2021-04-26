using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : Missile
{
    public float    smoothTime  = 0.3f;
    public float    max_speed = 2f;
    private Vector3 velocity    = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        time_alive = 0f;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (!!sr)
            sr.color = Color.yellow;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        chase_target();

        time_alive += Time.deltaTime;
        if ( time_alive >= lifespan )
            explode();
    }

    public void chase_target()
    {
        if (target!=null)
        {
            transform.position = Vector3.SmoothDamp(    transform.position, 
                                                        target.position, 
                                                        ref velocity, 
                                                        smoothTime,
                                                        max_speed );
        }
    }
}
