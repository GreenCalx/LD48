using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDamageable : Damageable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ExecuteOnCollide(Collision2D collision)
    {
        var shield = GetComponentInParent<ShieldBehavior>();
        if (shield.mActivated)
        {
            // go bounce
        }
    }
}
