using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thursters are object that will apply force to parent in there current Forward direction
public class ThrusterBehavior : ShipElem
{
    public ShipBehavior mParent;
    public Rigidbody2D mParentBody;
    public Transform mContactPoint;

    public enum Type { Clockwise, Anticlockwise, Forward, Null };
    public Type mType = Type.Null;

    public float mForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        mParentBody = mParent.GetComponent<Rigidbody2D>();
    }

    void AddThrust()
    {
        if (mParentBody)
        {
            mParentBody.AddForceAtPosition(mContactPoint.right * mForce, mContactPoint.position);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(mActivated)
        {
            AddThrust();
            DisplayElem(true);
        } 
       else
        {
            DisplayElem(false);
        }
    }
}
