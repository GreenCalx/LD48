using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinteBackground : MonoBehaviour
{
    public Texture2D Background;
    public RenderTexture Result;
    public Material BlitMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Graphics.Blit(Background, Result, BlitMaterial); 
    }
}
