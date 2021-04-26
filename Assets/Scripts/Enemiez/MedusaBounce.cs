using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaBounce : MonoBehaviour
{

    public float bounciness = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D iCol)
    {
        if (iCol.gameObject.GetComponent<ShipBehavior>())
        {
            Rigidbody2D rb2d = iCol.gameObject.GetComponent<Rigidbody2D>();
            if (!!rb2d)
            {
                var Direction = ( transform.position - iCol.transform.position ).normalized;
                rb2d.AddForce(Direction * bounciness, ForceMode2D.Impulse);
                rb2d.AddTorque(bounciness, ForceMode2D.Impulse);
            }
        }

    }
}
