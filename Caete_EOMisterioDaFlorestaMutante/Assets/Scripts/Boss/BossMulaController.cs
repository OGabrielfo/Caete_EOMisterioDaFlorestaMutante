using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMulaController : MonoBehaviour
{
    public string bossName;
    public GameObject attackCol, runAttack, barraHP;
    public BossController bossController;
    public Transform[] sides;
    public float speed;
    public int vidaTotal;
    public float waitTime;
    [HideInInspector] public bool TakeDamage;

    private int _currentSide = 0;
    [HideInInspector]public int _counter, _counterLimit;
    private int _vidaAtual;

    private int _damageCounter;

    private Animator _anim;
    [HideInInspector]public Rigidbody _rb;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        _vidaAtual = vidaTotal;
        _counterLimit = (int)Mathf.Floor(vidaTotal / _vidaAtual);
        _counter = _counterLimit;
        bossController.bossName = bossName;
        transform.position = sides[0].position;
        bossController.life = _vidaAtual;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _counterLimit = (int)Mathf.Floor(vidaTotal / _vidaAtual);
        if (_counter < 1)
        {
            _anim.SetInteger("RandomAction", 0);
            _counter = _counterLimit;
        }

        Flip();
    }

    private void LateUpdate()
    {
        foreach (Transform waypoint in sides)
        {
            waypoint.position = new Vector3(waypoint.position.x, transform.position.y, transform.position.z);
        }

        _anim.SetInteger("Counter", _counter);
        _anim.SetInteger("Life", _vidaAtual);
        bossController.life = _vidaAtual;
    }

    public void RunAttack()
    {
        if (sides.Length == 0) return;

        Transform currentTarget = sides[_currentSide];
        Vector3 moveDirection = currentTarget.position - transform.position;
        moveDirection.Normalize();
        //var moveAmount = speed * Time.deltaTime;
        //var moveAmount = speed * moveDirection;

        if (Vector3.Distance(transform.position, currentTarget.position) <= 0.1f)
        {
            if (_currentSide == sides.Length - 1)
            {
                _currentSide = 0;
            }
            else
            {
                _currentSide++;
            }

            runAttack.SetActive(false);
            _counter--;
            _rb.velocity = Vector3.zero;
        }
        else
        {
            runAttack.SetActive(true);
            _rb.velocity = moveDirection * speed;
        }
    }

    public void ReceberDano()
    {
        if (!_anim.GetBool("Invulneravel"))
        {
            barraHP.GetComponent<Animator>().SetTrigger("Damage");
            _damageCounter++;
            if (_damageCounter > 2)
            {
                _damageCounter = 0;
                _vidaAtual--;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.SendMessage("ReceberDano");
        }
    }

    private void Flip()
    {
        if (Vector3.Distance(transform.position, sides[0].position) <= 0.1f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            attackCol.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else if (Vector3.Distance(transform.position, sides[1].position) <= 0.1f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            attackCol.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    public void AttackOn()
    {
        attackCol.SetActive(true);
    }

    public void AttackOff()
    {
        attackCol.SetActive(false);
    }

    public void PowerActivate()
    {

    }
}
