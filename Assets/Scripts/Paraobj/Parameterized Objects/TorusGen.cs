using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusGen : AbstraktGeneration
{
    public override void CreateShape()
    {
        XYEdgeSize = XYEdgeSize / 2 * 2;
        ZEdgeSize = ZEdgeSize / 2 * 2;

        Subdivisions = new Vector2Int(ZEdgeSize, XYEdgeSize);
        VertexSize = Subdivisions + new Vector2Int(1, 1);

        Vertices = new Vector3[VertexSize.x * VertexSize.y];
        Uvs = new Vector2[Vertices.Length];

        float fullAngle = Mathf.PI * 2;

        for (var y = 0; y < VertexSize.y; y++)              //for each vertex on Y
        {
            var v = (1f / Subdivisions.y) * y;              //normalize the y coordinate as v

            for (var x = 0; x < VertexSize.x; x++)          //for each vertex on X
            {
                var u = (1f / Subdivisions.x) * x;          //normalize the x coordinate as u

                var vertex = new Vector3(
                    (TorusRadiusX + XRadius * Mathf.Cos(u * fullAngle)) * Mathf.Cos(v * fullAngle),
                    (TorusRadiusY + YRadius * Mathf.Cos(u * fullAngle)) * Mathf.Sin(v * fullAngle),
                    ZRadius * Mathf.Sin(u * fullAngle)
                    );

                var uv = new Vector2(u, v);

                var arrayIndex = x + y * VertexSize.x;

                Vertices[x + y * VertexSize.x] = vertex;
                Uvs[x + y * VertexSize.x] = uv;
            }
        }
    }
}
