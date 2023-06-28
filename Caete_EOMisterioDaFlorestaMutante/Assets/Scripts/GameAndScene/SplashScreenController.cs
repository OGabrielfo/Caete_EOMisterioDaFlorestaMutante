using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenController : MonoBehaviour
{

    public GameObject transition, splashScreen;
    UnityEngine.AsyncOperation asyncLoad;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator anim = splashScreen.gameObject.GetComponent<Animator>();
        float duracao = anim.GetCurrentAnimatorStateInfo(0).length;
        Destroy(splashScreen, duracao);

        asyncLoad = SceneManager.LoadSceneAsync("MenuInicial");
        asyncLoad.allowSceneActivation = false;

        // FadeIn
        StartCoroutine("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        if (splashScreen.IsDestroyed())
        {
            asyncLoad.allowSceneActivation = true;
            //SceneManager.LoadScene("MenuInicial");
        }
    }

    IEnumerator FadeIn()
    {
        transition.SetActive(true);
        float timer = 0f;
        float fadeDuration = 0.5f;
        float currentAlpha = 1f;
        Image transitionImage = transition.GetComponent<Image>();

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(timer / fadeDuration);
            float alpha = 1f - normalizedTime;

            currentAlpha = Mathf.Lerp(1f, alpha, normalizedTime);
            transitionImage.color = new Color(0f, 0f, 0f, currentAlpha);
            yield return null;
        }
        currentAlpha = 0f;
        transitionImage.color = new Color(0f, 0f, 0f, currentAlpha);
        transition.SetActive(false);
    }

}
