using UnityEngine;

public abstract class AbstraktGeneration
{
    ParaobjManager o = ParaobjManager.Instance;

    public int XYEdgeSize {
        get { return o.XYEdgeSize; }
        set { o.XYEdgeSize = value; } }

    public int ZEdgeSize {
        get { return o.ZEdgeSize; }
        set { o.ZEdgeSize = value; } }


    public float XRadius {
        get { return o.XRadius; }
        set { o.XRadius = value; } }

    public float YRadius {
        get { return o.YRadius; }
        set { o.YRadius = value; } }

    public float ZRadius {
        get { return o.ZRadius; }
        set { o.ZRadius = value; } }


    public float Stretch {
        get { return o.Stretch; }
        set { o.Stretch = value; } }

    public float Bow {
        get { return o.Bow; }
        set { o.Bow = value; } }

    public float SpinAmmount {
        get { return o.SpinAmmount; }
        set { o.SpinAmmount = value; } }
    
    public float TorusRadiusX {
        get { return o.TorusRadiusX; }
        set { o.TorusRadiusX = value; } }

    public float TorusRadiusY {
        get { return o.TorusRadiusY; }
        set { o.TorusRadiusY = value; } }


    public Vector3[] Vertices {
        get { return o.Vertices; }
        set { o.Vertices = value; } }

    public Vector2Int Subdivisions {
        get { return o.Subdivisions; }
        set { o.Subdivisions = value; } }

    public Vector2Int VertexSize {
        get { return o.VertexSize; }
        set { o.VertexSize = value; } }

    public Vector2[] Uvs {
        get { return o.Uvs; }
        set { o.Uvs = value; } }

    public int[] Triangles {
        get { return o.Triangles; }
        set { o.Triangles = value; } }


    public abstract void CreateShape();

    public virtual void DrawTriangles()
    {
        Triangles = new int[Subdivisions.x * Subdivisions.y * 6];

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
        }
    }
}
