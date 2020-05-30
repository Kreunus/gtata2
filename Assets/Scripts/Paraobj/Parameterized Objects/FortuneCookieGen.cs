using UnityEngine;


public class FortuneCookieGen : AbstraktGeneration
{
    public override void CreateShape()
    {
        XYEdgeSize = XYEdgeSize / 2 * 2;
        ZEdgeSize = ZEdgeSize / 2 * 2;

        Subdivisions = new Vector2Int(ZEdgeSize, XYEdgeSize);
        VertexSize = Subdivisions + new Vector2Int(1, 1);

        Vertices = new Vector3[VertexSize.x * VertexSize.y];
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
                    XRadius * Mathf.Cos(u * fullAngle) * Mathf.Cos(v * fullAngle),
                    YRadius * Mathf.Cos(u * fullAngle),
                    ZRadius * Mathf.Sin(u * fullAngle) * Mathf.Sin(v * fullAngle)
                    );

                var uv = new Vector2(u, v);

                var arrayIndex = x + y * VertexSize.x;

                Vertices[x + y * VertexSize.x] = vertex;
                Uvs[x + y * VertexSize.x] = uv;
            }
        }
    }
    

    public override void DrawTriangles()
    {
        Triangles = new int[Subdivisions.x * Subdivisions.y * 6];

        for (var i = 0; i < Subdivisions.x * Subdivisions.y / 2; i++)
        {
            var triangleIndex = (i % Subdivisions.x) + (i / Subdivisions.x) * VertexSize.x;
            var indexer = i * 6;

            if ((i % Subdivisions.x) >= (Subdivisions.x * .75f) || (i % Subdivisions.x) < (Subdivisions.x * .25f))
            {
                //triangle 1
                Triangles[indexer + 0] = triangleIndex;
                Triangles[indexer + 1] = triangleIndex + Subdivisions.x + 1;
                Triangles[indexer + 2] = triangleIndex + 1;

                //triangle 2
                Triangles[indexer + 3] = triangleIndex + 1;
                Triangles[indexer + 4] = triangleIndex + Subdivisions.x + 1;
                Triangles[indexer + 5] = triangleIndex + Subdivisions.x + 2;
            }
            else if ((i % Subdivisions.x) < (Subdivisions.x * .75f) && (i % Subdivisions.x) >= (Subdivisions.x * .25f))       //normals turned around
            {
                //triangle 1
                Triangles[indexer + 0] = triangleIndex;
                Triangles[indexer + 2] = triangleIndex + Subdivisions.x + 1;
                Triangles[indexer + 1] = triangleIndex + 1;

                //triangle 2
                Triangles[indexer + 3] = triangleIndex + 1;
                Triangles[indexer + 5] = triangleIndex + Subdivisions.x + 1;
                Triangles[indexer + 4] = triangleIndex + Subdivisions.x + 2;
            }
        }
    }
}
