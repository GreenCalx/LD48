using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipElem : MonoBehaviour
{
    public bool mActivated = false;
    public float mConsumption;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void DisplayElem(bool signal)
    {
        var Renderers = GetComponentsInChildren<SpriteRenderer>();
        if (Renderers.Length > 1)
            Renderers[1].enabled = signal;
        else
            Renderers[0].enabled = signal;

        var Audio = GetComponentInChildren<AudioSource>();
        if (signal)
        {
            if (!Audio.isPlaying)
                Audio.Play();
        }
        else
            Audio.Stop();
    }
}
