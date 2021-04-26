using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPistolero : MonoBehaviour
{

    public bool should_follow_player = false;
    public float max_step_for_follow = 0.1f;

    public float min_dist_from_player = 1f;
    public GameObject missile;
    public float fire_rate;
    public float force;

    private EnemyShootingRange self_range;
    protected float time_since_last_shot;

    public Transform shooting_point;

    public Animator animator;
    public SpriteRenderer renderer;


    // Start is called before the first frame update
    void Start()
    {
        self_range = GetComponentInChildren<EnemyShootingRange>();
    }

    void Update()
    {
        if ( !!self_range && self_range.hasTrackedShip() )
        {
            if ( time_since_last_shot < fire_rate )
            {
                time_since_last_shot += Time.deltaTime;
                animator.SetBool("Shooting", false);
            }
            else {
                fire(self_range.trackedShip.transform);
            }

            if (should_follow_player)
                follow_player();
        }
    }

    private void follow_player()
    {
        if ( Vector3.Distance(transform.position, self_range.trackedShip.transform.position) <= min_dist_from_player)
            return;

        transform.position = Vector3.MoveTowards( transform.position, self_range.trackedShip.transform.position, max_step_for_follow);

        if (transform.position.x >= self_range.trackedShip.transform.position.x) renderer.flipX = true;
        else renderer.flipX = false;
    }

    private void fire( Transform iTarget )
    {
        GameObject invoked_go = Instantiate(missile, transform.position, Quaternion.identity);
        shoot_player(invoked_go.transform, iTarget);
        time_since_last_shot = 0;

        animator.SetBool("Shooting", true);
    }

    private void shoot_player(Transform iProjectile, Transform iTarget)
    {
        var RB = iProjectile.GetComponent<Rigidbody2D>();
        var Direction = (iTarget.transform.position - transform.position).normalized;
        RB.AddForce(Direction * force, ForceMode2D.Impulse);
        RB.AddTorque(force, ForceMode2D.Impulse);
    }
}
