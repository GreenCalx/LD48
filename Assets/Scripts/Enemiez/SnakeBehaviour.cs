using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SnakeBehaviour : EnemyBehaviour
{
    public float MAX_SPEED = 2.0f;
    public float LOWEST_SPEED = 6.0f;
    public bool facing_right;

    private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        base.updateShipRef();
        init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (destroy_called)
            return;

        move();
    }

    public void move()
    {
        if (facing_right)
        {
            transform.position += new Vector3( speed, 0, 0);
        } else {
            transform.position -= new Vector3( speed, 0, 0);
        }
    }

    public override void init()
    {
        speed = Random.Range( LOWEST_SPEED, MAX_SPEED);
        if (facing_right)
        { // sprite faces left
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.flipX          = !sr.flipX;
        }
    }

}
