using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBehavior : MonoBehaviour
{
    public Battery mBattery;
    public Image energyImg;

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
        float mEnergy = mBattery.mEnergy;

        if (mEnergy < 50)
        {
            energyImg.color = new Color(1, mEnergy / 50, 0);
        }
        else
        {
            energyImg.color = new Color((50 - mEnergy) / 50 + 1, 1, 0);
        }
        energyImg.transform.localScale = new Vector2(0.8f * mEnergy / 100, 0.8f * mEnergy / 100);
    }
}
