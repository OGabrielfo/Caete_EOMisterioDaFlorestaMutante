using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject attackCol, patrolLimit01, patrolLimit02;
    public int vida;
    public float speed;
    public float attackDistance;

    public PlayerIdentifier playerIdentifier;

    private Animator _anim;
    [HideInInspector] public bool _isAttacking = false;
    [HideInInspector] public Rigidbody _rb;
    private GameObject _player;
    private bool _invulneravel = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        if (!_isAttacking && Vector3.Distance(transform.position, _player.transform.position) <= attackDistance)
        {
            _anim.SetBool("Attack", true);
        }
        else
        {
            _anim.SetBool("Attack", false);
        }
    }

    private void LateUpdate()
    {
        _anim.SetBool("PlayerChase", playerIdentifier.playerInArea);
        _anim.SetInteger("Life", vida);
        _anim.SetFloat("Speed", speed);

    }

    private void Attack()
    {
        _isAttacking = true;
        attackCol.SetActive(true);
    }

    private void AttackOff()
    {
        attackCol.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.SendMessage("ReceberDano");
        }
    }

    void Flip()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > attackDistance)
        {
            if (_rb.velocity.x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                attackCol.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                attackCol.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            }
        }
        else
        {
            float direction = _player.transform.position.x - transform.position.x;
            if (direction > 0f)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                attackCol.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                attackCol.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            }
        }
    }

    public void ReceberDano()
    {
        if (vida > 0 && !_invulneravel)
        {
            vida--;
            _anim.SetTrigger("TakeDamage");
        }
    }

    public void Invulneravel()
    {
        if (_invulneravel)
        {
            _invulneravel = false;
        }
        else
        {
            _invulneravel = true;
        }
    }
}
