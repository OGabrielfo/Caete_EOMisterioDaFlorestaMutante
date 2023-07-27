using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    public GameObject parentObject, rockExplosion;

    public AudioSource audioControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rockExplosion.transform.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().SendMessage("ReceberDano");
            rockExplosion.SetActive(true);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            audioControl.Play();
            StartCoroutine("DestroyObject");
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
        {
            rockExplosion.SetActive(true);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            audioControl.Play();
            StartCoroutine("DestroyObject");
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5f);
        if (gameObject!= null)
        {
            Destroy(parentObject);
        }
    }
}
