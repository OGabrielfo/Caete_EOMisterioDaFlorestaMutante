using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyFrogFire : MonoBehaviour
{
    public GameObject frogFireExplosion;

    private float fallVelocity;
    private float angulo;
    private bool isFliped;
    private Rigidbody _rb;
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float newVelocity = _rb.velocity.y;
        newVelocity -= fallVelocity;
        _rb.velocity = new Vector3(_rb.velocity.x, newVelocity, _rb.velocity.z);

        if (_rb.velocity.x != 0f)
        {
            SetRotation();
        }
        
    }

    private void LateUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            _anim.SetTrigger("Collision");
            frogFireExplosion.SetActive(true);
            _rb.velocity = Vector3.zero;
            if (collision.gameObject.CompareTag("Ground"))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (collision.gameObject.CompareTag("Wall"))
            {
                if (isFliped)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                }
            }

                _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        else if (collision.gameObject.CompareTag("Player") && !frogFireExplosion.activeSelf)
        {
            frogFireExplosion.SetActive(true);
            _anim.SetTrigger("Collision");
            _rb.velocity = Vector3.zero;

            if (isFliped)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            }

            _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            collision.gameObject.GetComponent<PlayerController>().SendMessage("ReceberDano");
        }
    }

    public void FireExplosionOff()
    {
        frogFireExplosion.SetActive(false);
        Destroy(gameObject);
        /*
        _rb.constraints = RigidbodyConstraints.None;
        _rb.constraints = RigidbodyConstraints.FreezePositionZ;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX;
        _rb.constraints = RigidbodyConstraints.FreezeRotationY;
        */
    }

    public void SetFallVelocity(float velocity, bool fliped)
    {
        fallVelocity = velocity;
        isFliped = fliped;
    }

    private void SetRotation()
    {
        Vector2 direcao = new Vector2(_rb.velocity.x, _rb.velocity.y).normalized;
        angulo = Mathf.Atan2(direcao.x, direcao.y * -1) * Mathf.Rad2Deg;

        //Mas caso perceba que radianos não seja o ideal, basta multiplicar por Mathf.Rad2Deg;
        //float angulo = Mathf.Atan2(direcaoNormalizada.x, direcaoNormalizada.y) * Mathf.Rad2Deg;

        //transform.localEulerAngles = new Vector3(0, 0, angulo);
        transform.rotation = Quaternion.Euler(0f, 0f, angulo);
    }
}
