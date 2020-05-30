using UnityEngine;
using UnityEditor;

public class ParaobjManager : Singleton<ParaobjManager>
{
    private Mesh generatedMesh;

    public enum ObjectType
    {
        none,
        sphere,
        torus,
        cylinder,
        fortuneCookie,
        wheel,
        cone,
        spiral
    }
    public ObjectType type = ObjectType.none;

    public int XYEdgeSize = 20;
    public int ZEdgeSize = 20;

    public float XRadius = 1;
    public float YRadius = 1;
    public float ZRadius = 1;

    public float Stretch = 0.8f;
    public float Bow = 2;
    public float SpinAmmount = 1;

    public float TorusRadiusX = 4;
    public float TorusRadiusY = 4;

    public bool drawGizmo = false;
    public float gizmoSize = 0.01f;

    public Vector3[] Vertices;
    public Vector2Int Subdivisions;
    public Vector2Int VertexSize;
    public Vector2[] Uvs;
    public int[] Triangles;

    private void Start()
    {
        generatedMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = generatedMesh;
        

        switch (type)
        {
            case ObjectType.none:
                Debug.LogError("No Object Selected at Crazy Generate");
                break;

            case ObjectType.sphere:
                SphereGen sphere = new SphereGen();
                CreateObject(sphere);
                break;

            case ObjectType.torus:
                TorusGen torus = new TorusGen();
                CreateObject(torus);
                break;

            case ObjectType.cylinder:
                CylinderGen cylinder = new CylinderGen();
                CreateObject(cylinder);
                break;

            case ObjectType.fortuneCookie:
                FortuneCookieGen fortuneCookie = new FortuneCookieGen();
                CreateObject(fortuneCookie);
                break;

            case ObjectType.wheel:
                WheelGen wheel = new WheelGen();
                CreateObject(wheel);
                break;

            case ObjectType.cone:
                ConeGen cone = new ConeGen();
                CreateObject(cone);
                break;

            case ObjectType.spiral:
                SpiralGen sprial = new SpiralGen();
                CreateObject(sprial);
                break;

            default:
                Debug.LogError("Default Object Selected at Crazy Generate");
                break;
        }
    }

    private void CreateObject(AbstraktGeneration obj)
    {
        string objName = obj.ToString();

        obj.CreateShape();
        obj.DrawTriangles();

        UpdateMesh();
        GetComponent<MeshCollider>().sharedMesh = generatedMesh;

        AssetDatabase.CreateAsset(generatedMesh, "Assets/Scripts/Paraobj/serialized/"+objName);
        AssetDatabase.SaveAssets();
    }

    private void UpdateMesh()
    {
        generatedMesh.Clear();

        generatedMesh.vertices = Vertices;
        generatedMesh.uv = Uvs;
        generatedMesh.triangles = Triangles;

        generatedMesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (drawGizmo == true)
        {
            if (Vertices == null)
                return;

            for (int i = 0; i < Vertices.Length; i++)
            {
                Gizmos.DrawSphere(Vertices[i], gizmoSize);
            }
        }
    }

}
