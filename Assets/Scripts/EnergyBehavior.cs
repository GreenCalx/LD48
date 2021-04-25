using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBehavior : MonoBehaviour
{
    public Battery mBattery;
    public Image energy;

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
        float mEnergy = mBattery.mEnergy;

        Image newWedge = Instantiate(energy) as Image;
        newWedge.transform.SetParent(transform, false);
        if (mEnergy < 50)
        {
            newWedge.color = new Color(1, mEnergy / 50, 0);
        }
        else
        {
            newWedge.color = new Color((50 - mEnergy) / 50 + 1, 1, 0);
        }
        newWedge.transform.localScale = new Vector2(0.8f * mEnergy / 100, 0.8f * mEnergy / 100);
        newWedge.enabled = true;
    }
}
