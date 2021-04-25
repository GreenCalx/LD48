using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(RectTransform))]
public class EventStrip : MonoBehaviour
{
    public const int MIN_MENACE = 1;
    public const int MAX_MENACE = 10;

    [Range(0, 10)]
    public int threat_level;

    [Range(MIN_MENACE, MAX_MENACE)]
    public int menace_density;

    public GameObject refEventCollection;
    public GameObject refShip;

    [HideInInspector]
    public float event_cooldown;
    private float cooldown_before_next_event;
    private bool strip_is_active;
    
    private BoxCollider2D bc2d;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        strip_is_active = false;
        cooldown_before_next_event = 0f;
        event_cooldown = (1 + ( 1 / menace_density )) * (MAX_MENACE+1 - menace_density);

        // if unset we try to retrieve collec from scene.
        if (refEventCollection == null)
            refEventCollection = GameObject.Find("EventCollection");

        if (refShip == null)
            refShip = GameObject.Find("Ship");
        
        bc2d = GetComponent<BoxCollider2D>();
        rectTransform = GetComponent<RectTransform>();

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
        if (refEventCollection!=null)
        {
            ThreatCollection tc = refEventCollection.GetComponent<ThreatCollection>();
            if (tc==null)
            {
                Debug.LogWarning("EventCollection not holding threatcollection.");
                return;
            }
            GameObject menace = tc.getRandomMenace(threat_level);
            if (menace!=null)
                spawnMenace(menace);
            else
                Debug.LogWarning("Menace from collection is null. nothing will happen.");

        } else {
            Debug.LogWarning("EventCollection not found for strip");
        }
    }

    private void spawnMenace( GameObject iPrefab )
    {

        // Set position somewhere in the strip
        Vector3 position = new Vector3( Random.Range(rectTransform.rect.xMin, rectTransform.rect.xMax),
                                        Random.Range(rectTransform.rect.yMin, rectTransform.rect.yMax),
                                        0) + rectTransform.transform.position;
        
        GameObject new_menace = Instantiate(iPrefab, position, Quaternion.identity);

        // Set direction towards player ? Init Menace behaviour accordingly?

    }

    private void OnTriggerStay2D( Collider2D iCol )
    {
        PlayerController pc = iCol.GetComponent<PlayerController>();
        if (!!pc && !strip_is_active)
        {
            strip_is_active = true;
            Debug.Log("Entering event strip " + gameObject.name );
        }
    }

    private void OnTriggerExit2D( Collider2D iCol )
    {
        PlayerController pc = iCol.GetComponent<PlayerController>();
        if (!!pc && strip_is_active)
        {
            strip_is_active = false;
            Debug.Log("Exiting strip " + gameObject.name );
        }
    }

}
