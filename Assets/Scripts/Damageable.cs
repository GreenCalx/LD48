using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int mHealth;
    [SerializeField]
    private int mCurrentHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ExecuteOnCollide(collision);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HitMe(int Damage)
    {
        mCurrentHealth -= Damage;
        if (mCurrentHealth <= 0) Die();

    }

    void Die()
    {
        Debug.Log("IM DEAD PLEASE STOP");

        var Ship = gameObject.GetComponent<ShipBehavior>();
        if (Ship) Ship.YOUDEAD();

        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ExecuteOnCollide(Collision2D collision)
    {
        var CollisionDamager = collision.gameObject.GetComponent<Damager>();
        if (CollisionDamager)
        {
            HitMe(CollisionDamager.GetDamage());
        }
    }
}
