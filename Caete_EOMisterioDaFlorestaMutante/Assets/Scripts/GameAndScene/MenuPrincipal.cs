using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.HighDefinition.CameraSettings;

public class MenuPrincipal : MonoBehaviour
{
    private GameController gameController;

    public GameObject loadingScreen, configScreen;

    public Button continuar;

    public Image chargingStone;
    public Sprite[] chargingAnimation;

    public GameObject transition;

    public AudioMixer audioMixer;

    [SerializeField] private Slider volumeMusic;
    [SerializeField] private Slider volumeSFX;

    private void Awake()
    {
        loadingScreen.SetActive(false);
        gameController = GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {

        // FadeIn
        StartCoroutine("FadeIn");
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
        //StartCoroutine("LoadGameScene");
        SceneManager.LoadScene("CutSceneInicio");
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
        configScreen.SetActive(true);
    }

    public void CloseConfig()
    {
        configScreen.SetActive(false);
    }

    public void SFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", volumeSFX.value);
        gameController._volumeSFX = volumeSFX.value;
        gameController.SaveConfigs();
    }

    public void MusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", volumeMusic.value);
        gameController._volumeMusic = volumeMusic.value;
        gameController.SaveConfigs();
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
                asyncLoad.allowSceneActivation = true;
                break;
            }

            yield return null;
        }

        // Ao completar carregamento libera transição de cena
        //asyncLoad.allowSceneActivation = true;
        //loadingScreen.SetActive(false);
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
