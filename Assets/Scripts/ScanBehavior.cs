using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanBehavior : ShipElem
{
    public Timer mAnimationTimer;
    public float mMaxRadius;

    public Texture2D mImage;
    public Texture2D mLayout;

    public RenderTexture RTFinal;

    public GameObject[] mObjects;
    [SerializeField]
    public Material Mat;
    // Start is called before the first frame update
    void Start()
    {
        mConsumption = 1;
    }

    int GetFlatIndex(int x, int y)
    {
        return (mImage.width * y + x);
    }

    int GetFlatIndex(Vector2 Coordinates)
    {
        return Mathf.Clamp(GetFlatIndex((int)Coordinates.x,(int)Coordinates.y), 0, mImage.width * mImage.height);
    }

    void PrintDot(Color32[] Pixels, int x, int y, int GaussianDistanceRadiusInPixel, Color32 Color)
    {
        Pixels[GetFlatIndex(x, y)] = Color;
        for(int i = x - GaussianDistanceRadiusInPixel; i < x+ GaussianDistanceRadiusInPixel; ++i)
        {
            for(int j = y - GaussianDistanceRadiusInPixel; j < y + GaussianDistanceRadiusInPixel; ++j)
            {
                if(Vector2.Distance(new Vector2(x,y), new Vector2(i,j)) <= GaussianDistanceRadiusInPixel)
                {

                    Pixels[GetFlatIndex(i, j)] = Color;

                }
            }
        }
    }

    void PrintLayout(Color32[] Pixels)
    {

        //var PixelRatioX = mMaxRadius * 2 / mImage.width;
        //var PixelRatioY = mMaxRadius * 2 / mImage.height;

        //for (int i = 0; i < mImage.width; ++i)
        //{
        //    for (int j = 0; j < mImage.height; ++j)
        //    {
        //        //var DistanceToCenter = Vector3.Distance(new Vector3(PixelRatioX * i, PixelRatioY * j), transform.position);
        //        //if (Mathf.Round(DistanceToCenter) % 2 == 0)
        //        //    Pixels[GetFlatIndex(i, j)] = new Color32(255, 255, 255, 255);
        //        //else
        //            Pixels[GetFlatIndex(i, j)] = new Color32(0, 0, 0, 255);
        //    }
        //}
    }

    void ResolveRadar()
    {
        // NOTE mtn5 : Seems to be a perf bottleneck

        Graphics.Blit(mLayout, RTFinal, Mat);
        var Pixels = mImage.GetPixels32();
        for (int i = 0; i < Pixels.Length; ++i) Pixels[i] = new Color32(0, 0, 0, 0);
        var CurrentPositionCenteredInPixel = new Vector2(mImage.width / 2, mImage.height / 2);
        PrintDot(Pixels, (int)CurrentPositionCenteredInPixel.x, (int)CurrentPositionCenteredInPixel.y, 3, new Color32(255, 0, 0, 255));
        var PixelRatioX = mMaxRadius * 2 / mImage.width;
        var PixelRatioY = mMaxRadius * 2 / mImage.height;

        foreach (GameObject O in mObjects)
        {
            var DirectionInUnits = O.transform.position - transform.position;
            var PointCenterInPixel = new Vector2((CurrentPositionCenteredInPixel.x + (DirectionInUnits.x / PixelRatioX)),
                (CurrentPositionCenteredInPixel.y + (DirectionInUnits.y / PixelRatioY)));
            PrintDot(Pixels, (int)PointCenterInPixel.x, (int)PointCenterInPixel.y, 3, new Color32(255, 0, 0, 255));
        }
        mImage.SetPixels32(Pixels);
        mImage.Apply(false);
        Graphics.Blit(mImage, RTFinal, Mat);

    }

    private void FixedUpdate()
    {
        mAnimationTimer.Update(Time.deltaTime);
        if (mActivated)
        {
            ResolveRadar();
            DisplayElem(true);
        }
        else
        {
            DisplayElem(false);
        }

    }
}
