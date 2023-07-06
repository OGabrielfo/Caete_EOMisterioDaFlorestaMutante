using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrogController : MonoBehaviour
{
    public int vidaTotal;
    public float fireDelay;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        StartCoroutine("FrogFire");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FrogFire()
    {
        _anim.SetBool("Idle", true);
        yield return new WaitForSeconds(fireDelay);
        _anim.SetBool("Idle", false);
        StartCoroutine("FrogFire");
    }
}
