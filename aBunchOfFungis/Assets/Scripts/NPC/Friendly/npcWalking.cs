using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcWalking : MonoBehaviour
{
    [SerializeField] private float speed;
    private float universalConstant = 0.0625f;

    private float waitTime;
    [SerializeField] private float startWaitTime;

    public Transform[] moveSpots;
    private int spot;

    private void Start()
    {
        waitTime = startWaitTime;

        spot = 0;
    }

    private void Update()
    {
        var actualSpeed = speed * universalConstant;
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[spot].position, actualSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, moveSpots[spot].position) <= 1f)
        {
            if(waitTime <= 0)
            {
                waitTime = startWaitTime;
                if(spot < moveSpots.Length - 1)
                {
                    spot++;
                }
                else
                {
                    spot = 0;
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
