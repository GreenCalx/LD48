using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatCollection : MonoBehaviour
{
    [System.Serializable]
    public class InvokableAndMenace
    {
        public GameObject    invokable;
        public int           menaceLevel;
    }

    [SerializeField]
    public List<InvokableAndMenace> invokables_and_menace_level;

    // TODO if needed.
    private List<InvokableAndMenace> cached_event_pool;

    public GameObject getRandomMenace( int iThreatLevel )
    {
        // Build randomization pool ( all event with a threat <= to iThreatLevel)
        List<GameObject> selected_events = new List<GameObject>();
        foreach( InvokableAndMenace invok in invokables_and_menace_level)
        {
            if ( invok.menaceLevel <= iThreatLevel )
            {
                selected_events.Add(invok.invokable);
            }
        }//!foreach

        // Return random elem form the list.
        int n_events = selected_events.Count;
        int rand_val = Random.Range( 0, n_events);
        if ( (rand_val >= 0) && (rand_val<selected_events.Count) )
            return selected_events[rand_val];
        else
        { Debug.LogError("Chosen threat event is out of range.");  return null; }
        
    } //! getRandomMenace



    void Start()
    {
    }
    void Update()
    {   
    }

}   
