using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeepnessWidget : MonoBehaviour
{
    public TextMeshProUGUI ValueText;
    public ShipBehavior Ship;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float GrdDist = Ship.GetDeepness();
        ValueText.text = ((int)(GrdDist)).ToString();
    }
}
