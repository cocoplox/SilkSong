using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPointFollow2 : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 4f;
    public int patrolDestination;
    private bool movingUp = true;

    private void Update()
    {
        if (movingUp)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, waypoints[0].position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, waypoints[0].position.y)) < .1f)
            {
                movingUp = false;
            }
        }
        else
        {
            Vector3 targetPosition = new Vector3(transform.position.x, waypoints[1].position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, waypoints[1].position.y)) < .1f)
            {
                movingUp = true;
            }
        }
    }
}
