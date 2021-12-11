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

    private int xSave;
    private int ySave;

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
        int dirX = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        int dirY = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

        var attacking = Input.GetAxisRaw("Fire1");

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

        if (dirX != 0 || dirY != 0)
        {
            xSave = dirX;
            xSave += 1;
            ySave = dirY;
            ySave += 1;
        }

        if(Time.time >= nextAttackTime)
        {
            if (attacking != 0)
            {
                if (!attacked)
                {
                    Attack(xSave, ySave);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            else
            {
                attacked = false;
            }
        }
    }
    void Attack(int dirX, int dirY)
    {
        //Debug.Log("Direction X = " + dirX + ", Direction Y = " + dirY);
        //play an attack animation
        animator.SetTrigger("Attacking");

        Debug.Log(dirX + ", " + dirY);

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
