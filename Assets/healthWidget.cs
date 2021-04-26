using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthWidget : MonoBehaviour
{
    public Image Value;
    public Damageable Ship;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gradient g = new Gradient();
        GradientColorKey[] Keys = new GradientColorKey[2];
        Keys[0].color = Color.green;
        Keys[0].time  = 1;
        Keys[1].color = Color.red;
        Keys[1].time  = 0;
        GradientAlphaKey[] AlphaKeys = new GradientAlphaKey[2];
        AlphaKeys[0].alpha = 1;
        AlphaKeys[0].time = 1;
        AlphaKeys[1].alpha = 1;
        AlphaKeys[1].time = 0;
        g.mode = GradientMode.Blend;
        g.SetKeys(Keys, AlphaKeys);
        Value.color = g.Evaluate(Ship.GetCurrentHealth01());
    }
}
