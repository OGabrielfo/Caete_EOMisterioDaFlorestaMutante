using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrogController : MonoBehaviour
{
    public int vidaTotal;
    public float fireDelay, fireSpeed, fireFallVelocity;

    public GameObject frogFire, firePoint, explosionFX;

    public AudioClip dano, morte, ataque;
    public AudioSource audioControl;

    private int vida;
    private bool _dead;
    private Animator _anim;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        vida = vidaTotal;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        StartCoroutine("FrogFireDelay");
    }

    // Update is called once per frame
    void Update()
    {
        if(_dead)
        {
            _rb.velocity = Vector3.zero;
        }
    }

    IEnumerator FrogFireDelay()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            firePoint.transform.position = new Vector3(transform.position.x - 0.6f, firePoint.transform.position.y, firePoint.transform.position.z);
        }
        else
        {
            firePoint.transform.position = new Vector3(transform.position.x + 0.6f, firePoint.transform.position.y, firePoint.transform.position.z);
        }

        _anim.SetBool("Idle", true);
        yield return new WaitForSeconds(fireDelay);
        _anim.SetBool("Idle", false);
        //StartCoroutine("FrogFireDelay");
    }

    public void FrogFire()
    {
        StartCoroutine("FrogFireDelay");
        if (!_dead)
        {
            audioControl.clip = ataque;
            audioControl.Play();
            GameObject bullet = Instantiate(frogFire, firePoint.transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyFrogFire>().SetFallVelocity(fireFallVelocity, gameObject.GetComponent<SpriteRenderer>().flipX);
            bullet.layer = LayerMask.NameToLayer("Attacks");
            if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
            {
                bullet.GetComponent<Rigidbody>().velocity = new Vector3(fireSpeed * -1, 2f, 0f);
            }
            else
            {
                bullet.GetComponent<Rigidbody>().velocity = new Vector3(fireSpeed, 2f, 0f);
            }
        }
    }

    public void ReceberDano()
    {
        if (vida > 0)
        {
            audioControl.clip = dano;
            audioControl.Play();
            vida--;
            _anim.SetTrigger("TakeDamage");
        }
        Debug.Log("Recebeu Dano");
    }

    public void EnemyDead()
    {
        if (audioControl.clip != morte)
        {
            audioControl.clip = morte;
            audioControl.Play();
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        StopCoroutine("FrogFireDelay");

        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        _dead = true;
        yield return new WaitForSeconds(120f);
        vida = vidaTotal;
        explosionFX.SetActive(false);
        _dead = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        StartCoroutine("FrogFireDelay");
    }

    public void DeathExplosion()
    {
        explosionFX.SetActive(true);
    }
}
