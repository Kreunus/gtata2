using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeGen : AbstraktGeneration
{
    public override void CreateShape()
    {
        ZEdgeSize = ZEdgeSize / 2 * 2;
        XYEdgeSize = XYEdgeSize / 2 * 2;

        Subdivisions = new Vector2Int(ZEdgeSize, XYEdgeSize);
        VertexSize = Subdivisions + new Vector2Int(1, 1);

        Vertices = new Vector3[VertexSize.x * VertexSize.y + 1];
        Uvs = new Vector2[Vertices.Length];

        var vertex = new Vector3();
        float fullAngle = Mathf.PI * 2;

        for (var y = 0; y < VertexSize.y; y++)              //for each vertex on Y
        {
            var v = (1f / Subdivisions.y) * y;              //normalize the y coordinate as v

            for (var x = 0; x < VertexSize.x; x++)          //for each vertex on X
            {
                var u = (1f / Subdivisions.x) * x;          //normalize the x coordinate as u

                vertex = new Vector3(
                    XRadius * Mathf.Sqrt(Mathf.Pow(u * (float)Mathf.PI, Bow)) * Mathf.Cos(v * fullAngle),
                    YRadius * Mathf.Sqrt(Mathf.Pow(u * (float)Mathf.PI, Bow)) * Mathf.Sin(v * fullAngle),
                    ZRadius * u * (float)Mathf.PI
                    );

                var uv = new Vector2(u, v);
                var arrayIndex = x + y * VertexSize.x;

                Vertices[x + y * VertexSize.x] = vertex;
                Uvs[x + y * VertexSize.x] = uv;
            }
        }

        Vertices[VertexSize.x * VertexSize.y] = new Vector3(0, 0, ZRadius * (float)Mathf.PI);
    }

    public override void DrawTriangles()
    {

        Triangles = new int[(Subdivisions.x * Subdivisions.y * 6) + (Subdivisions.y * 3)];
        var offset = Subdivisions.x * Subdivisions.y * 6;       //offset for correct triangle index (no collision with indexer)

        for (var i = 0; i < Subdivisions.x * Subdivisions.y; i++)
        {
            var triangleIndex = (i % Subdivisions.x) + (i / Subdivisions.x) * VertexSize.x;
            var indexer = i * 6;

            //triangle 1
            Triangles[indexer + 0] = triangleIndex;
            Triangles[indexer + 1] = triangleIndex + Subdivisions.x + 1;
            Triangles[indexer + 2] = triangleIndex + 1;

            //triangle 2
            Triangles[indexer + 3] = triangleIndex + 1;
            Triangles[indexer + 4] = triangleIndex + Subdivisions.x + 1;
            Triangles[indexer + 5] = triangleIndex + Subdivisions.x + 2;


            //fillup off empty circle areas with triangles
            if (i % Subdivisions.x == Subdivisions.x - 1)
            {
                Triangles[offset + 0] = triangleIndex + 1;
                Triangles[offset + 2] = VertexSize.x * VertexSize.y;
                Triangles[offset + 1] = triangleIndex + Subdivisions.x + 2;
                offset += 3;        //itterate over avaliable space
            }
        }
    }
}
