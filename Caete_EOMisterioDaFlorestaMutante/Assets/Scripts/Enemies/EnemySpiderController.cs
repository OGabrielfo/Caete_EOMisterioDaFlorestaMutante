using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpiderController : MonoBehaviour
{
    public GameObject baseTeia, teia, explosionFX;

    public float minDistance, maxDistance, velocity;
    public int vidaTotal;

    private int vida;

    private Rigidbody _rb;
    private Animator _anim;
    private float distance;
    private bool _dead = false;

    private void Awake()
    {
        vida = vidaTotal;
    }
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        _rb.velocity = Vector3.up * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs(baseTeia.transform.position.y - transform.position.y);
        if (!_dead)
        {
            if (distance <= minDistance)
            {
                //_rb.velocity = Vector3.down * velocity;
                ChangeMovement("down");
            }
            else if (distance >= maxDistance)
            {
                //_rb.velocity = Vector3.up * velocity;
                ChangeMovement("up");
            }
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
        

        teia.transform.localScale = new Vector3(1f, distance * 0.8f, 1f);
    }

    private void LateUpdate()
    {
        
    }

    private void ChangeMovement(string side)
    {
        if (side == "down" && _rb.velocity.y != velocity * -1)
        {
            _rb.velocity -= new Vector3(0f, 0.1f, 0f);
        }
        else if (side == "up" && _rb.velocity.y != velocity)
        {
            _rb.velocity += new Vector3(0f, 0.1f, 0f);
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
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        teia.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        _dead = true;
        yield return new WaitForSeconds(3f);
        vida = vidaTotal;
        explosionFX.SetActive(false);
        _dead = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        teia.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        _rb.velocity = Vector3.up * velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SendMessage("ReceberDano");
        }
    }

    public void DeathExplosion()
    {
        explosionFX.SetActive(true);
    }
}
