using UnityEngine;

public class CylinderGen : AbstraktGeneration
{

    public override void CreateShape()
    {

        Subdivisions = new Vector2Int(XYEdgeSize, ZEdgeSize);
        VertexSize = Subdivisions + new Vector2Int(1, 1);

        Vertices = new Vector3[VertexSize.x * VertexSize.y + 2];
        Uvs = new Vector2[Vertices.Length];

        float fullAngle = 2 * Mathf.PI;

        for (var y = 0; y < VertexSize.y; y++)              //for each vertex on Y
        {
            var v = (1f / Subdivisions.y) * y;              //normalize the y coordinate as v

            for (var x = 0; x < VertexSize.x; x++)          //for each vertex on X
            {
                var u = (1f / Subdivisions.x) * x;          //normalize the x coordinate as u
                
                var vertex = new Vector3(
                    XRadius * Mathf.Sin(v * fullAngle),
                    YRadius * Mathf.Cos(v * fullAngle),
                    ZRadius * -u
                    );

                var uv = new Vector2(u, v);

                var arrayIndex = x + y * VertexSize.x;


                Vertices[x + y * VertexSize.x] = vertex;
                Uvs[x + y * VertexSize.x] = uv;
            }
        }
        
        Vertices[VertexSize.x * VertexSize.y + 1] = new Vector3(0, 0, -ZRadius);
        Vertices[VertexSize.x * VertexSize.y] = new Vector3(0, 0, 0);
    }

    public override void DrawTriangles() {

        Triangles = new int[(Subdivisions.x * Subdivisions.y * 6) + (Subdivisions.y * 6)];
        var offset = Subdivisions.x * Subdivisions.y * 6;       //offset for correct triangle index (no collision with indexer)
        var offset2 = Subdivisions.x * Subdivisions.y * 6 + (Subdivisions.y * 3);       //offset for correct triangle index (no collision with offset and indexer)

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
            if (i % Subdivisions.x == 0)
            {
                Triangles[offset + 0] = triangleIndex;
                Triangles[offset + 1] = VertexSize.x * VertexSize.y;
                Triangles[offset + 2] = triangleIndex + Subdivisions.x + 1;
                offset += 3;        //itterate over avaliable space
            }
            if (i % Subdivisions.x == Subdivisions.x-1)
            {
                Triangles[offset2 + 0] = triangleIndex+1;
                Triangles[offset2 + 2] = VertexSize.x * VertexSize.y + 1;
                Triangles[offset2 + 1] = triangleIndex + Subdivisions.x + 2;
                offset2 += 3;        //itterate over avaliable space
            }
        }
    }
}
