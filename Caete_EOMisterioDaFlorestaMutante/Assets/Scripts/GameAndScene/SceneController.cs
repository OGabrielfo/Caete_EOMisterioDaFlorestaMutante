using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [HideInInspector]
    public int mula, mapinguari, iara, boitata, homem, pontos;
    [HideInInspector]
    public int hpUp01, hpUp02, hpUp03, spUp01, spUp02;

    public GameObject deathScreen;

    public GameController gameController;

    private GameObject _player;
    private PlayerController _playerController;

    [SerializeField] private GameObject saveAnimation, transition;
    [SerializeField] private GameObject vidaMax, vidaAtual, dashLimit, dashCounter, moldura;
    public Sprite[] vidaMaxImgs, vidaAtualImgs, moldurasCaete;

    private float baseHPSize, baseSPSize;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();

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
            spUp01 = gameController._spUp01;
            spUp02 = gameController._spUp02;
        }
        else
        {
            gameController.NewGame();
        }

        gameController.LoadConfigs();
        LoadGame();
        SaveGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerInformation();

        // FadeIn
        StartCoroutine("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        // Habilidades
        mula = gameController._mula;
        mapinguari = gameController._mapinguari;
        iara = gameController._iara;
        boitata = gameController._boitata;
        hpUp01 = gameController._hpUp01;
        hpUp02 = gameController._hpUp02;
        hpUp03 = gameController._hpUp03;
        spUp01 = gameController._spUp01;
        spUp02 = gameController._spUp02;
        UpdatePlayerInformation();

        // Player Death
        if (deathScreen.activeSelf)
        {
            _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _player.GetComponent<Rigidbody>().useGravity = false;
        }
        
        #region TESTE DE HABILIDADES
        if (Input.GetKeyDown(KeyCode.C) && Input.GetKeyDown(KeyCode.Alpha0))
        {
            gameController._mula = 0;
            gameController._mapinguari = 0;
            gameController._iara = 0;
            gameController._boitata = 0;
            gameController._homem = 0;
        }

        if (Input.GetKeyDown(KeyCode.C) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameController._mula = 1;
            gameController._mapinguari = 0;
            gameController._iara = 0;
            gameController._boitata = 0;
            gameController._homem = 0;
        }

        if (Input.GetKeyDown(KeyCode.C) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameController._mula = 1;
            gameController._mapinguari = 1;
            gameController._iara = 1;
            gameController._boitata = 1;
            gameController._homem = 1;
        }
        #endregion
        
    }

    private void UpdatePlayerInformation()
    {
        // Update Player Information
        // Dash
        if (mula != 0)
        {
            _playerController._canDash = true;
        }
        else
        {
            _playerController._canDash = false;
        }

        // Jump
        if (mapinguari != 0)
        {
            _playerController.jumpLimit = 2;
            _playerController._canClimb = true;
        }
        else
        {
            _playerController.jumpLimit = 1;
            _playerController._canClimb = false;
        }

        // Swim
        if (iara != 0)
        {
            _playerController._canSwim = true;
        }
        else
        {
            _playerController._canSwim = false;
        }

        // Tranformação Tatu
        if (boitata != 0)
        {
            _playerController._canTatuTransform = true;
        }
        else
        {
            _playerController._canTatuTransform = false;
        }

        // Hp Máximo
        _playerController.vidaMax = 3 + hpUp01 + hpUp02 + hpUp03;

        // Sp Máximo
        _playerController.dashLimit = mula + spUp01 + spUp02;
    }

    private void LateUpdate()
    {
        // Update HUD
            // Limites HP
        vidaMax.GetComponent<Image>().sprite = vidaMaxImgs[_playerController.vidaMax - 3];
        vidaAtual.GetComponent<Image>().sprite = vidaAtualImgs[_playerController.vidaMax - 3];

            // Controles de HP
        RectTransform vidaMaxRect = vidaMax.transform as RectTransform;
        RectTransform vidaAtualRect = vidaAtual.transform as RectTransform;
        vidaMaxRect.sizeDelta = new Vector2((_playerController.vidaMax * 32f) + 32f, vidaMaxRect.sizeDelta.y);
        if (_playerController.vida < _playerController.vidaMax)
        {
            vidaAtualRect.sizeDelta = new Vector2(_playerController.vida * 32f, vidaAtualRect.sizeDelta.y);
        }
        else
        {
            vidaAtualRect.sizeDelta = new Vector2((_playerController.vida * 32f) + 32f, vidaAtualRect.sizeDelta.y);
        }

            // Controles de SP
        RectTransform dashLimitRect = dashLimit.transform as RectTransform;
        RectTransform dashCounterRect = dashCounter.transform as RectTransform;
        dashLimitRect.sizeDelta = new Vector2((_playerController.dashLimit) * 39.5f, dashLimitRect.sizeDelta.y);
        dashCounterRect.sizeDelta = new Vector2((_playerController.dashLimit - _playerController.dashCounter) * 39.5f, dashLimitRect.sizeDelta.y);

            // Moldura
        if (_playerController.vida >= _playerController.vidaMax * 0.8)
        {
            moldura.GetComponent<Image>().sprite = moldurasCaete[0];
        }
        else if (_playerController.vida >= _playerController.vidaMax * 0.5)
        {
            moldura.GetComponent<Image>().sprite = moldurasCaete[1];
        }
        else if (_playerController.vida <= _playerController.vidaMax * 0.35)
        {
            moldura.GetComponent<Image>().sprite = moldurasCaete[2];
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
        gameController._spUp01 = spUp01;
        gameController._spUp02 = spUp02;

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
        spUp01 = gameController._spUp01;
        spUp02 = gameController._spUp02;

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
            transition.SetActive(true);
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
