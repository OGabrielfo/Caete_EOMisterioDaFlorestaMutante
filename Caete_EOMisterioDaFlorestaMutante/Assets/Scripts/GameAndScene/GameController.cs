using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Global Variables
    //Player
    [HideInInspector]
    public int _vidaMax, _vidaAtual, _limitePulo, _pontos;
    [HideInInspector]
    public float _posX, _posY, _posZ;

    //Bosses
    [HideInInspector]
    public int _mula, _mapinguari, _iara, _boitata, _homem;

    //Items
    [HideInInspector]
    public int _hpUp01, _hpUp02, _hpUp03, _hpUp04, _hpUp05;
    #endregion

    #region Variáveis Gerais
    public int _gamePlayed;
    public float _volumeGlobal;
    #endregion

    private void Awake()
    {
        if (PlayerPrefs.HasKey("gamePlayed"))
        {
            _gamePlayed = PlayerPrefs.GetInt("gamePlayed");
        }
        else
        {
            _gamePlayed = 0;
        }

        LoadConfigs();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NewGame() {
        PlayerPrefs.SetInt("gamePlayed", 1);

        //Player
        PlayerPrefs.GetInt("vidaMax", 3);
        PlayerPrefs.GetInt("vidaAtual", 3);
        PlayerPrefs.GetInt("limitePulo", 1);
        PlayerPrefs.GetInt("pontos", 0);

        //Bosses
        PlayerPrefs.GetInt("mula", 0);
        PlayerPrefs.GetInt("mapinguari", 0);
        PlayerPrefs.GetInt("iara", 0);
        PlayerPrefs.GetInt("boitata", 0);
        PlayerPrefs.GetInt("homem", 0);

        //Items
        PlayerPrefs.GetInt("hpUp01", 0);
        PlayerPrefs.GetInt("hpUp02", 0);
        PlayerPrefs.GetInt("hpUp03", 0);
        PlayerPrefs.GetInt("hpUp04", 0);
        PlayerPrefs.GetInt("hpUp05", 0);

    }

    public void SaveGame() {
        //Player
        PlayerPrefs.GetInt("vidaMax", _vidaMax);
        PlayerPrefs.GetInt("vidaAtual", _vidaAtual);
        PlayerPrefs.GetInt("limitePulo", _limitePulo);
        PlayerPrefs.GetInt("pontos", _pontos);
        PlayerPrefs.GetFloat("posX", _posX);
        PlayerPrefs.GetFloat("posY", _posY);
        PlayerPrefs.GetFloat("posZ", _posZ);

        //Bosses
        PlayerPrefs.GetInt("mula", _mula);
        PlayerPrefs.GetInt("mapinguari", _mapinguari);
        PlayerPrefs.GetInt("iara", _iara);
        PlayerPrefs.GetInt("boitata", _boitata);
        PlayerPrefs.GetInt("homem", _homem);

        //Items
        PlayerPrefs.GetInt("hpUp01", _hpUp01);
        PlayerPrefs.GetInt("hpUp02", _hpUp02);
        PlayerPrefs.GetInt("hpUp03", _hpUp03);
        PlayerPrefs.GetInt("hpUp04", _hpUp04);
        PlayerPrefs.GetInt("hpUp05", _hpUp05);
    }

    public void LoadGame() {
        //Player
        _vidaMax = PlayerPrefs.GetInt("vidaMax");
        _vidaAtual = PlayerPrefs.GetInt("vidaAtual");
        _limitePulo = PlayerPrefs.GetInt("limitePulo");
        _pontos = PlayerPrefs.GetInt("pontos");
        _posX = PlayerPrefs.GetFloat("posX");
        _posY = PlayerPrefs.GetFloat("posY");
        _posZ = PlayerPrefs.GetFloat("posZ");

        //Bosses
        _mula = PlayerPrefs.GetInt("mula");
        _mapinguari = PlayerPrefs.GetInt("mapinguari");
        _iara = PlayerPrefs.GetInt("iara");
        _boitata = PlayerPrefs.GetInt("boitata");
        _homem = PlayerPrefs.GetInt("homem");

        //Items
        _hpUp01 = PlayerPrefs.GetInt("hpUp01");
        _hpUp02 = PlayerPrefs.GetInt("hpUp02");
        _hpUp03 = PlayerPrefs.GetInt("hpUp03");
        _hpUp04 = PlayerPrefs.GetInt("hpUp04");
        _hpUp05 = PlayerPrefs.GetInt("hpUp05");

    }

    public void SaveConfigs()
    {
        PlayerPrefs.GetFloat("volumeGlobal", _volumeGlobal);
    }

    public void LoadConfigs()
    {
        if (PlayerPrefs.HasKey("volumeGlobal"))
        {
            _volumeGlobal = PlayerPrefs.GetFloat("volumeGlobal");
        }
        else
        {
            _volumeGlobal = 100;
            PlayerPrefs.GetFloat("volumeGlobal", _volumeGlobal);
        }
    }
}
