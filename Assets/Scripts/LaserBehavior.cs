using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Damager))]
public class LaserBehavior : ShipElem
{
    CapsuleCollider2D cc2d;
    public Animator Anim;
    public Animator LaserAnim;
    private bool lastSignal = false;
    // Start is called before the first frame update
    void Start()
    {
        cc2d = GetComponent<CapsuleCollider2D>();
        cc2d.enabled = mActivated;

        lastSignal =!mActivated;
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

    public override void DisplayElem(bool signal)
    {
        if (signal != lastSignal)
        {
            lastSignal = signal;
            base.DisplayElem(signal);

            if (signal)
            {
                Anim.SetTrigger("SHOT");
                Anim.SetBool("isShooting", true);
                Anim.SetBool("isMirror", false);
                LaserAnim.SetTrigger("SHOT");
                LaserAnim.SetBool("isShooting", true);
                LaserAnim.SetBool("isMirror", false);

            }
            else
            {
                Anim.SetTrigger("SHOT");
                Anim.SetBool("isShooting", false);
                Anim.SetBool("isMirror", true);
                LaserAnim.SetTrigger("SHOT");
                LaserAnim.SetBool("isShooting", false);
                LaserAnim.SetBool("isMirror", true);

            }
        }
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
