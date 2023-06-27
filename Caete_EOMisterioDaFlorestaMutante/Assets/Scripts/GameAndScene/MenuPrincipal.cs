using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    private GameController gameController;

    public GameObject loadingScreen;

    public Button continuar;

    public Image chargingStone;
    public Sprite[] chargingAnimation;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController._gamePlayed == 1)
        {
            continuar.interactable = true;
        }
        else
        {
            continuar.interactable = false;
        }
    }

    public void NewGame()
    {
        gameController.NewGame();
        //SceneManager.LoadSceneAsync("GameStages");
        StartCoroutine("LoadGameScene");
    }

    public void LoadGame()
    {
        gameController.LoadGame();
        //SceneManager.LoadSceneAsync("GameStages");
        StartCoroutine("LoadGameScene");
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Config()
    {

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

            // Sai do loop quando o progresso é 100% e todos os frames foram exibidos
            if (asyncLoad.progress >= 0.9f && frameIndex >= chargingAnimation.Length - 1)
            {
                break;
            }

            yield return null;
        }

        // Ao completar carregamento libera transição de cena
        asyncLoad.allowSceneActivation = true;
        loadingScreen.SetActive(false);
    }
}
