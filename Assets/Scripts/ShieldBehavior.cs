using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : ShipElem
{
    public enum Type { Port, Starbord, Null };
    public Type mType = Type.Null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hitbox = GetComponent<PolygonCollider2D>();

        if (mActivated)
        {
            DisplayElem(true);
            hitbox.enabled = true;
        }
        else
        {
            DisplayElem(false);
            hitbox.enabled = false;
        }
    }
}
