using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaActivator : MonoBehaviour
{
    public GameObject bossHUD, boss;


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
        if (other.gameObject.CompareTag("Player"))
        {
            bossHUD.SetActive(true);
            boss.SetActive(true);
        }
    }
}
