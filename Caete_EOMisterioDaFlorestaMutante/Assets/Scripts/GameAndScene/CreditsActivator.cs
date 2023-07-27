using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsActivator : MonoBehaviour
{
    public GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GoToCredits()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Credits");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            HUD.SetActive(false);
            StartCoroutine("GoToCredits");
        }
    }
}
