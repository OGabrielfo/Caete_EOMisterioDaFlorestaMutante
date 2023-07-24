using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Patroller : MonoBehaviour
{
    public Transform[] waypoints;
    [HideInInspector] public float speed;
    public float waitTime;

    private int currentWaypoint = 0;

    private Animator _anim;
    private Rigidbody _rb;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        speed = GetComponent<EnemyController>().speed;
    }

    void FixedUpdate()
    {
        foreach (Transform waypoint in waypoints)
        {
            waypoint.position = new Vector3(waypoint.position.x, transform.position.y, transform.position.z);
        }
    }
    public void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        Transform currentTarget = waypoints[currentWaypoint];
        Vector3 moveDirection = currentTarget.position - transform.position;
        moveDirection.Normalize();
        //var moveAmount = speed * Time.deltaTime;
        //var moveAmount = speed * moveDirection;

        if (Vector3.Distance(transform.position, currentTarget.position) <= 0.1f)
        {
            if (currentWaypoint == waypoints.Length - 1)
            {
                currentWaypoint = 0;
            }
            else
            {
                currentWaypoint++;
            }

            _anim.SetBool("Patroll", false);
            _rb.velocity = Vector3.zero;
        }
        else
        {
            _rb.velocity = moveDirection * speed;
        }

        //_rb.velocity = moveDirection * speed;

    }
}
