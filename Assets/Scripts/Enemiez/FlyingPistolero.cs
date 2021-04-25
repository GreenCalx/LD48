using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPistolero : MonoBehaviour
{

    public bool follow_player;
    public GameObject missile;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void shoot_straight(Transform iProjectile)
    {
        //var RB = iProjectile.GetComponent<Rigidbody2D>();
        //var Direction = shoot_straight_direction.position - shooting_point.position;
        //RB.AddForce(Direction * mForce, ForceMode2D.Impulse);
        //RB.AddTorque(mForce, ForceMode2D.Impulse);
    }
}
