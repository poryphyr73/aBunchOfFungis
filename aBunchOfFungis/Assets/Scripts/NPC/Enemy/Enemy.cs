using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    private Animator animator;
    [SerializeField] private float decomposeTime;
    public float damage = 10;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("TookDamage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died!");

        animator.SetTrigger("Died");
        gameObject.layer = 7;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, decomposeTime);
        GetComponent<MeleeEnemy>().enabled = false;
    }
}
