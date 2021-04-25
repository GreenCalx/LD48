using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : EnemyBehaviour
{
    public GameObject mFocusedObject;
    public float mForce;
    // Start is called before the first frame update
    void Start()
    {
        base.updateShipRef();
        init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void init()
    {
        Debug.Log("init asteroid !");
        mFocusedObject = mShipRef;

        var RB = GetComponent<Rigidbody2D>();
        var Direction = (mFocusedObject.transform.position - transform.position).normalized;
        RB.AddForce(Direction * mForce, ForceMode2D.Impulse);
        RB.AddTorque(mForce, ForceMode2D.Impulse);
    }
}
