using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDisplay : MonoBehaviour
{
    public Texture BackgroundT;
    public RenderTexture BackgroundRT;
    public Material InfiniteBackgroundRenderer;
    public RenderTexture ResultRT;
    public Material BlitMat;
    public Gradient G;
    public ShipBehavior ship;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPreRender()
    {
        // clear caemra with background
        InfiniteBackgroundRenderer.SetVector("_UvShift", this.transform.position * 0.04f);


        InfiniteBackgroundRenderer.SetColor("_Color", G.Evaluate(ship.GetDeepness01()));
        Graphics.Blit(BackgroundT, (RenderTexture)null, InfiniteBackgroundRenderer);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
    }
}
