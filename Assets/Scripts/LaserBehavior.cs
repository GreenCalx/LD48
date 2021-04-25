using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Damager))]
public class LaserBehavior : ShipElem
{
    CapsuleCollider2D cc2d;

    // Start is called before the first frame update
    void Start()
    {
        mConsumption = 1;

        cc2d = GetComponent<CapsuleCollider2D>();
        cc2d.enabled = mActivated;
    }

    public void Update()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DisplayElem(mActivated);
        cc2d.enabled = mActivated;
    }

    void OnTriggerStay2D(Collider2D iCol)
    {
        var collisionDamageable = iCol.gameObject.GetComponent<Damageable>();
        if (collisionDamageable)
        {
            collisionDamageable.HitMe(GetComponent<Damager>().GetDamage());
        }
    }

    void OnTriggerEnter2D(Collider2D iCol)
    {
        var collisionDamageable = iCol.gameObject.GetComponent<Damageable>();
        if (collisionDamageable)
        {
            collisionDamageable.HitMe(GetComponent<Damager>().GetDamage());
        }
    }

}
