using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public float speed;
    private Transform playerBody;
    private Rigidbody2D playerRigid;
    public float lineOfSite;
    private float universalConstant = 0.0625f;

    private bool currentState;

    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player").transform;
        playerRigid = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Rigidbody2D>();
    }

   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentState = playerRigid.isKinematic;
            if (playerRigid.isKinematic)
            {
                playerRigid.isKinematic = false;
            }
            Vector2 difference = playerRigid.transform.position - transform.position;
            difference = difference.normalized * 20;
            playerRigid.AddForce(difference, ForceMode2D.Impulse);

            playerRigid.isKinematic = currentState;
        }
    }*/

    void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(transform.position, playerBody.position);

        if (distanceFromPlayer < lineOfSite)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerBody.position, speed * Time.deltaTime * universalConstant);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

    /*
    public float speed;
    private Transform playerBody;
    private bool facingRight = true;
    public float lineOfSite;
    public float shootingRange;
    public float rateOfFire = 1f;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletPoint;

    public float rotVal;

    public Transform staffAxis;
    public Transform staff;
    public bool staffCheck;
    public bool inverted;

    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("PlayerBody").transform;

        rotVal = staffAxis.rotation.z;
    }

    void FixedUpdate()
    {
        float distanceFromPlayer = Vector2.Distance(transform.position, playerBody.position);

        Vector3 difference = staffAxis.position - playerBody.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (distanceFromPlayer < lineOfSite)
        {
            if ((transform.position.x < playerBody.position.x && facingRight) || (transform.position.x > playerBody.position.x && !facingRight))
            {
                facingRight = !facingRight;
                transform.Rotate(new Vector3(0, 180, 0));
                Vector3 Scale = staff.localScale;
                Scale.y *= -1;
                staff.localScale = Scale;
            }

            if (distanceFromPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, playerBody.position, speed * Time.deltaTime);
            }
            else if (nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletPoint.transform.position, bulletPoint.transform.rotation);
                nextFireTime = Time.time + rateOfFire;
            }

            staffAxis.rotation = Quaternion.Euler(0f, 0f, rotZ);
            inverted = false;
        }
        else if (staffCheck && !inverted)
        {
            staffAxis.rotation = (Quaternion.Euler(0f, 0f, rotVal));
            if (!facingRight)
            {
                rotZ = 180;
                staffAxis.rotation = Quaternion.Euler(0f, 0f, rotZ);
            }
            inverted = true;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
    */
}
