using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{
    public GameObject text;

    public VideoPlayer cutScene;


    public GameObject loadingScreen;

    public Image chargingStone;
    public Sprite[] chargingAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space)) && !text.activeSelf)
        {
            text.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.P) && text.activeSelf)
        {
            cutScene.Pause();
            StartCoroutine("LoadGameScene");
        }
        if (cutScene.isPaused)
        {
            StartCoroutine("LoadGameScene");
        }
        
    }

    IEnumerator LoadGameScene()
    {
        loadingScreen.SetActive(true);

        // Verificação dos componentes
        if (chargingStone == null)
        {
            Debug.LogError("Image component reference is missing.");
            yield break;
        }
        if (chargingAnimation == null || chargingAnimation.Length == 0)
        {
            Debug.LogError("No images assigned to the array.");
            yield break;
        }

        // Animação do Loading
        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameStages");

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            int frameIndex = Mathf.RoundToInt(progress * (chargingAnimation.Length - 1));
            chargingStone.sprite = chargingAnimation[frameIndex];

            Debug.Log(frameIndex);

            // Sai do loop quando o progresso é 100% e todos os frames foram exibidos
            if (asyncLoad.progress >= 0.9f && frameIndex >= chargingAnimation.Length - 1)
            {
                asyncLoad.allowSceneActivation = true;
                break;
            }

            yield return null;
        }

        // Ao completar carregamento libera transição de cena
        //asyncLoad.allowSceneActivation = true;
        //loadingScreen.SetActive(false);
    }
}
