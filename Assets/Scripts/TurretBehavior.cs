using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : ShipElem
{
    public enum Type { Port, Starbord, Null };
    public Type mType = Type.Null;

    // Start is called before the first frame update
    void Start()
    {
        mConsumption = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mActivated)
        {
            DisplayElem(true);
        }
        else
        {
            DisplayElem(false);
        }
    }
}
