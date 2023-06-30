using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject attackCol, patrolLimit01, patrolLimit02;
    public int vida;
    public float speed;

    public PlayerIdentifier playerIdentifier;

    private Animator _anim;
    private bool _isAttacking = false;
    [HideInInspector] public Rigidbody _rb;
    private GameObject _player;

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
        
        if (!_isAttacking && Vector3.Distance(transform.position, _player.transform.position) <= 3f)
        {
            _anim.SetTrigger("Attack");
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
        _isAttacking = false;
        attackCol.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    void Flip()
    {
        float direction = _player.transform.position.x - transform.position.x;
        if (_rb.velocity.x > 0 ||(direction > 0f && _isAttacking))
        {
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            attackCol.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            attackCol.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }
}
