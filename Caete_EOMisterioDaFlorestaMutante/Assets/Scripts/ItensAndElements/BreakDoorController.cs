using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDoorController : MonoBehaviour
{
    public GameObject breakParticles;
    public AudioClip breaking;
    public AudioSource audioControl;

    private MeshRenderer _mesh;
    private BoxCollider _col;
    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>()._isDashing)
            {
                StartCoroutine("DestroyDoor");
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>()._isDashing)
            {
                StartCoroutine("DestroyDoor");
            }
        }
    }

    IEnumerator DestroyDoor()
    {
        audioControl.clip = breaking;
        audioControl.Play();

        breakParticles.SetActive(true);
        _mesh.enabled = false;
        _col.enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(breakParticles);
        Destroy(gameObject);
    }
}
