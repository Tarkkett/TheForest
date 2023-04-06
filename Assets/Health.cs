using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public bool autoDestroy = true;
    public GameObject dieEffect;
    public GameObject damageEffect;
    public UnityEvent onDie;
    public UnityEvent onDamage;

    private void Start()
    {
        if (hp <= 0)
        {
            hp = maxHp;
        }

    }
    public void Damage(int damage)
    {
        hp -= damage;
        onDamage.Invoke();
        if (damageEffect)
        {
            Instantiate(damageEffect, transform.position, transform.rotation);
        }
        if (hp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        onDie.Invoke();
        if (autoDestroy)
        {
            Destroy(gameObject);
        }
        if (dieEffect)
        {
            Instantiate(dieEffect, transform.position, transform.rotation);
        }
    }
}
