using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Animator animator;

    private Vector2[,] attackPoints = new Vector2[3,3];
    public Transform[] attackPointsSerialize;
    private int x = 0;
    private bool attacked;
    private PlayerScript playerScipt;

    public float attackRange = 1f;
    private int attackDamage = 40;

    public float attackRate = 2f;
    public float nextAttackTime;
    private float attackTimer;
    private bool attackCounter = false;

    public LayerMask enemyLayers;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                attackPoints[i,j] = new Vector2(attackPointsSerialize[x].position.x, attackPointsSerialize[x].position.y);
                Debug.Log(attackPoints[i,j].x + attackPoints[i,j].y);
                x++;
                if (x > 8)
                {
                    x = 0;
                }
            }
        }
    }

    void FixedUpdate()
    {
        int dirX = Mathf.RoundToInt(Input.GetAxisRaw("AttackAxisY"));
        int dirY = Mathf.RoundToInt(Input.GetAxisRaw("AttackAxisX"));

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                attackPoints[i, j] = new Vector2(attackPointsSerialize[x].position.x, attackPointsSerialize[x].position.y);
                x++;
                if(x > 8)
                {
                    x = 0;
                }
            }
        }

        dirX += 1;
        dirY += 1;

        if(Time.time >= nextAttackTime)
        {
            if (dirX != 1 || dirY != 1)
            {
                if (!attacked)
                {
                    Attack(dirX, dirY, attackCounter);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            else
            {
                attacked = false;
            }
        }
        Debug.Log(Time.time);
        Debug.Log("Time is Set: " + attackTimer);
        Debug.Log(attackCounter);

    }
    void Attack(int dirX, int dirY, bool manyAttacks)
    {
        //play an attack animation
        if (attackCounter == false)
        {
            animator.SetTrigger("Attacking");
            attackCounter = true;
            attackTimer = Time.time + 0.75f;
            Debug.Log("AttackingOneRan");
        }
        else if(attackTimer > Time.time)
        {
            animator.SetTrigger("Attacking2");
            attackCounter = false;
            Debug.Log("AttackingTwoRan");
        }
        else
        {
            animator.SetTrigger("Attacking");
            attackCounter = true;
            attackTimer = Time.time + 0.75f;
            Debug.Log("AttackingOneRan");
        }

        //detect all enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoints[dirX,dirY], attackRange, enemyLayers);

        //apply damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        attacked = true;
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < 9; i++)
        {
            Gizmos.DrawWireSphere(attackPointsSerialize[i].position, attackRange);
        }
    }
}
