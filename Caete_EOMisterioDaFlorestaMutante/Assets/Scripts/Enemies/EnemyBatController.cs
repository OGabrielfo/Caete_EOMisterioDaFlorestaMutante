using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatController : MonoBehaviour
{
    public GameObject /*attackCol,*/ ceilingPoint;
    public int vidaTotal;
    public float speed;

    public PlayerIdentifier playerIdentifier;

    private Animator _anim;
    [HideInInspector] public Rigidbody _rb;
    private GameObject _player;
    private bool _dead = false;
    private int vida;
    private bool _inCeilingPoint;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        vida = vidaTotal;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    private void LateUpdate()
    {
        _anim.SetBool("PlayerChase", playerIdentifier.playerInArea);
        _anim.SetBool("CeilingPoint", _inCeilingPoint);
        _anim.SetInteger("Life", vida);
        _anim.SetFloat("Speed", speed);
        _anim.SetBool("IsDead", _dead);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.SendMessage("ReceberDano");
        }
    }*/

    void Flip()
    {
        if (_rb.velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            //attackCol.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            //attackCol.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    public void ReceberDano()
    {
        if (vida > 0)
        {
            vida--;
            _anim.SetTrigger("TakeDamage");
        }
    }

    public void EnemyDead()
    {
        _rb.useGravity = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        transform.position = ceilingPoint.transform.position;
        _dead = true;
        yield return new WaitForSeconds(120f);
        vida = vidaTotal;
        _dead = false;
        _rb.useGravity = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void MoveToCeiling()
    {
        if (!_inCeilingPoint)
        {
            Vector3 moveDirection = ceilingPoint.transform.position - transform.position;
            moveDirection.Normalize();
            //var moveAmount = speed * Time.deltaTime;
            //var moveAmount = speed * moveDirection;
            _rb.velocity = moveDirection * speed;
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ceilingPoint)
        {
            _inCeilingPoint = true;
            _rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ceilingPoint)
        {
            _inCeilingPoint = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SendMessage("ReceberDano");
        }
    }
}
