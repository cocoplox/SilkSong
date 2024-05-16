using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMover1 : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 4f;
    public int patrolDestination;
    private bool playerTouched = false;

    private void Update()
    {
        if (playerTouched)
        {
            if (patrolDestination == 0)
            {
                Vector3 targetPosition = new Vector3(waypoints[0].position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(waypoints[0].position.x, transform.position.y)) < .1f)
                {
                    //transform.localScale = new Vector3(-1, 1, 1);
                    patrolDestination = 1;
                }
            }
            else if (patrolDestination == 1)
            {
                Vector3 targetPosition = new Vector3(waypoints[1].position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(waypoints[1].position.x, transform.position.y)) < .1f)
                {
                    //transform.localScale = new Vector3(1, 1, 1);
                    patrolDestination = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTouched = true;
        }
    }
}
