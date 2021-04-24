using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public  float   ship_internal_ray   = 2;
    public  float   move_theta_step   = 0.1f;
    public  float   air_travel_time = 1f;
    public  Vector3 approx_epsilon = new Vector3(0.5f, 0.5f, 0f);
    
    private float       curr_theta = 0;
    private bool        in_air = false;
    private Vector3     jump_destination;
    private float       dest_theta = 0f;
    private Vector3     jump_velocity = Vector3.zero;
    private bool        is_facing_right;
    
    private ShipElemActivator  activator_in_range;

    // Start is called before the first frame update
    void Start()
    {
        curr_theta = Random.Range (0, 2*Mathf.PI);
        is_facing_right = true;
        activator_in_range = null;
    }

    // Update is called once per frame
    void Update()
    {
        if ( in_air )
        { // wait for travel
            
            updateAirPosition();

        } else 
        { // control around circle
        
            if (Input.GetKeyDown("space") )
            {
                tryJump();
            }
            if (Input.GetKey("left"))
            {
                curr_theta -= move_theta_step;

                if ( is_facing_right )
                    flip_sprite();
            }
            if (Input.GetKey("right"))
            {
                curr_theta += move_theta_step;

                if (!is_facing_right)
                    flip_sprite();
            }
            if (Input.GetKeyDown("down"))
            {
                doAction();
            }
            
            updateGroundPosition();
        }      


    }

    public void set_activator_in_range(ShipElemActivator iActivator)
    {
        activator_in_range = iActivator;
    }

    public void unset_activator_in_range()
    {
        activator_in_range = null;
    }

    private void doAction()
    {
        // activate butn
        if (!!activator_in_range)
        {
            activator_in_range.toggle();
        }
    }

    private void updateGroundPosition()
    {
        Vector3 point = new Vector3 (   (ship_internal_ray * Mathf.Cos(curr_theta)), 
                                        (ship_internal_ray * Mathf.Sin(curr_theta)), 
                                        0
                                    );
        transform.localPosition = point;
    }

    private void updateAirPosition()
    {
        Vector3 point = Vector3.SmoothDamp( transform.localPosition, 
                                            jump_destination, 
                                            ref jump_velocity, 
                                            air_travel_time);
        transform.localPosition = point;
        if ( eq_approx_position( jump_destination, point, approx_epsilon) )
        { 
            transform.localPosition = jump_destination;
            in_air = false; 
            curr_theta = dest_theta; 
        }
    }

    private bool eq_approx_position( Vector3 iPos, Vector3 iDest, Vector3 iEps )
    {
        if ( iPos == iDest )
            return true;

        return System.Math.Abs(iPos.x - iDest.x) <= iEps.x &&
               System.Math.Abs(iPos.y - iDest.y) <= iEps.y;
    }

    private void tryJump()
    {
        if (!in_air)
        {
            in_air = true;
            dest_theta = curr_theta + Mathf.PI;
            jump_destination = new Vector3  (   (ship_internal_ray * Mathf.Cos(dest_theta)), 
                                                (ship_internal_ray * Mathf.Sin(dest_theta)), 
                                                0
                                            );
        }
    }

    private void flip_sprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (!!sr)
        {
            is_facing_right = !is_facing_right;
            sr.flipX        = !sr.flipX;
        }
    }
    
}
