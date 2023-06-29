using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenButtonsActivator : MonoBehaviour
{
    public GameObject background, buttonContinuar, buttonSair;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DeathScreenActivator()
    {
        background.SetActive(true);
        buttonContinuar.SetActive(true);
        buttonSair.SetActive(true);
        gameObject.SetActive(false);
    }
}
