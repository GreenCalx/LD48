using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float mHealth;
    [SerializeField]
    private float mCurrentHealth;
    public bool is_friendly = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ExecuteOnCollide(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ExecuteOnCollide(collision);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HitMe(float Damage)
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

    public void Regenerate(float life)
    {
        mCurrentHealth += life;
        if (mCurrentHealth > mHealth)
        {
            mCurrentHealth = mHealth;
        }
    }

     public float GetCurrentHealth01()
    {
        return mCurrentHealth / mHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ExecuteOnCollide(Collision2D collision)
    {
        var CollisionDamager = collision.gameObject.GetComponent<Damager>();

        if ( CollisionDamager == null )
        {
            return;
        }

        // friendly vs friendly or enemy vs enemy
        if ( CollisionDamager.is_friendly == is_friendly)
            return;

        if (CollisionDamager)
        {
            HitMe(CollisionDamager.GetDamage());
        }
    }

}
