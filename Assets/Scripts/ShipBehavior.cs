using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipBehavior : MonoBehaviour
{
    //InputManager InputManager;
    //Weapons[]    MyWeapons;
    //Shields[]    MyShields;

    public Battery mBattery;
    public ThrusterBehavior[] Thrusters;
    public ShieldBehavior[] Shields;


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
