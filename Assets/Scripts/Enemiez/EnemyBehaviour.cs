using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public bool mIsTracked = false;
    protected GameObject mShipRef = null;
    protected bool destroy_called = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void onDeath()
    {
        destroy_called = true;

        ScanBehavior sb = mShipRef.GetComponentInChildren<ScanBehavior>();
        if (!!sb)
        {
            sb.removeFromScan(this.gameObject);
            removeFromScan();
        }

        Destroy(this.gameObject);
    }

    public virtual void init()
    { 

    }
    
    public void updateShipRef()
    {
        mShipRef = GameObject.Find("Ship");
        if (mShipRef==null)
            Debug.LogError("Failed to retrieve ship ref in the scene in EnemyBehaviour super class.");

        addToScan();
    }

    public void addToScan()
    {
        ScanBehavior sb = mShipRef.GetComponentInChildren<ScanBehavior>();
        if (!!sb)
        {
            if (!sb.mObjects.Contains(this.gameObject))
                sb.mObjects.Add(this.gameObject);
        }
    }

    public void removeFromScan()
    {

    }

}
