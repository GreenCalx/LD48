using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public float mMax = 100;
    public float mEnergy = 100;
    public float mRegen = 0;
    public float mConsumption = 0;
    public ShipElem[] mShipElems;

    public void Set_regen(float regen)
    {
        mRegen = regen;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float consumption = 0;
        foreach (ShipElem shipElem in mShipElems)
        {
            if (shipElem.mActivated)
            {
                consumption += shipElem.mConsumption;
            }
        }
        mConsumption = consumption;

        mEnergy += (mRegen - consumption) * Time.fixedDeltaTime;

        if (mEnergy <= 0)
        {
            Shutdown();
            mEnergy = 0;
        }
        else if (mEnergy > mMax)
        {
            mEnergy = mMax;
        }
    }
    
    void Shutdown()
    {
        foreach (ShipElem shipElem in mShipElems)
        {
            shipElem.mActivated = false;
        }
    }
}
