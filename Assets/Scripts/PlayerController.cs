using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public  float   ship_internal_ray   = 2;
    public  float   move_theta_step   = 0.1f;
    public  float   air_travel_time = 1f;
    public  float   action_cooldown = 0.2f;
    public  Vector3 approx_epsilon = new Vector3(0.5f, 0.5f, 0f);
    
    private float       curr_theta = 0;
    private bool        in_air = false;
    private Vector3     jump_destination;
    private Vector3     jump_start_position;
    private float       dest_theta = 0f;
    private Vector3     jump_velocity = Vector3.zero;
    private bool        is_facing_right;
    private float       cooldown_before_action = 0f;
    
    private ShipElemActivator  activator_in_range;

    private bool mov_left;
    private bool mov_right;
    private bool jump;
    private bool action;

    private

    // Start is called before the first frame update
    void Start()
    {
        //curr_theta = Random.Range (0, 2*Mathf.PI);
        curr_theta = 0; //Mathf.PI;
        is_facing_right = true;
        activator_in_range = null;
        cooldown_before_action = 0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        jump = Input.GetKey("up") || Input.GetKey("w");
        mov_left = Input.GetKey("left") || Input.GetKey("a") ;
        mov_right = Input.GetKey("right") || Input.GetKey("d") ;
        action = Input.GetKey("down") || Input.GetKey("space") || Input.GetKey("s");

        interprete_inputs();

    }

    private void interprete_inputs()
    {
        if ( in_air )
        { // wait for travel
            
            updateAirPosition();

        } else 
        { // control around circle
        
            if (mov_left)
            {
                curr_theta -= move_theta_step;

                if ( is_facing_right )
                    flip_sprite();
            }
            if (mov_right)
            {
                curr_theta += move_theta_step;

                if (!is_facing_right)
                    flip_sprite();
            }

            if ( checkActionCD() )
            {
                if ( jump )
                {
                    tryJump();
                }
                else if (action)
                {
                    doAction();
                }
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

    public bool has_activator_in_range()
    {
        return (activator_in_range!=null);
    }

    private bool checkActionCD()
    {
        if ( cooldown_before_action < action_cooldown )
        {
            cooldown_before_action += Time.deltaTime;
            return false;
        }
        return true;
    }

    private void doAction()
    {
        // activate butn
        if (!!activator_in_range)
        {
            activator_in_range.toggle();
            cooldown_before_action = 0f;
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
                                                
        Vector3 point = Vector3.Lerp( transform.localPosition, 
                                      jump_destination, 
                                      air_travel_time );
        transform.localPosition = point;

        if ( eq_approx_position( jump_destination, point, approx_epsilon) )
        { 
            curr_theta = dest_theta; 
            in_air = false;
            updateGroundPosition();
        }
    }

    private bool eq_approx_position( Vector3 iPos, Vector3 iDest, Vector3 iEps )
    {
        if ( iPos == iDest )
            return true;
        //return false;
        
        return System.Math.Abs(iPos.x - iDest.x) <= iEps.x &&
               System.Math.Abs(iPos.y - iDest.y) <= iEps.y;
               
    }

    private void tryJump()
    {
        if (!in_air)
        {
            in_air = true;
            dest_theta = curr_theta + Mathf.PI;

            jump_start_position = transform.localPosition;
            jump_destination    = new Vector3  (    (ship_internal_ray * Mathf.Cos(dest_theta)), 
                                                    (ship_internal_ray * Mathf.Sin(dest_theta)), 
                                                    0
                                                );
            cooldown_before_action = 0f;
            //jump_destination += transform.localPosition;
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
