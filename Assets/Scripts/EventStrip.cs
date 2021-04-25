using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStrip : MonoBehaviour
{
    public const int MIN_MENACE = 1;
    public const int MAX_MENACE = 10;

    [Range(0, 10)]
    public int threat_level;

    [Range(MIN_MENACE, MAX_MENACE)]
    public int menace_density;

    [HideInInspector]
    public float event_cooldown;
    private float cooldown_before_next_event;
    private bool strip_is_active;
    

    // Start is called before the first frame update
    void Start()
    {
        strip_is_active = false;
        cooldown_before_next_event = 0f;
        event_cooldown = (1 + ( 1 / menace_density )) * (MAX_MENACE+1 - menace_density);
    }

    // Update is called once per frame
    void Update()
    {
        if (!strip_is_active)
            return;

        if ( cooldown_before_next_event < event_cooldown )
        {
            cooldown_before_next_event += Time.deltaTime;
            return;
        }
        cooldown_before_next_event = 0f;
        callEvent();
    }

    private void callEvent()
    {
        Debug.Log("Call Event !");
    }

    private void OnTriggerStay2D( Collider2D iCol )
    {
        PlayerController pc = iCol.GetComponent<PlayerController>();
        if (!!pc)
        {
            strip_is_active = true;
        }
    }

    private void OnTriggerExit2D( Collider2D iCol )
    {
        PlayerController pc = iCol.GetComponent<PlayerController>();
        if (!!pc)
        {
            strip_is_active = false;
        }
    }

}