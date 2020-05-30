using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralGen : AbstraktGeneration
{
    public override void CreateShape()
    {
        ZEdgeSize = ZEdgeSize / 2 * 2;
        XYEdgeSize = XYEdgeSize / 2 * 2;

        Subdivisions = new Vector2Int(ZEdgeSize, XYEdgeSize);
        VertexSize = Subdivisions + new Vector2Int(1, 1);

        Vertices = new Vector3[VertexSize.x * VertexSize.y + 2];
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
                    XRadius * Mathf.Sqrt(u * (float)Mathf.PI) * Mathf.Cos(v * fullAngle * SpinAmmount),
                    YRadius * Mathf.Sqrt(u * (float)Mathf.PI) * Mathf.Sin(v * fullAngle * SpinAmmount),
                    ZRadius * v * (float)Mathf.PI
                    );

                var uv = new Vector2(u, v);
                var arrayIndex = x + y * VertexSize.x;

                Vertices[x + y * VertexSize.x] = vertex;
                Uvs[x + y * VertexSize.x] = uv;
            }
        }
    }
}
