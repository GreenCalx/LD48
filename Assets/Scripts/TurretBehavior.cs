using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : ShipElem
{
    public enum Type { Port, Starbord, Null };
    public Type mType = Type.Null;
    
    public float mForce;
    public float mFireRate;
    
    public GameObject missile;
    public Transform shoot_straight_direction; //is child transform, could be retrieved

    private List<Missile> invoked_missiles;
    protected float time_since_last_shot;
    protected List<Damageable> tracked_damageables;

    // Start is called before the first frame update
    void Start()
    {
        mConsumption = 1;
        invoked_missiles = new List<Missile>(0);
        tracked_damageables = new List<Damageable>(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mActivated)
        {
            DisplayElem(true);
            if ( time_since_last_shot < mFireRate )
            {
                time_since_last_shot += Time.deltaTime;
            } else {
                // TODO check its the first activation and shoot
                // a missile before the CD.
                fire();
            }
        }
        else
        {
            DisplayElem(false);
        }
    }

    public void OnTriggerEnter2D( Collider2D iCol)
    {
        Damageable d = iCol.GetComponent<Damageable>();
        if ( !!d )
            trackEnemy(d);
    }

    // TODO optimize here if its too cpu heavy at some point
    // Dont do this when it may already be tracked.
    public void OnTriggerStay2D( Collider2D iCol) 
    {
        Damageable d = iCol.GetComponent<Damageable>();
        if ( !!d && !tracked_damageables.Contains(d) )
            trackEnemy(d);
    }

    public void OnTriggerExit2D( Collider2D iCol)
    {
        Damageable d = iCol.GetComponent<Damageable>();
        if (!!d)
            tryReleaseTrack(d);
    }
    
    protected void trackEnemy(Damageable iCol)
    {
        if (!tracked_damageables.Contains(iCol))
            tracked_damageables.Add(iCol);
    }

    protected void tryReleaseTrack(Damageable iCol)
    {
        if (tracked_damageables.Contains(iCol))
            tracked_damageables.Remove(iCol);
    }

    protected void fire()
    {
        GameObject invoked_go = Instantiate(missile);

        Missile new_missile = invoked_go.GetComponent<Missile>();
        invoked_missiles.Add(new_missile);

        invoked_go.transform.position = transform.position;

        // Try find a target
        if ( tracked_damageables.Count != 0 )
        {
            // Find target between targeted fucckers
            Damageable target = null;
            foreach( Damageable d in tracked_damageables )
            {
                EnemyBehaviour eb = d.gameObject.GetComponent<EnemyBehaviour>();
                if ( eb && !eb.mIsTracked)
                { target = d; break; }
            }

            // If no new target, take the first one in the list atm
            // maybe get the closest?
            if (target == null)
                target = tracked_damageables[0];

            new_missile.target = target.transform;
            shoot_target(new_missile.transform, target.transform);

        } else {
            // Or shoot straight-y
            Debug.Log("Shoot straight-");
            shoot_straight(new_missile.transform);
        }

        // reset CD to finish
        time_since_last_shot = 0f;

    }

    private void shoot_target(Transform iProjectile, Transform iTarget)
    {
        var RB = iProjectile.GetComponent<Rigidbody2D>();
        var Direction = (iTarget.position - transform.position).normalized;
        RB.AddForce(Direction * mForce, ForceMode2D.Impulse);
        RB.AddTorque(mForce, ForceMode2D.Impulse);
    }

    private void shoot_straight(Transform iProjectile)
    {
        var RB = iProjectile.GetComponent<Rigidbody2D>();
        var Direction = shoot_straight_direction.position;
        Debug.DrawRay( transform.position, Direction * mForce, Color.red);
        RB.AddForce(Direction * mForce, ForceMode2D.Impulse);
        RB.AddTorque(mForce, ForceMode2D.Impulse);
    }
}
