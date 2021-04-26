using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipBehavior : MonoBehaviour
{
    //InputManager InputManager;

    public Battery mBattery;
    public PowerUIrotation mPowerUIrotation;
    public GameObject mEndMarker;
    public GameObject mPlayer;

    public ThrusterBehavior[] mThrusters;
    public ShieldBehavior[] mShields;
    public TurretBehavior[] mTurrets;
    public ScanBehavior mScan;
    public LaserBehavior mLaser;
    public RepairBehavior mRepair;

    private float mStartDeepness;

    // World ref for gravity and ennmies?

    // Start is called before the first frame update
    void Start()
    {
        mStartDeepness = GetDeepness();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var damageable = GetComponent<Damageable>();
        if (mRepair.mActivated)
        {
            damageable.Regenerate(mRepair.mLifePerFrame);
        }

        var transform = GetComponent<Transform>();
        float angle = transform.rotation.eulerAngles.z;
        mPowerUIrotation.SetRotation(angle);

        mBattery.Set_regen(100 * GetDeepness() / mStartDeepness);
    }

    public void YOUDEAD()
    {
        SceneManager.LoadScene("DeathScreen");
    }

    public float GetDeepness()
    {
        return mPlayer.transform.position.y - mEndMarker.transform.position.y;
    }
}
