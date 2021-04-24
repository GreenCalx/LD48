using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipBehavior : MonoBehaviour
{
    //InputManager InputManager;

    public Battery mBattery;
    public ThrusterBehavior[] mThrusters;
    public ShieldBehavior[] mShields;
    public TurretBehavior[] mTurrets;
    public ScanBehavior mScan;
    public LaserBehavior mLaser;
    public RepairBehavior mRepair;

    public float mHaulIntegrity = 100;

    // World ref for gravity and ennmies?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void YOUDEAD()
    {
        SceneManager.LoadScene("DeathScreen");
    }
}
