using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    public float mEnergy = 100;
    public float mRegen = 100;
    public Image mWedge;

    // Start is called before the first frame update
    void Start()
    {
        MakeGraph();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);
        MakeGraph();
    }

    void MakeGraph()
    {
        Image newWedge = Instantiate(mWedge) as Image;
        newWedge.transform.SetParent(transform, false);
        newWedge.color = new Color(1 - mEnergy / 100, mEnergy / 100, 0);
        newWedge.fillAmount = mEnergy / 100;
        newWedge.enabled = true;
    }
}
