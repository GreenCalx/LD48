using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatMissile : Missile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D iCol)
    {
        if (iCol.GetComponent<ShipBehavior>())
        {
            Damageable d = iCol.GetComponent<Damageable>();
            Damager dmger = GetComponent<Damager>();
            if (!!d)
                d.HitMe(dmger.mDamage);
            explode();
        }

    }

    void OnTriggerStay2D(Collider2D iCol)
    {
        if (iCol.GetComponent<ShipBehavior>())
        {
            Damageable d = iCol.GetComponent<Damageable>();
            Damager dmger = GetComponent<Damager>();
            if (!!d)
                d.HitMe(dmger.mDamage);
            explode();
        } 
        
        if (  iCol.GetComponent<ShipBehavior>() )
        {
            Debug.Log("hit shield");
            explode();
        }
    }

}
