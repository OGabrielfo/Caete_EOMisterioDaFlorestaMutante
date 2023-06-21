using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeMesh : MonoBehaviour
{
    private MeshCollider _meshCollider;
    
    void Start()
    {
        // Obtém o componente MeshCollider
        _meshCollider = GetComponent<MeshCollider>();

        // Obtém o componente MeshFilter
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError("O objeto não possui um MeshFilter com uma malha válida.");
            return;
        }

        // Cria uma cópia da malha do MeshFilter
        Mesh originalMesh = meshFilter.sharedMesh;
        Mesh newMesh = Instantiate(originalMesh);

        // Unifica os triângulos na nova malha
        newMesh.CombineMeshes(new CombineInstance[] { new CombineInstance { mesh = newMesh } });

        // Atribui a nova malha unificada ao MeshCollider
        _meshCollider.sharedMesh = newMesh;
    }

    
    void Update()
    {
        
    }
}
