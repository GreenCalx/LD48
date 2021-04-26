using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroRange : MonoBehaviour
{
    public GameObject trackedShip;

    // Start is called before the first frame update
    void Start()
    {
        trackedShip = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool hasTrackedShip()
    {
        return (trackedShip!=null);
    }

    void OnTriggerStay2D(Collider2D iCol)
    {
        if (iCol.GetComponent<ShipBehavior>() && !hasTrackedShip())
        {
            trackedShip = iCol.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D iCol)
    {
        if (iCol.GetComponent<ShipBehavior>())
        {
            trackedShip = null;
        }   
    }
}
