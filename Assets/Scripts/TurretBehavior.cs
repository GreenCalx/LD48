using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : ShipElem
{
    public enum Type { Port, Starbord, Null };
    public Type mType = Type.Null;
    
    public float mNumberOfMissiles;
    public float mForce;
    public float mFireRate;
    
    public GameObject missile;
    public GameObject guided_missile;
    public Transform shooting_point; //is child transform, could be retrieved
    public Transform shoot_straight_direction; //is child transform, could be retrieved

    private List<Missile> invoked_missiles;
    protected float time_since_last_shot;
    protected List<Damageable> tracked_damageables;

    // Start is called before the first frame update
    void Start()
    {
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
        GameObject invoked_go = null;

        // Try find a target
        if ( tracked_damageables.Count != 0 )
        {
            // Find target between targeted fucckers
            Damageable target = null;
            foreach( Damageable d in tracked_damageables )
            {
                if (!d) continue;

                EnemyBehaviour eb = d.gameObject.GetComponent<EnemyBehaviour>();
                if ( eb && !eb.mIsTracked)
                { target = d; break; }
            }

            // If no new target, take the first one in the list atm
            // maybe get the closest?
            if (target == null)
                target = tracked_damageables[0];

            // Invoke right Missile
            invoked_go = Instantiate(guided_missile);
    
            GuidedMissile new_missile = invoked_go.GetComponent<GuidedMissile>();
            invoked_missiles.Add(new_missile);
    
            new_missile.target = target.transform;
            //shoot_target(new_missile.transform, target.transform);
            shoot_guided_missile(new_missile.transform);
        } else {
            // Or shoot straight-y

            invoked_go = Instantiate(missile);
            Missile new_missile = invoked_go.GetComponent<Missile>();
            invoked_missiles.Add(new_missile);
            shoot_straight(new_missile.transform);
        }

        // Set Missile position
        invoked_go.transform.position = transform.position;

        // check if we need to autodestroy some missilez
        checkInvokedMissileSize();

        // reset CD to finish
        time_since_last_shot = 0f;

    }

    private void checkInvokedMissileSize()
    {
        if ( mNumberOfMissiles < 0 )
            return;
        
        invoked_missiles.RemoveAll(item => item == null);
        while ( invoked_missiles.Count > mNumberOfMissiles )
        {
            Missile to_explode = invoked_missiles[0];
            if (!!to_explode)
            {
                invoked_missiles.Remove(to_explode);
                to_explode.explode();
                invoked_missiles.RemoveAll(item => item == null);
            }
        }
    }

    private void shoot_target(Transform iProjectile, Transform iTarget)
    {
        var RB = iProjectile.GetComponent<Rigidbody2D>();
        var Direction = (iTarget.position - shooting_point.position).normalized;
        RB.AddForce(Direction * mForce, ForceMode2D.Impulse);
        RB.AddTorque(mForce, ForceMode2D.Impulse);

    }

    private void shoot_straight(Transform iProjectile)
    {
        var RB = iProjectile.GetComponent<Rigidbody2D>();
        var Direction = shoot_straight_direction.position - shooting_point.position;
        RB.AddForce(Direction * mForce, ForceMode2D.Impulse);
        RB.AddTorque(mForce, ForceMode2D.Impulse);
    }

    private void shoot_guided_missile(Transform iProjectile)
    {
        var RB = iProjectile.GetComponent<Rigidbody2D>();
        var Direction = shoot_straight_direction.position - shooting_point.position;
        var dampened_force = mForce/4;

        RB.AddForce(Direction * dampened_force, ForceMode2D.Impulse);
        RB.AddTorque(dampened_force, ForceMode2D.Impulse);
    }
}
