using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMulaController : MonoBehaviour
{
    public GameObject attackCol, runAttack;
    public Transform[] sides;
    public float speed;
    public int vidaTotal;
    public float waitTime;

    private int _currentSide = 0;
    [HideInInspector]public int _counter, _counterLimit;
    private int vidaAtual;

    private int _damageCounter;

    private Animator _anim;
    [HideInInspector]public Rigidbody _rb;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        vidaAtual = vidaTotal;
        _counterLimit = (int)Mathf.Floor(vidaTotal / vidaAtual);
        _counter = _counterLimit;
        transform.position = sides[0].position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _counterLimit = (int)Mathf.Floor(vidaTotal / vidaAtual);
        if (_counter < 1)
        {
            _anim.SetInteger("RandomAction", 0);
            _counter = _counterLimit;
        }
    }

    private void LateUpdate()
    {
        foreach (Transform waypoint in sides)
        {
            waypoint.position = new Vector3(waypoint.position.x, transform.position.y, transform.position.z);
        }

        _anim.SetInteger("Counter", _counter);
        _anim.SetInteger("Life", vidaAtual);
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

            _counter--;
            _rb.velocity = Vector3.zero;
        }
        else
        {
            _rb.velocity = moveDirection * speed;
        }
    }

    public void ReceberDano()
    {
        if (!_anim.GetBool("Invulneravel"))
        {
            _damageCounter++;
            if (_damageCounter > 2)
            {
                _damageCounter = 0;
                vidaAtual--;
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
}