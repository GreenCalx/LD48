using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsSounds : MonoBehaviour
{
    public AudioClip[] explosionsClips;
    private int count = 0;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = explosionsClips[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        audio.Play();

        count++;
        if (count == explosionsClips.Length)
            count = 0;

        audio.clip = explosionsClips[count];
    }
}
