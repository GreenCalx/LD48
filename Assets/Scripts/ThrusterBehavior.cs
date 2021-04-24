using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thursters are object that will apply force to parent in there current Forward direction
public class ThrusterBehavior : ShipElem
{
    public ShipBehavior mParent;
    public Rigidbody2D mParentBody;

    public enum Type { Clockwise, Anticlockwise, Forward, Null };
    public Type mType = Type.Null;

    public float mForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        mParentBody = mParent.GetComponent<Rigidbody2D>();
        mConsumption = 1;
    }

    void AddThrust()
    {
        if (mParentBody)
        {
            mParentBody.AddForceAtPosition(transform.right * mForce, transform.position);
        }
    }

    void DisplayThrust(bool True)
    {
        var Renderer = GetComponentInChildren<SpriteRenderer>();
        Renderer.enabled = True;

        var Audio = GetComponentInChildren<AudioSource>();
        if (True)
        {
            if (!Audio.isPlaying)
                Audio.Play();
        }
        else
            Audio.Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(mActivated)
        {
            AddThrust();
            DisplayThrust(true);
        } 
       else
        {
            DisplayThrust(false);
        }
    }
}
