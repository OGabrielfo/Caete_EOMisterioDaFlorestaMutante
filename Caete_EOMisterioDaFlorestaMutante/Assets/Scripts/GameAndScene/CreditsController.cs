using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GoToMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(37.6f);
        SceneManager.LoadScene("MenuInicial");
    }
}
