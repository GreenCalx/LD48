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

    public List<GameObject> mObjects;
    [SerializeField]
    public Material Mat;

    public Canvas BlitTexture;
    public Texture2D mTopLayout;
    public Material RotatedBlit;
    public float Speed;
    public int DotSize;
    public Texture2D mEnnemieSprite;
    public Texture2D mEnnemiesOOBSprite;
    // Start is called before the first frame update
    void Start()
    {
        mObjects = new List<GameObject>(0);
    }

    int GetFlatIndex(int x, int y)
    {
        return Mathf.Clamp(mImage.width * Mathf.Clamp(y, 0, mImage.width-1) + Mathf.Clamp(x, 0,mImage.height-1), 0, mImage.width * mImage.height);;
    }

    int GetFlatIndex(Vector2 Coordinates)
    {
        return Mathf.Clamp(GetFlatIndex((int)Coordinates.x,(int)Coordinates.y), 0, mImage.width * mImage.height);
    }

    void PrintDot(int x, int y, int GaussianDistanceRadiusInPixel, Color32 Color)
    {
        float x_ClipSpace = Mathf.Clamp((float)x / mImage.width, 0.1f, 0.9f);
        float y_ClipSpace = Mathf.Clamp((float)y / mImage.height, 0.1f, 0.9f);

        bool IsArrow = (x_ClipSpace != (float)x / mImage.width || y_ClipSpace != (float)y / mImage.height);

        float size_x = (float)GaussianDistanceRadiusInPixel / mImage.width;
        float size_y = (float)GaussianDistanceRadiusInPixel / mImage.height;

        if (!IsArrow)
            Mat.SetTexture("_MainTex", mEnnemieSprite);
        else
            RotatedBlit.SetTexture("_MainTex", mEnnemiesOOBSprite);

        GL.PushMatrix();
        GL.LoadOrtho();
        // activate the first shader pass (in this case we know it is the only pass)
        if(!IsArrow)
        {
            Mat.SetPass(0);
        }
        else
        {

            var rotationAngle = Vector3.SignedAngle(new Vector3(x_ClipSpace, y_ClipSpace, 0) - new Vector3(0.5f, 0.5f, 0), new Vector3(0,1,0), new Vector3(0,0,1));
            RotatedBlit.SetMatrix("_RotationMatrix", Matrix4x4.TRS(new Vector3(0.5f,0.5f,0), Quaternion.Euler(0, 0, rotationAngle - 180), Vector3.one));
            RotatedBlit.SetPass(0);
        }
        // draw a quad over whole screen
        GL.Begin(GL.QUADS);
        GL.TexCoord2(0f, 0f); GL.Vertex3(x_ClipSpace - size_x, y_ClipSpace - size_y, 0f);    
        GL.TexCoord2(0f, 1f); GL.Vertex3(x_ClipSpace - size_x, y_ClipSpace + size_y, 0f);
        GL.TexCoord2(1f, 1f); GL.Vertex3(x_ClipSpace + size_x, y_ClipSpace + size_y, 0f);
        GL.TexCoord2(1f, 0f); GL.Vertex3(x_ClipSpace + size_x, y_ClipSpace - size_y, 0f);
        GL.End();
        GL.PopMatrix();
    }

    void PrintLayout()
    {
        Graphics.Blit(mLayout, RTFinal, Mat);
    }

    public override void DisplayElem(bool T)
    {
        if (BlitTexture) BlitTexture.enabled = T;
    }

    void ResolveRadar()
    {
        // Print global layout
        PrintLayout();
        // Print center point as self
        var CurrentPositionCenteredInPixel = new Vector2(mImage.width / 2, mImage.height / 2);
        PrintDot((int)CurrentPositionCenteredInPixel.x, (int)CurrentPositionCenteredInPixel.y, DotSize, new Color32(255, 0, 0, 255));
        // Print Ennemies point
        var PixelRatioX = mMaxRadius * 2 / mImage.width;
        var PixelRatioY = mMaxRadius * 2 / mImage.height;
        mObjects.RemoveAll(item => item == null);
        foreach (GameObject O in mObjects)
        {
            var DirectionInUnits = O.transform.position - transform.position;
            var PointCenterInPixel = new Vector2((CurrentPositionCenteredInPixel.x + (DirectionInUnits.x / PixelRatioX)),
                (CurrentPositionCenteredInPixel.y + (DirectionInUnits.y / PixelRatioY)));
            PrintDot( (int)PointCenterInPixel.x, (int)PointCenterInPixel.y, DotSize, new Color32(255, 0, 0, 255));
        }
        // Print Sonar scan
        RotatedBlit.SetMatrix("_RotationMatrix", Matrix4x4.TRS(new Vector3(0.5f,0.5f,0), Quaternion.Euler(0, 0, Time.time * -Speed), Vector3.one));
        RotatedBlit.SetMatrix("_MoveCenter", Matrix4x4.Translate(new Vector3(-0.5f, -0.5f, 0)));
        Graphics.Blit(mTopLayout, RTFinal, RotatedBlit);
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

    public void removeFromScan( GameObject iObj )
    {
        if (mObjects.Contains(iObj))
            mObjects.Remove(iObj);
        //mObjects.RemoveAll(item => item == null);
    }
}
