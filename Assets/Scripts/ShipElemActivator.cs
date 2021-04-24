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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggle(){

        activated_obj.mActivated = !activated_obj.mActivated;

        /// DEBUG
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (!!sr)
        {
            sr.color = activated_obj.mActivated ? Color.green : Color.red;
        }
    }

    public void OnTriggerEnter2D(Collider2D iCol)
    {
        PlayerController pc = iCol.GetComponent<PlayerController>();
        if (!!pc)
            pc.set_activator_in_range(this);
    }

    public void OnTriggerExit2D(Collider2D iCol)
    {
        PlayerController pc = iCol.GetComponent<PlayerController>();
        if (!!pc)
            pc.unset_activator_in_range();
    }
}
