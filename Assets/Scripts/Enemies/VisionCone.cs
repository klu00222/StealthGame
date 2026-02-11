using UnityEngine;

public class VisionCone : MonoBehaviour
{
    [Header("Vision Rendering")]
    [SerializeField]
    private Material visionMaterial;
    [SerializeField]
    private int visionResolution = 30;

    private Mesh visionMesh;
    private MeshFilter meshFilter;
    private EnemyData enemyData;

    private void Awake()
    {
        enemyData = GetComponentInParent<EnemyData>();
        meshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = visionMaterial;

        visionMesh = new Mesh
        {
            name = "Vision Cone Mesh"
        };

        meshFilter.mesh = visionMesh;
    }

    private void FixedUpdate()
    {
        DrawVisionCone();
    }

    private void DrawVisionCone()
    {
        int vertexCount = visionResolution + 2;

        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;

        float angleStep = enemyData.VisionAngle / visionResolution;
        float startAngle = -enemyData.VisionAngle / 2f;

        for (int i = 0; i <= visionResolution; i++)
        {
            float angle = startAngle + (angleStep * i);

            // LOCAL SPACE — DO NOT USE transform.right
            Vector3 dir = Quaternion.Euler(0f, 0f, angle) * Vector3.right;
            vertices[i + 1] = dir * enemyData.DetectionRange;
        }

        int triIndex = 0;
        for (int i = 0; i < vertexCount - 2; i++)
        {
            triangles[triIndex++] = 0;
            triangles[triIndex++] = i + 1;
            triangles[triIndex++] = i + 2;
        }

        visionMesh.Clear();
        visionMesh.vertices = vertices;
        visionMesh.triangles = triangles;
        visionMesh.RecalculateBounds();
    }
}
