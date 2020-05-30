using UnityEngine;

public class SphereGen : AbstraktGeneration
{
    public override void CreateShape()
    {
        XYEdgeSize = XYEdgeSize / 2 * 2;
        ZEdgeSize = ZEdgeSize / 2 * 2;

        Subdivisions = new Vector2Int(ZEdgeSize, XYEdgeSize);
        VertexSize = Subdivisions + new Vector2Int(1, 1);

        Vertices = new Vector3[VertexSize.x * VertexSize.y + 2];
        Uvs = new Vector2[Vertices.Length];

        float fullAngle = Mathf.PI * 2;

        for (var y = 0; y < VertexSize.y; y++)              //for each vertex on Y
        {
            var v = (1f / Subdivisions.y) * y;              //normalize the y coordinate as v

            for (var x = 0; x < VertexSize.x; x++)          //for each vertex on X
            {
                var u = (1f / Subdivisions.x) * x;          //normalize the x coordinate as u

                var vertex = new Vector3(XRadius * Mathf.Cos(u * fullAngle) * Mathf.Cos(v * fullAngle), 
                    YRadius * Mathf.Cos(u * fullAngle) * Mathf.Sin(v * fullAngle), 
                    ZRadius * Mathf.Sin(u * fullAngle));

                var uv = new Vector2(u, v);

                var arrayIndex = x + y * VertexSize.x;
                
                Vertices[x + y * VertexSize.x] = vertex;
                Uvs[x + y * VertexSize.x] = uv;
            }
        }

        Vertices[VertexSize.x * VertexSize.y + 1] = new Vector3(0, 0, -ZRadius);
        Vertices[VertexSize.x * VertexSize.y] = new Vector3(0, 0, +ZRadius);
    }

    public override void DrawTriangles()
    {
        Triangles = new int[(Subdivisions.x * Subdivisions.y * 6) + (Subdivisions.y * 6)];
        var offset = Subdivisions.x * Subdivisions.y * 6;       //offset for correct triangle index (no collision with indexer)
        var offset2 = Subdivisions.x * Subdivisions.y * 6 + (Subdivisions.y * 3);       //offset for correct triangle index (no collision with offset and indexer)

        for (var i = 0; i < (Subdivisions.x * Subdivisions.y) / 2; i++)
        {
            var triangleIndex = (i % Subdivisions.x) + (i / Subdivisions.x) * VertexSize.x;
            var indexer = i * 6;

            if ((i % Subdivisions.x) > (Subdivisions.x*.75f) || (i % Subdivisions.x) < (Subdivisions.x * .25f - 1))
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
            else if ((i % Subdivisions.x) < (Subdivisions.x * .75f - 1) && (i % Subdivisions.x) > (Subdivisions.x * .25f))       //normals turned around
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
            
            //fillup off empty circle areas with triangles
            if (i % Subdivisions.x == (Subdivisions.x / 4))
            {
                if (Subdivisions.x % 4 != 0)
                {
                    Triangles[offset + 0] = triangleIndex;
                    Triangles[offset + 2] = VertexSize.x * VertexSize.y;
                    Triangles[offset + 1] = triangleIndex + Subdivisions.x + 1;
                    offset += 3;        //itterate over avaliable space
                }
                else
                {
                    Triangles[offset + 0] = triangleIndex - 1;
                    Triangles[offset + 2] = VertexSize.x * VertexSize.y;
                    Triangles[offset + 1] = triangleIndex + Subdivisions.x;
                    offset += 3;        //itterate over avaliable space
                }
                
                Triangles[offset2 + 0] = triangleIndex + 1;
                Triangles[offset2 + 2] = VertexSize.x * VertexSize.y;
                Triangles[offset2 + 1] = triangleIndex + Subdivisions.x + 2;
                offset2 += 3;        //itterate over avaliable space
            }
            else if (i % Subdivisions.x == (Subdivisions.x / 4 * 3))
            {
                if (Subdivisions.x % 4 != 0)
                {
                    Triangles[offset + 0] = triangleIndex + 2;
                    Triangles[offset + 1] = VertexSize.x * VertexSize.y + 1;
                    Triangles[offset + 2] = triangleIndex + Subdivisions.x + 3;
                    offset += 3;        //itterate over avaliable space
                }
                else
                {
                    Triangles[offset + 0] = triangleIndex - 1;
                    Triangles[offset + 1] = VertexSize.x * VertexSize.y + 1;
                    Triangles[offset + 2] = triangleIndex + Subdivisions.x;
                    offset += 3;        //itterate over avaliable space
                }
                
                Triangles[offset2 + 0] = triangleIndex + 1;
                Triangles[offset2 + 1] = VertexSize.x * VertexSize.y + 1;
                Triangles[offset2 + 2] = triangleIndex + Subdivisions.x + 2;
                offset2 += 3;
            }
        }
    }
}
