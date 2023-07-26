using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyController : MonoBehaviour
{
    public GameObject attackCol, patrolLimit01, patrolLimit02, explosionFX;
    public int vidaTotal;
    public float speed;
    public float attackDistance;
    public AudioClip dano, morte, javaliAttack;
    public AudioSource audioControl;

    public PlayerIdentifier playerIdentifier;

    private Animator _anim;
    [HideInInspector] public bool _isAttacking = false;
    [HideInInspector] public Rigidbody _rb;
    private GameObject _player;
    private bool _invulneravel = false;
    private bool _dead = false;
    private int vida;

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
        if (!_isAttacking && Vector3.Distance(transform.position, _player.transform.position) <= attackDistance && !_dead)
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
        _anim.SetBool("IsDead", _dead);
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
        if (Vector3.Distance(transform.position, _player.transform.position) > attackDistance && !_invulneravel)
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
            audioControl.clip = dano;
            audioControl.Play();
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

    public void EnemyDead()
    {
        _rb.useGravity = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        _dead = true;
        yield return new WaitForSeconds(120f);
        vida = vidaTotal;
        explosionFX.SetActive(false);
        _dead = false;
        _rb.useGravity = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void DeathExplosion()
    {
        if (audioControl.clip != morte)
        {
            audioControl.clip = morte;
            audioControl.Play();
        }
        explosionFX.SetActive(true);
    }

    public void JavaliAttack()
    {
        audioControl.clip = javaliAttack;
        audioControl.Play();
    }
}
