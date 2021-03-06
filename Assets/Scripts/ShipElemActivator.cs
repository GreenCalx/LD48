using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipElemActivator : MonoBehaviour
{
    public ShipElem activated_obj;
    public bool enabled = false;

    // Start is called before the first frame update
    void Start()
    {
        enabled = activated_obj.mActivated;
        update_color();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        update_color();
    }

    private void update_color()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (!!sr)
        {
            //sr.color = (activated_obj.mActivated ? new Color(0,0.5f,0) : new Color(0.5f,0,0));
            sr.color = activated_obj.mActivated ? new Color(0.2f,0.5f,0.2f) : Color.white;
        }
    }

    public void toggle(){

        activated_obj.mActivated = !activated_obj.mActivated;

        update_color();
    }

    public void OnTriggerEnter2D(Collider2D iCol)
    {
        PlayerController pc = iCol.GetComponent<PlayerController>();
        if (!!pc)
            pc.set_activator_in_range(this);
    }

    public void OnTriggerStay2D(Collider2D iCol)
    {
        PlayerController pc = iCol.GetComponent<PlayerController>();
        if (!!pc)
        {
            if ( !pc.has_activator_in_range() )
                pc.set_activator_in_range(this);
        }
    }

    public void OnTriggerExit2D(Collider2D iCol)
    {
        PlayerController pc = iCol.GetComponent<PlayerController>();
        if (!!pc)
            pc.unset_activator_in_range();
    }
}
