using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public float velocidadeMovimento = 2.5f; // Ajuste a velocidade de movimento conforme necessário
    public float intensidadeDistorcao = 5.1f; // Ajuste a intensidade da distorção conforme necessário

    private float offsetX;
    private float offsetY;

    private Material materialAgua;

    private void Start()
    {
        materialAgua = GetComponent<Renderer>().material;
        offsetX = Random.Range(0f, 100f);
        offsetY = Random.Range(0f, 100f);
    }

    private void Update()
    {
        float offsetAtualX = Time.time * velocidadeMovimento + offsetX;
        float offsetAtualY = Time.time * velocidadeMovimento + offsetY;
        Vector2 offset = new Vector2(offsetAtualX, offsetAtualY);

        materialAgua.SetTextureOffset("_MainTex", offset);
        materialAgua.SetTextureOffset("_BumpMap", offset * intensidadeDistorcao);
    }
}
