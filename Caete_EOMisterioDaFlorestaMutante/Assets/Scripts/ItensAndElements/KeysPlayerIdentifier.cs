using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysPlayerIdentifier : MonoBehaviour
{
    public bool playerTouched = false;
    public GameObject light;
    public Animator bird;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerTouched = true;
            light.SetActive(true);
            bird.SetBool("Active", true);
        }
    }
}
