using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecVida : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().vida++;
            StartCoroutine("CoolDown");
        }
    }

    IEnumerator CoolDown()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        yield return new WaitForSeconds(120f);
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }
}