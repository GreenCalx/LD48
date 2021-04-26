using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float mDamage;
    public float mDamageTickRate;
    private float time_since_last_damage = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public float GetDamage()
    {
        if ( time_since_last_damage < mDamageTickRate )
        {
            time_since_last_damage += Time.deltaTime;
            return 0f;
        } else {
            time_since_last_damage = 0f;
            return mDamage;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
