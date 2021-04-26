using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : EnemyBehaviour
{
    public float MAX_SPEED = 2.0f;
    public float LOWEST_SPEED = 6.0f;

    private float smoothTime = 0.8f;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        base.updateShipRef();
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy_called)
            return;

        go_towards_player();
    }

    void go_towards_player()
    {
        if (mShipRef!=null)
        {
            transform.position = Vector3.SmoothDamp(    transform.position, 
                                                        mShipRef.transform.position, 
                                                        ref velocity, 
                                                        smoothTime );
        }
    }

    private void follow_player()
    {
        if ( Vector3.Distance(transform.position, self_range.trackedShip.transform.position) <= min_dist_from_player)
            return;

        transform.position = Vector3.MoveTowards( transform.position, self_range.trackedShip.transform.position, max_step_for_follow);
    }
        
    public override void init()
    {
        smoothTime = Random.Range( LOWEST_SPEED, MAX_SPEED);
    }

    void OnCollisionEnter2D( Collision2D iCol )
    {
        if (iCol.gameObject.GetComponent<ShipBehavior>())
        {
            base.onDeath();
        }
    }
}
