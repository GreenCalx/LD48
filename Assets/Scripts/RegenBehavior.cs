using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegenBehavior : MonoBehaviour
{
    public Battery mBattery;
    public Image regenPositive;
    public Image regenNegative;

    // Start is called before the first frame update
    void Start()
    {
        MakeGraph();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Destroy(transform.GetChild(0).gameObject);
        MakeGraph();
    }

    void MakeGraph()
    {
        float mRegen = mBattery.mRegen - mBattery.mConsumption;

        if (mRegen < 0)
        {
            Image newWedge = Instantiate(regenNegative) as Image;
            newWedge.transform.SetParent(transform, false);
            newWedge.color = new Color(1, 1 + mRegen / 100, 0);
            newWedge.fillAmount = -mRegen / 200;
            newWedge.enabled = true;
        }
        else
        {
            Image newWedge = Instantiate(regenPositive) as Image;
            newWedge.transform.SetParent(transform, false);
            newWedge.color = new Color(1 - mRegen / 100, 1, 0);
            newWedge.fillAmount = mRegen / 200;
            newWedge.enabled = true;
        }
    }
}
