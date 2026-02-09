using System;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetector : MonoBehaviour
{
    public LayerMask Player;
    public LayerMask Visible;

    [SerializeField] private float detectionRange;
    [SerializeField] private float visionAngle;

    [Header("Vision Rendering")]
    [SerializeField] private Material visionMaterial;
    [SerializeField] private int visionResolution = 30;

    private Mesh visionMesh;
    private MeshFilter meshFilter;

    public static event Action OnPlayerDetected;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Vector3 direction = Quaternion.AngleAxis(visionAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction * detectionRange);

        Vector3 direction2 = Quaternion.AngleAxis(-visionAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, direction2 * detectionRange);

        Gizmos.color = Color.white;
    }


    private void Awake()
    {
        Debug.Log($"{name} VisionDetector Awake — enabled={enabled}");

        meshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = visionMaterial;

        visionMesh = new Mesh
        {
            name = "Vision Cone Mesh"
        };

        meshFilter.mesh = visionMesh;
    }

    private void Update()
    {
        if (DetectPlayers().Length > 0)
        {
            Debug.Log("Player detected");
            OnPlayerDetected?.Invoke();
        }
    }

    //Wait for enemy to move and detect players first before drawing vision cone
    private void LateUpdate()
    {
        DrawVisionCone();
    }

    private void DrawVisionCone()
    {
        int vertexCount = visionResolution + 2;

        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;

        float angleStep = visionAngle / visionResolution;
        float startAngle = -visionAngle / 2f;

        for (int i = 0; i <= visionResolution; i++)
        {
            float angle = startAngle + (angleStep * i);

            // LOCAL SPACE — DO NOT USE transform.right
            Vector3 dir = Quaternion.Euler(0f, 0f, angle) * Vector3.right;
            vertices[i + 1] = dir * detectionRange;
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

    private Transform[] DetectPlayers()
    {
        List<Transform> players = new();

        if (PlayerInRange(ref players))
        {
            if (PlayerInAngle(ref players))
            {
                _ = PlayerIsVisible(ref players);
            }
        }

        return players.ToArray();
    }

    private bool PlayerInRange(ref List<Transform> players)
    {
        bool res = false;
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, Player);

        if (playerColliders.Length != 0)
        {
            res = true;

            foreach (Collider2D player in playerColliders)
            {
                players.Add(player.transform);
            }
        }

        return res;
    }

    private bool PlayerInAngle(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            float angle = GetAngle(players[i]);

            if (angle > visionAngle / 2)
            {
                players.RemoveAt(i);
            }
        }

        return players.Count > 0;
    }

    private float GetAngle(Transform target)
    {
        Vector2 targetDir = target.position - transform.position;
        float angle = Vector2.Angle(targetDir, transform.right);

        return angle;
    }

    private bool PlayerIsVisible(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            bool isVisible = IsVisible(players[i]);

            if (!isVisible)
            {
                _ = players.Remove(players[i]);
            }
        }

        return players.Count > 0;
    }

    private bool IsVisible(Transform target)
    {
        Vector3 targetDir = target.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, detectionRange, Player);

        return (hit.collider != null) && (hit.collider.transform == target);
    }
}
