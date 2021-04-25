using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegenBehavior : MonoBehaviour
{
    public Battery mBattery;
    public Image regenPositiveImg;
    public Image regenNegativeImg;

    // Start is called before the first frame update
    void Start()
    {
        MakeGraph();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MakeGraph();
    }

    void MakeGraph()
    {
        float mRegen = mBattery.mRegen - mBattery.mConsumption;

        if (mRegen < 0)
        {
            regenPositiveImg.enabled = false;
            regenNegativeImg.enabled = true;
            regenNegativeImg.color = new Color(1, 1 + mRegen / 100, 0);
            regenNegativeImg.fillAmount = -mRegen / 200;
        }
        else
        {
            regenNegativeImg.enabled = false;
            regenPositiveImg.enabled = true;
            regenPositiveImg.color = new Color(1 - mRegen / 100, 1, 0);
            regenPositiveImg.fillAmount = mRegen / 200;
        }
    }
}
