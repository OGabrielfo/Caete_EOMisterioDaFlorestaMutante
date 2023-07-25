using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BossController : MonoBehaviour
{
    public GameObject bossHUD, controleHP, retratoBoss, bossObject;
    public GameController gameController;
    public Sprite[] bossSprites;

    public string bossName;
    public int life;

    private void Awake()
    {
        retratoBoss.SetActive(true);
        if (PlayerPrefs.HasKey(bossName) && PlayerPrefs.GetInt(bossName) != 0)
        {
            Destroy(bossObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (life >= 5)
        {
            retratoBoss.GetComponent<UnityEngine.UI.Image>().sprite = bossSprites[0];
        }
        else if (life >= 3)
        {
            retratoBoss.GetComponent<UnityEngine.UI.Image>().sprite = bossSprites[1];
        }
        else
        {
            retratoBoss.GetComponent<UnityEngine.UI.Image>().sprite = bossSprites[2];
        }
    }

    private void LateUpdate()
    {
        RectTransform HPRect = controleHP.transform as RectTransform;
        HPRect.sizeDelta = new Vector2(HPRect.sizeDelta.x, 20.85f * life);
    }

    public void DeadBoss()
    {
        bossHUD.SetActive(false);
        controleHP.SetActive(false);
        retratoBoss.SetActive(false);
        switch (bossName)
        {
            case "Mula":
                gameController._mula = 1;
                break;
            case "Mapinguari":
                gameController._mapinguari = 1;
                break;
            default: break;
        }

        gameController.SaveGame();

        Destroy(bossObject);

    }
}
