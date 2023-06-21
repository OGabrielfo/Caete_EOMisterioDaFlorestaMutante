using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeMesh : MonoBehaviour
{
    private MeshCollider _meshCollider;
    
    void Start()
    {
        // Obt�m o componente MeshCollider
        _meshCollider = GetComponent<MeshCollider>();

        // Obt�m o componente MeshFilter
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError("O objeto n�o possui um MeshFilter com uma malha v�lida.");
            return;
        }

        // Cria uma c�pia da malha do MeshFilter
        Mesh originalMesh = meshFilter.sharedMesh;
        Mesh newMesh = Instantiate(originalMesh);

        // Unifica os tri�ngulos na nova malha
        newMesh.CombineMeshes(new CombineInstance[] { new CombineInstance { mesh = newMesh } });

        // Atribui a nova malha unificada ao MeshCollider
        _meshCollider.sharedMesh = newMesh;
    }

    
    void Update()
    {
        
    }
}
