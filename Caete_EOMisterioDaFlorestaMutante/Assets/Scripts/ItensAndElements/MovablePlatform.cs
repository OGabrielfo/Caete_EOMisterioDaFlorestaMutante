using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;

    private int currentWaypoint = 0;

    private Animator _anim;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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

            //_anim.SetBool("Patroll", false);
            //_rb.velocity = Vector3.zero;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
            //_rb.velocity = moveDirection * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            playerRb.velocity = new Vector3(_rb.velocity.x, 0f, 0f);
            //playerRb.useGravity = false;

            collision.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
        collision.transform.parent = null;
        //playerRb.useGravity = true;
    }
}
