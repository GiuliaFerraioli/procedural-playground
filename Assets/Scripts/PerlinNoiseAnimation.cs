using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseAnimation : MonoBehaviour
{
    public bool isAnimating = false;
    public float noiseScale = 1f;
    public float speed = 1f;
    public float amount = 0.5f;

    //Class to store mesh related data for each child
    private class MeshData
    {
        public Mesh originalMesh;
        public Mesh clonedMesh;
        public Vector3[] originalVertices;
        public Vector3[] normals;
    }

    private List<MeshData> meshDataList = new List<MeshData>();

    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        //Setup data for each mesh
        foreach (MeshFilter meshFilter in meshFilters)
        {
            MeshData data = new MeshData();
            data.originalMesh = meshFilter.mesh;
            data.clonedMesh = new Mesh();
            data.clonedMesh.vertices = data.originalMesh.vertices;
            data.clonedMesh.triangles = data.originalMesh.triangles;
            data.clonedMesh.normals = data.originalMesh.normals;
            data.clonedMesh.uv = data.originalMesh.uv;
            //Store the original vertex positions and normals
            data.originalVertices = data.originalMesh.vertices;
            data.normals = data.originalMesh.normals;
            //Replace the original mesh with the copy
            meshFilter.mesh = data.clonedMesh;
            meshDataList.Add(data);
        }
    }

    void Update()
    {
        if (isAnimating)
        {
            foreach (MeshData data in meshDataList)
            {
                Vector3[] vertices = new Vector3[data.originalVertices.Length];

                for (int i = 0; i < vertices.Length; i++)
                {
                    Vector3 vertex = data.originalVertices[i];
                    //Generate Perlin noise value based on vertex position and time
                    float noise = Mathf.PerlinNoise(
                        vertex.x * noiseScale + Time.time * speed,
                        vertex.y * noiseScale + Time.time * speed
                    );
                    //Move vertices on their normal direction based on noise
                    vertices[i] = vertex + data.normals[i] * noise * amount;
                }

                data.clonedMesh.vertices = vertices;
                data.clonedMesh.RecalculateBounds();
            }
        }
        else
        {
            //Reset all meshes to their original state
            foreach (MeshData data in meshDataList)
            {
                data.clonedMesh.vertices = data.originalVertices;
                data.clonedMesh.RecalculateBounds();
            }
        }
    }
}

