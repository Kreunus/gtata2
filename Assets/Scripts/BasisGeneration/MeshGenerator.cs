using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    private Mesh generatedMesh;

    [SerializeField] private int xSize = 8;
    [SerializeField] private int ySize = 8;
    [SerializeField] private float noiseScale = 0.1f;
    [SerializeField] private float noiseArea = 0.1f;
    [SerializeField] private bool drawGizmo = false;

    Vector3[] vertices;
    Vector2Int subdivisions;
    Vector2Int vertexSize;
    Vector2[] uvs;
    int[] triangles;

    void Start()
    {
        generatedMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = generatedMesh;

        CreateShape();
        DrawTriangles();
        UpdateMesh();
    }

    void CreateShape()
    {

        subdivisions = new Vector2Int(xSize, ySize);
        vertexSize = subdivisions + new Vector2Int(1, 1);

        vertices = new Vector3[vertexSize.x * vertexSize.y];
        uvs = new Vector2[vertices.Length];

        for (var y = 0; y < vertexSize.y; y++)              //for each vertex on Y
        {
            var v = (1f / subdivisions.y) * y;              //normalize the y coordinate as v

            for (var x = 0; x < vertexSize.x; x++)          //for each vertex on X
            {
                var u = (1f / subdivisions.x) * x;          //normalize the x coordinate as u

                float noise = Mathf.PerlinNoise(x * noiseArea, y * noiseArea) * noiseScale;
                var vertex = new Vector3(u, v, noise);          //calculate a vertex position

                var uv = new Vector2(u, v);

                var arrayIndex = x + y * vertexSize.x;


                vertices[x + y * vertexSize.x] = vertex;
                uvs[x + y * vertexSize.x] = uv;
            }
        }
    }

    void DrawTriangles()
    {
        triangles = new int[subdivisions.x * subdivisions.y * 6];

        for (var i = 0; i < subdivisions.x * subdivisions.y; i++)
        {
            var triangleIndex = (i % subdivisions.x) + (i / subdivisions.x) * vertexSize.x;
            var indexer = i * 6;

            //triangle 1
            triangles[indexer + 0] = triangleIndex;
            triangles[indexer + 1] = triangleIndex + subdivisions.x + 1;
            triangles[indexer + 2] = triangleIndex + 1;

            //triangle 2
            triangles[indexer + 3] = triangleIndex + 1;
            triangles[indexer + 4] = triangleIndex + subdivisions.x + 1;
            triangles[indexer + 5] = triangleIndex + subdivisions.x + 2;
        }
    }

    void UpdateMesh()
    {
        generatedMesh.Clear();

        generatedMesh.vertices = vertices;
        generatedMesh.uv = uvs;
        generatedMesh.triangles = triangles;

        generatedMesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (drawGizmo == true)
        {
            if (vertices == null)
                return;

            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], .01f);
            }
        }
    }
}