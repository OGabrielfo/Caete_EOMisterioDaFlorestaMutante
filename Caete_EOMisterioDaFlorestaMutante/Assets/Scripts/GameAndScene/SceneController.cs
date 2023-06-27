using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public int mula, mapinguari, iara, boitata, homem, pontos;
    public int hpUp01, hpUp02, hpUp03, hpUp04, hpUp05;


    public GameController gameController;

    private GameObject _player;

    [SerializeField] private GameObject saveAnimation;
    [SerializeField] private GameObject transition;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
        _player = GameObject.FindGameObjectWithTag("Player");

        if (gameController._gamePlayed == 1)
        {
            gameController.LoadGame();
            mula = gameController._mula;
            mapinguari = gameController._mapinguari;
            iara = gameController._iara;
            boitata = gameController._boitata;
            homem = gameController._homem;
            hpUp01 = gameController._hpUp01;
            hpUp02 = gameController._hpUp02;
            hpUp03 = gameController._hpUp03;
            hpUp04 = gameController._hpUp04;
            hpUp05 = gameController._hpUp05;
        }
        else
        {
            gameController.NewGame();
        }

        gameController.LoadConfigs();
        LoadGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        //Player
        gameController._vidaMax = _player.GetComponent<PlayerController>().vidaMax;
        gameController._vidaAtual = _player.GetComponent<PlayerController>().vida;
        gameController._limitePulo = _player.GetComponent<PlayerController>().jumpLimit;
        gameController._posX = _player.transform.position.x;
        gameController._posY = _player.transform.position.y;
        gameController._posZ = _player.transform.position.z;

        //Bosses
        gameController._mula = mula;
        gameController._mapinguari = mapinguari;
        gameController._iara = iara;
        gameController._boitata = boitata;
        gameController._homem = homem;
        
        //Items
        gameController._hpUp01 = hpUp01;
        gameController._hpUp02 = hpUp02;
        gameController._hpUp03 = hpUp03;
        gameController._hpUp04 = hpUp04;
        gameController._hpUp05 = hpUp05;

        gameController._pontos = pontos;

        gameController.SaveGame();
        StartCoroutine("SaveAnimation");
    }

    public void LoadGame()
    {
        gameController.LoadGame();

        //Player
        _player.GetComponent<PlayerController>().vidaMax = gameController._vidaMax;
        _player.GetComponent<PlayerController>().vida = gameController._vidaAtual;
        _player.GetComponent<PlayerController>().jumpLimit = gameController._limitePulo;
        if(PlayerPrefs.HasKey("posX") && PlayerPrefs.HasKey("posY") && PlayerPrefs.HasKey("posZ"))
        {
            _player.transform.position = new Vector3(gameController._posX, gameController._posY, gameController._posZ);
        }

        //Bosses
        mula = gameController._mula;
        mapinguari = gameController._mapinguari;
        iara = gameController._iara;
        boitata = gameController._boitata;
        homem = gameController._homem;

        //Items
        hpUp01 = gameController._hpUp01;
        hpUp02 = gameController._hpUp02;
        hpUp03 = gameController._hpUp03;
        hpUp04 = gameController._hpUp04;
        hpUp05 = gameController._hpUp05;

        pontos = gameController._pontos;
    }

    IEnumerator SaveAnimation()
    {
        saveAnimation.SetActive(true);
        saveAnimation.GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(2);
        saveAnimation.GetComponent<Animator>().SetTrigger("Out");
    }

    IEnumerator FadeIn()
    {
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
